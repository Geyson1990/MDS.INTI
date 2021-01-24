using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Minedu.Core.General.Communication.Extension;
using Newtonsoft.Json;
using Minedu.Core.General.Communication;
using System.Net;
using System.Text.RegularExpressions;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Middleware
{
    public class HttpMethodSecurityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private string _param = "_param";
        private bool isValidParametro = true;
        private StatusResponse respuestas;

        public HttpMethodSecurityMiddleware(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        private bool IsValidParametro(string strInput)
        {
            Regex rg = new Regex(@"^[a-zñA-Z0-9ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïñòóôõöùúûüýÿ.&'/=^_`|~\-\s,]*$");
            return rg.IsMatch(strInput);
        }

        public bool IsValidJson(string strInput, string method)
        {
            respuestas = new StatusResponse<string>();
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) ||
                (strInput.StartsWith("[") && strInput.EndsWith("]")))
            {
                try
                {
                    var jToken = JToken.Parse(strInput);
                    //parse the input into a JObject
                    var jObject = JObject.Parse(strInput);

                    foreach (var jo in jObject)
                    {
                        string name = jo.Key;
                        JToken value = jo.Value;

                        //if the element has a missing value, it will be Undefined - this is invalid
                        if (value.Type == JTokenType.Undefined)
                        {
                            return false;
                        }
                        if(method == "GET")
                        {
                            if (IsValidParametro(jo.Value.ToString()) == false)
                            {
                                isValidParametro = false;
                                respuestas.Validations.Add(new MessageStatusResponse("El campo " + jo.Key + " no es válido.", "03"));
                            }
                        }
                        
                    }
                    return respuestas.Validations.Count() > 0 ? false : true;
                    //turn true;
                }
                catch (JsonReaderException jex)
                {
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var request = httpContext.Request;
            if (request.Path.StartsWithSegments(new PathString("/api")))
            {
                JObject jObject = null;
                string bodyParametro = "";
                var originalBodyStream = httpContext.Response.Body;
                bool existeParam = false;
                int numeroParametros = 0;
                bool isValidJson = true;
                bool isValidParam = false;
                bool isValidValueParam = false;
                switch (request.Method)
                {
                    case "GET":
                        IQueryCollection query = request.Query;
                        numeroParametros = query.Count;
                        if (query.Count == 1)
                        {
                            if (query.Keys.Contains(_param))
                            {
                                existeParam = true;
                                string valueDecrypt = HttpMethodEncryptationSecurity.TryDecrypt<string>(request.QueryString.Value.Replace("?" + _param + "=", ""), "");
                                if (valueDecrypt != "")
                                {
                                    isValidParam = true;
                                    isValidValueParam = this.IsValidJson(valueDecrypt,"GET");
                                }
                                string cadena = SanitizeFrom.JsonRequest(valueDecrypt);
                                request.QueryString = new QueryString("?" + cadena);
                            }
                        }
                        else if (query.Count == 0)
                        {
                            isValidJson = true; existeParam = true; isValidParam = true; isValidValueParam = true;
                        }
                        break;
                    case "POST":
                    case "PUT":
                    case "PATCH":
                    case "DELETE":
                        if (request.ContentType != null && request.ContentType.Contains("multipart/form-data"))
                        {
                            IFormCollection form = await request.ReadFormAsync();
                            Dictionary<string, StringValues> dictValues = new Dictionary<string, StringValues>();

                            StringValues value;
                            foreach (KeyValuePair<string, StringValues> pair in form)
                            {
                                if (pair.Key == _param)
                                {
                                    existeParam = true;
                                    value = new StringValues(HttpMethodEncryptationSecurity.TryDecrypt<string>(pair.Value, ""));
                                }
                                else
                                {
                                    value = pair.Value;
                                }
                                dictValues.Add(pair.Key, Enumerable.First<string>(value));
                            }

                            request.Form = new FormCollection(dictValues, form.Files);
                        }
                        else
                        {
                            using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                            {
                                bodyParametro = await reader.ReadLineAsync();
                                if (bodyParametro == null)
                                {
                                    isValidJson = false;
                                    break;
                                }
                                isValidJson = this.IsValidJson(bodyParametro, "POST");

                                if (isValidJson) // si es valido el formato del JSON 
                                {
                                    var json = JObject.Parse(bodyParametro);
                                    numeroParametros = json.Count;
                                    if (json.Count == 1 && json.ContainsKey(_param))
                                    {
                                        existeParam = true;
                                        bodyParametro = json[_param].ToString();
                                        var valueDecrypt = HttpMethodEncryptationSecurity.TryDecrypt<string>(bodyParametro, "");
                                        if (valueDecrypt != "")
                                        {
                                            isValidParam = true;
                                            // validamos que el valor desencriptado sea valido
                                            isValidValueParam = this.IsValidJson(valueDecrypt,"POST");
                                        }
                                        var requestContent = new StringContent(valueDecrypt, Encoding.UTF8);
                                        request.Body = await requestContent.ReadAsStreamAsync();
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }

                if (isValidJson && existeParam && isValidParam && isValidValueParam)
                {

                    await using var responseBody = _recyclableMemoryStreamManager.GetStream();
                    httpContext.Response.Body = responseBody;

                    await _next(httpContext);

                    httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

                    var contentType = httpContext.Response.ContentType?.ToLower();
                    contentType = contentType?.Split(';', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

                    if (contentType != null && contentType.Contains("application/json"))
                    {
                        if (jObject == null)
                        {
                            jObject = new JObject();
                        }

                        httpContext.Response.Headers.Add(_param, _param);

                        bodyParametro = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

                        var parseParametro = JsonConvert.DeserializeObject(bodyParametro);
                        var fsdfsd = JObject.Parse(parseParametro.ToString());

                        jObject[_param] = HttpMethodEncryptationSecurity.TryEncrypt(bodyParametro);
                        bodyParametro = jObject.ToString();

                        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
                        await httpContext.Response.WriteAsync(bodyParametro);
                        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
                    }

                    await responseBody.CopyToAsync(originalBodyStream);
                }
                else
                {
                    var response = new StatusResponse<string>();
                    response.Success = false;
                    response.Data = null;

                    if (!isValidJson)
                    {
                        response.Validations.Add(new MessageStatusResponse("El parámetro enviado, no es un JSON válido", "00"));
                    }
                    else
                    {
                        if (respuestas.Validations.Count() > 0)
                        {
                            response.Validations = respuestas.Validations;
                            //respuestas.Validations.RemoveRange(0,respuestas.Validations.Count());
                        }
                        else
                        {
                            if (!existeParam)
                            {
                                if (numeroParametros <= 1)
                                    response.Validations.Add(new MessageStatusResponse("No se envió el parámetro Http [_param] para ejecutar el servicio", "01"));
                                else
                                    response.Validations.Add(new MessageStatusResponse("Solo se acepta el parámetro Http [_param]  para ejecutar el servicio", "01"));
                            }
                            else
                            {
                                if (!isValidParam)
                                    response.Validations.Add(new MessageStatusResponse("El valor del parámetro Http [_param]  no es válido", "02"));
                                else
                                    response.Validations.Add(new MessageStatusResponse("El valor del parámetro Http [_param]  no tiene un formato Json válido", "02"));
                            }
                        }
                    }

                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";

                    jObject = new JObject();
                    jObject[_param] = HttpMethodEncryptationSecurity.TryEncrypt(JsonConvert.SerializeObject(response));
                    bodyParametro = jObject.ToString();
                    await httpContext.Response.WriteAsync(bodyParametro, Encoding.UTF8);
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}

