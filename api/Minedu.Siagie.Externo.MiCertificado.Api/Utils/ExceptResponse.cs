using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Utils
{
    public class ExceptResponse
    {
        private readonly IInstitucionEducativaService _institucioneducativaService;

        public ExceptResponse(IInstitucionEducativaService institucioneducativaService)
        {
            _institucioneducativaService = institucioneducativaService;
        }

        public static StatusResponse<IEnumerable<object>> ProcesarRespuesta(IEnumerable<object> items, IEnumerable<NivelRequestDto> inicialCuna, string idSistema)
        {
            var listaObjetos = new List<object>();
            var response = new StatusResponse<IEnumerable<object>>();
            //Preguntar si hay Niveles A1
            foreach (var item in items)
            {
                int i = 0;
                var properties = item.GetType().GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    string pName = p.Name.ToString();
                    if (pName == "IdNivel")
                    {
                        string pValue = p.GetValue(item).ToString();
                        if (pValue != "A1")
                        {
                            if (pValue != "E0")
                            {
                                listaObjetos.Add(item);
                                break;
                            }
                        }                                                           
                    }
                }
            }

            if (inicialCuna.Count() > 0)
            {
                var nivel = inicialCuna.FirstOrDefault().IdNivel;
                response.Success = listaObjetos.Any() ? true : false;
                response.Message = items.Count() == 1 ? "El nivel "+nivel+" no es contemplado para el proyecto MiCertificado." : "Consulta Exitosa";
                response.Data = listaObjetos.Count()>0 ? listaObjetos : null;
            }
            else
            {
                if (items.Any())
                {
                    response.Message = "Consulta exitosa";
                    response.Success = true;
                    response.Data = items;
                }
                else
                {
                    if (idSistema != "")
                    {
                        response.Message = (idSistema == "1" ?
                                            "No se encontró información sobre registros a partir del año 2011." : 
                                            "No se encontró información sobre registros a partir del año 2013.");
                    }
                    else
                    {
                        response.Message = "No se encontró información con los filtros ingresados.";
                    }
                    response.Success = false;
                    response.Data =  null;
                }
            }
            return response;
        }
    }
}
