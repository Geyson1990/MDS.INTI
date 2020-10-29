using iText.Barcodes;
using iText.Barcodes.Qrcode;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minedu.Comun.Helper;
using MuniSayan.Application.Contracts.Security;
using MuniSayan.Application.Contracts.Services;
using Mappers = MuniSayan.Application.Mappers;
using Models = MuniSayan.BusinessLogic.Models;
using Entities = MuniSayan.DataAccess.Contracts.Entities;
using MuniSayan.DataAccess.Contracts.UnitOfWork;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuniSayan.Application.Utils;
using System.IO;
using MuniSayan.Application.Constants;
using System.Globalization;
using iText.Kernel.Pdf.Extgstate;
using iText.Kernel.Colors;
using MuniSayan.Application.Security;
using MuniSayan.Application.Mappers.Certificado;

namespace MuniSayan.Application.Services
{
    public class CertificadoService : ICertificadoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReniecService _reniecService;
        private readonly ISiagieService _siagieService;
        private readonly IConfiguration _configuration;
        private readonly IEncryptionServerSecurity _encryptionServerSecurity;
        public readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISeguridadService _seguridadService;

        public CertificadoService(IUnitOfWork unitOfWork,
            IReniecService reniecService,
            ISiagieService siagieService,
            IConfiguration configuration,
            IEncryptionServerSecurity encryptionServerSecurity,
            IHttpContextAccessor httpContextAccessor,
            ISeguridadService seguridadService
            )
        {
            _unitOfWork = unitOfWork;
            _reniecService = reniecService;
            _siagieService = siagieService;
            _configuration = configuration;
            _encryptionServerSecurity = encryptionServerSecurity;
            _httpContextAccessor = httpContextAccessor;
            _seguridadService = seguridadService;
        }

        public async Task<StatusResponse> ConsultarCertificado(Models.Certificado.CertificadoModel model)
        {
            var result = new StatusResponse();

            try
            {
                var certificado = await _unitOfWork.GetCertificado(Mappers.Certificado.CertificadoMapper.Map(model));

                certificado.ToList();

                if (certificado != null)
                {

                    result.Success = true;
                    result.Data = certificado.Select(x => new
                    {
                        ID_SOLICITUD = _encryptionServerSecurity.Encrypt(x.ID_SOLICITUD.ToString()),
                        TIPO_DOCUMENTO = x.TIPO_DOCUMENTO,
                        NUMERO_DOCUMENTO = x.NUMERO_DOCUMENTO,
                        NOMBRE_ESTUDIANTE = x.NOMBRE_ESTUDIANTE,
                        APELLIDO_PATERNO = x.APELLIDO_PATERNO,
                        APELLIDO_MATERNO = x.APELLIDO_MATERNO,
                        NOMBRE = x.NOMBRE,
                        CORREO_ELECTRONICO = x.CORREO_ELECTRONICO,
                        ULTIMO_ANIO = x.ULTIMO_ANIO,
                        FECHA_SOLICITUD = x.FECHA_SOLICITUD,
                        ESTADO_SOLICITUD = x.ESTADO_SOLICITUD,
                        DSC_ESTADO_SOLICITUD = x.DSC_ESTADO_SOLICITUD,
                        ID_ESTUDIANTE = _encryptionServerSecurity.Encrypt(x.ID_ESTUDIANTE.ToString()),
                        ID_SOLICITANTE = _encryptionServerSecurity.Encrypt(x.ID_SOLICITANTE.ToString()),
                        ID_MOTIVO = _encryptionServerSecurity.Encrypt(x.ID_MOTIVO.ToString()),
                        ID_MODALIDAD = ReactEncryptationSecurity.Encrypt(x.ID_MODALIDAD.ToString()),
                        ABR_MODALIDAD = x.ABR_MODALIDAD,
                        DSC_MODALIDAD = x.DSC_MODALIDAD,
                        ID_NIVEL = ReactEncryptationSecurity.Encrypt(x.ID_NIVEL.ToString()),
                        DSC_NIVEL = x.DSC_NIVEL,
                        ID_GRADO = x.ID_GRADO,
                        DSC_GRADO = x.DSC_GRADO,
                        FECHA_PROCESO = x.FECHA_PROCESO,
                        CODIGO_VIRTUAL = x.CODIGO_VIRTUAL,
                        ID_PERSONA = x.ID_PERSONA

                    }).ToList();
                    result.Messages.Add("Solicitud exitosa.");
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Se presentó un inconveniente al validar la información en el sistema.");
                return result;
            }
        }

        public async Task<int> UpdateCertificado(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var rows = default(int); 
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var model = JsonConvert.DeserializeObject<Models.Certificado.CertificadoModel>(desencriptarObjeto);            

            try
            {
                model.IdSolicitud = _encryptionServerSecurity.Decrypt<int>(model.CodSolicitud.ToString(), 0);
                model.IdMotivo = model.CodMotivo.Length > 2 ? _encryptionServerSecurity.Decrypt<int>(model.CodMotivo.ToString(), 0) : Convert.ToInt32(model.CodMotivo);
                //model.IdPersona = _encryptionServerSecurity.Decrypt<int>(model.IdPersona.ToString(), 0);
                //model.IdEstudiante = _encryptionServerSecurity.Decrypt<int>(model.IdEstudiante.ToString(), 0);
                // model.IdSolicitante = _encryptionServerSecurity.Decrypt<int>(model.IdSolicitud.ToString(), 0);

                //var certificado = _unitOfWork.
                var certificadoModel = new Models.Certificado.CertificadoModel
                {
                    IdSolicitud = model.IdSolicitud,
                    IdMotivo = model.IdMotivo
                };

                var listaSolicitud = await _unitOfWork.GetSolicitud(Mappers.Certificado.CertificadoMapper.Map(certificadoModel));

                var solicitud = listaSolicitud.FirstOrDefault();

                solicitud.ID_MOTIVO = model.IdMotivo;
                solicitud.ESTADO_SOLICITUD = model.EstadoSolicitud;

                _unitOfWork.BeginTransaction();

                rows = await _unitOfWork.UpdateCertificado(solicitud);

                if (rows > 0)
                {
                    _unitOfWork.Commit();

                    var estudianteResponse = await _unitOfWork.ObtenerSolicitudPorID(model.IdSolicitud);
                    //var informacionEstudiante = estudianteResponse.FirstOrDefault();
                    Models.Certificado.SolicitudCertificadoModel solicitudEstudiante = Mappers.Certificado.SolicitudCertificadoMapper.Map(estudianteResponse.FirstOrDefault());

                    var nombresCompleto = solicitudEstudiante.apellidoPaterno + " " + solicitudEstudiante.apellidoMaterno + ", " + solicitudEstudiante.nombre;
                    if (solicitudEstudiante.correoElectronico != "")
                    {
                        var correo = PrepararCorreo(solicitudEstudiante, nombresCompleto, solicitudEstudiante.correoElectronico);
                        var correoResult = await EnviarCorreo(correo);
                    }
                }
                else
                {
                    _unitOfWork.Rollback();
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
            }

            return rows;
        }

        public async Task<StatusResponse> ConsultarDatosAcademicos(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var result = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var model = JsonConvert.DeserializeObject<Models.Certificado.CertificadoModel>(desencriptarObjeto);
            List<Models.Certificado.PDFNotaCertificado> competenciaList = new List<Models.Certificado.PDFNotaCertificado>();
            List<Models.Certificado.PDFNotaCertificado> tallerList = new List<Models.Certificado.PDFNotaCertificado>();
            List<Models.Certificado.PDFNotaCertificado> cursoList = new List<Models.Certificado.PDFNotaCertificado>();
            List<Models.Certificado.ObservacionCertificadoModel> observacionlist = new List<Models.Certificado.ObservacionCertificadoModel>();
            List<Models.Certificado.GradoCertificadoModel> gradoList = new List<Models.Certificado.GradoCertificadoModel>();

            var FlagDatosSiagie = 0;

            var siagie = _encryptionServerSecurity.Decrypt<string>(
                ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                , "");

            var decrypted = new Models.Certificado.CertificadoModel
            {
                IdSolicitud = _encryptionServerSecurity.Decrypt<int>(model.CodSolicitud, 0)
            };


            var certificadoEntidad = await _unitOfWork.GetSolicitudCertificado(Mappers.Certificado.CertificadoMapper.Map(decrypted));

            var listaCertificadoEntidad = certificadoEntidad.ToList();


            try
            {
                var decryptedCertificate = new Models.Certificado.EstudianteModalidadNivelRequest2
                {
                    idModalidad = listaCertificadoEntidad[0].ID_MODALIDAD,
                    idNivel = listaCertificadoEntidad[0].ID_NIVEL,
                    idPersona = listaCertificadoEntidad[0].ID_PERSONA.ToString()
                };

                if (decryptedCertificate.idModalidad != null && decryptedCertificate.idNivel != null)
                {
                    FlagDatosSiagie = 1;
                    var ListaSiagieGradosEstudiante = await SiagieGradosEstudiante(siagie, decryptedCertificate);
                    if (ListaSiagieGradosEstudiante != null)
                    {
                        gradoList = ListaSiagieGradosEstudiante.ToList();

                        var ListaSiagieNotasEstudiante = await SiagieNotasEstudiante(siagie, decryptedCertificate);
                        if (ListaSiagieNotasEstudiante != null)
                        {
                            cursoList = OrdenarNotasEstudiante(ListaSiagieNotasEstudiante, ListaSiagieGradosEstudiante, "001");
                            competenciaList = OrdenarNotasEstudiante(ListaSiagieNotasEstudiante, ListaSiagieGradosEstudiante, "003");
                            tallerList = OrdenarNotasEstudiante(ListaSiagieNotasEstudiante, ListaSiagieGradosEstudiante, "002");                            
                        }
                    }

                    var encryptedCertificate = new Models.Certificado.EstudianteModalidadNivelRequest2
                    {
                        idPersona = _encryptionServerSecurity.Encrypt(listaCertificadoEntidad[0].ID_PERSONA.ToString()),
                        idModalidad = ReactEncryptationSecurity.Encrypt(listaCertificadoEntidad[0].ID_MODALIDAD),
                        idNivel = ReactEncryptationSecurity.Encrypt(listaCertificadoEntidad[0].ID_NIVEL)
                    };

                    List<Models.Certificado.ObservacionCertificadoModel> listObs = await SiagieObservacionesEstudiante(siagie, encryptedCertificate);

                    if (listObs != null)
                    {
                        observacionlist = listObs;
                    }
                }
                
                var InfoEstudiante = new Models.Siagie.EstudianteInfoPorNivel2
                {
                    codigoPersona = _encryptionServerSecurity.Encrypt(listaCertificadoEntidad[0].ID_PERSONA.ToString()),
                    numeroDocumento = listaCertificadoEntidad[0].NUMERO_DOCUMENTO,
                    apellidoPaterno = listaCertificadoEntidad[0].APELLIDO_PATERNO,
                    apellidoMaterno = listaCertificadoEntidad[0].APELLIDO_MATERNO,
                    nombres = listaCertificadoEntidad[0].NOMBRE,
                    idNivel = decryptedCertificate.idNivel,
                    dscNivel = ConsultarNivelEducativo(decryptedCertificate.idNivel).descripcionNivel,
                    idAnio = listaCertificadoEntidad[0].ULTIMO_ANIO,
                };

                var lista = new Models.Certificado.DatosEstudianteResponse
                {
                    estudiante = InfoEstudiante,
                    cursoList = FlagDatosSiagie == 1 ? cursoList : null,
                    competenciaList = FlagDatosSiagie == 1 ? competenciaList : null,
                    tallerList = FlagDatosSiagie == 1 ? tallerList : null,
                    gradosList = FlagDatosSiagie == 1 ? gradoList : null,
                    observacionesList = FlagDatosSiagie == 1 ? observacionlist : null
                };

                result.Data = ReactEncryptationSecurity.Encrypt(JsonConvert.SerializeObject(lista));
                result.Success = true;
                result.Messages.Add("Consulta Exitosa");

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Success = false;
                result.Messages.Add("Error");
            }

            return result;
        }


        private async Task<Models.Certificado.EstudianteCertificado> SiagieEstudiante(string siagie, Models.Certificado.EstudianteModalidadNivelPersonaRequest2 request)
        {
            Models.Certificado.EstudianteCertificado estudianteCertificado = new Models.Certificado.EstudianteCertificado();

            //OBTENER DATA DE SIAGIE
            var infoEstudiante = await SiagieInfoEstudiante(siagie, request);
            if (infoEstudiante == null)
            {
                return null;
            }

            estudianteCertificado.solicitud = new Models.Certificado.SolicitudCertificadoModel()
            {
                idModalidad = infoEstudiante.idModalidad,
                abrModalidad = infoEstudiante.abrModalidad,
                dscModalidad = infoEstudiante.dscModalidad,
                idNivel = infoEstudiante.idNivel,
                dscNivel = infoEstudiante.dscNivel,
                idGrado = infoEstudiante.idGrado,
                dscGrado = infoEstudiante.dscGrado,
                director = infoEstudiante.director
            };

            estudianteCertificado.estudiante = new Models.Certificado.EstudianteCertificadoModel()
            {
                idPersona = infoEstudiante.idPersona,
                idTipoDocumento = infoEstudiante.idTipoDocumento,
                numeroDocumento = infoEstudiante.numeroDocumento,
                apellidoPaterno = infoEstudiante.apellidoPaterno,
                apellidoMaterno = infoEstudiante.apellidoMaterno,
                nombres = infoEstudiante.nombres
            };

            var requestModalidadNivel = new Models.Certificado.EstudianteModalidadNivelRequest2()
            {
                idPersona = request.idPersona.Length < 12
                    ? request.idPersona
                    : _encryptionServerSecurity.Decrypt<int>(request.idPersona, 0).ToString(),
                idModalidad = request.idModalidad.Length < 5
                    ? request.idModalidad
                    : ReactEncryptationSecurity.Decrypt<string>(request.idModalidad, "00"),
                idNivel = request.idNivel.Length < 5
                    ? request.idNivel
                    : ReactEncryptationSecurity.Decrypt<string>(request.idNivel, "00")
            };

            estudianteCertificado.grados = await SiagieGradosEstudiante(siagie, requestModalidadNivel);
            if (estudianteCertificado.grados == null || estudianteCertificado.grados.Count == 0)
            {
                return null;
            }

            //List<NotaModel> notasList = await SiagieNotasEstudiante(idPersona, idModalidad, idNivel);
            estudianteCertificado.notas = await SiagieNotasEstudiante(siagie, requestModalidadNivel);
            if (estudianteCertificado.notas == null || estudianteCertificado.notas.Count == 0)
            {
                return null;
            }

            estudianteCertificado.observaciones = await SiagieObservacionesEstudiante(siagie, requestModalidadNivel);

            return estudianteCertificado;
        }

        private async Task<Models.Siagie.EstudianteInfoPorNivel> SiagieInfoEstudiante(string siagie, Models.Certificado.EstudianteModalidadNivelPersonaRequest2 request)
        {
            try
            {
                var decrypted = new
                {
                    token = siagie,
                    idPersona = request.idPersona.Length < 12
                        ? Convert.ToInt32(request.idPersona)
                        : _encryptionServerSecurity.Decrypt<int>(request.idPersona, 0),
                    idModalidad = request.idModalidad.Length < 8
                        ? request.idModalidad
                        : ReactEncryptationSecurity.Decrypt<string>(request.idModalidad, "00"),
                    idNivel = request.idNivel.Length < 12
                        ? request.idNivel
                        : ReactEncryptationSecurity.Decrypt<string>(request.idNivel, "00"),
                    idTipoDocumento = request.idTipoDocumento,
                    numeroDocumento = request.numeroDocumento.Length < 18
                        ? request.numeroDocumento
                        : ReactEncryptationSecurity.Decrypt<string>(request.numeroDocumento,"00"),
                    idSistema = "1"
                };


                var statusResponse = await _siagieService
                        .GetServiceByQueryAndToken<StatusResponse,
                        object>(decrypted.token, "pdf/info", decrypted);

                //Información del estudiante en Siagie: FAIL?
                if (!statusResponse.Success)
                {
                    return null;
                }

                //Información del estudiante en Siagie: OK
                var result = JsonConvert
                    .DeserializeObject<List<Models.Siagie.EstudianteInfoPorNivel>>(statusResponse.Data.ToString())
                    .FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<List<Models.Certificado.GradoCertificadoModel>> SiagieGradosEstudiante(string siagie, Models.Certificado.EstudianteModalidadNivelRequest2 request)
        {
            try
            {
                var decrypted = new
                {
                    token = siagie,
                    idPersona = request.idPersona == "0" ? 1 : Convert.ToInt32(request.idPersona),
                    request.idModalidad,
                    request.idNivel,
                    idSistema = "1"
                };

                var statusResponse = await _siagieService
                        .GetServiceByQueryAndToken<StatusResponse,
                        object>(decrypted.token, "pdf/grados", decrypted);

                //Información de grados del estudiante en Siagie: FAIL?
                if (!statusResponse.Success)
                {
                    return null;
                }

                //Información de grados del estudiante en Siagie: OK
                var result = JsonConvert
                    .DeserializeObject<List<Models.Certificado.GradoCertificadoModel>>(statusResponse.Data.ToString())
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<List<Models.Certificado.NotaCertificadoModel>> SiagieNotasEstudiante(string siagie, Models.Certificado.EstudianteModalidadNivelRequest2 request)
        {
            try
            {
                var decrypted = new
                {
                    token = siagie,
                    idPersona = Convert.ToInt32(request.idPersona),
                    request.idModalidad,
                    request.idNivel,
                    idSistema = "1"
                };

                var statusResponse = await _siagieService
                        .GetServiceByQueryAndToken<StatusResponse,
                        object>(decrypted.token, "pdf/notas", decrypted);

                //Información de notas del estudiante en Siagie: FAIL?
                if (!statusResponse.Success)
                {
                    return null;
                }

                //Información de notas del estudiante en Siagie: OK
                var result = JsonConvert
                    .DeserializeObject<List<Models.Certificado.NotaCertificadoModel>>(statusResponse.Data.ToString())
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // ORDENAR NOTAS CON LINQ
        private List<Models.Certificado.PDFNotaCertificado> OrdenarNotasEstudiante(List<Models.Certificado.NotaCertificadoModel> notasList, List<Models.Certificado.GradoCertificadoModel> gradosList, string idTipoArea)
        {
            List<Models.Certificado.PDFNotaCertificado> constanciaNotasList = new List<Models.Certificado.PDFNotaCertificado>();
            constanciaNotasList = notasList
                                   .Where(z => z.idTipoArea == idTipoArea)
                                   .GroupBy(c => new { c.idArea, c.dscArea, c.idTipoArea, c.dscTipoArea, c.esAreaSiagie })
                                   .Select(g => new Models.Certificado.PDFNotaCertificado
                                   {
                                       IdArea = g.Key.idArea,
                                       DscArea = g.Key.dscArea,
                                       IdTipoArea = g.Key.idTipoArea,
                                       DscTipoArea = g.Key.dscTipoArea,
                                       EsAreaSiagie = g.Key.esAreaSiagie,
                                       GradoNotas = gradosList
                                                       .GroupBy(a => new { a.idGrado, a.dscGrado, a.idAnio, a.codMod })
                                                       .Select(b => new Models.Certificado.PDFGradoNotaCertificado
                                                       {
                                                           IdGrado = b.Key.idGrado,
                                                           DscGrado = b.Key.dscGrado,
                                                           AnioElectivo = b.Key.idAnio,
                                                           CodigoModular = b.Key.codMod,
                                                           NotaFinalArea = notasList
                                                                               .Where(w => w.idGrado == b.Key.idGrado && w.idArea.Trim() == g.Key.idArea.Trim())
                                                                               .Select(h => h.notaFinalArea)
                                                                               .FirstOrDefault(),
                                                           Estado = notasList.Where(w => w.idGrado.Trim() == b.Key.idGrado.Trim() && w.idArea.Trim() == g.Key.idArea.Trim() && w.notaFinalArea != null).Count() > 0 ? "1" : "0"
                                                       }).ToList(),
                                       Estado = notasList.Where(x => x.idArea.Trim() == g.Key.idArea.Trim() && x.estado == "0").Select(c => c.estado).Count() == notasList.Where(x => x.idArea.Trim() == g.Key.idArea.Trim()).Select(c => c.estado).Count() ? "0" : "1",

                                       /*gradosList
                                                       .GroupBy(a => new { a.idGrado, a.dscGrado, a.idAnio, a.codMod })
                                                       .Select(b => new Models.Certificado.PDFGradoNotaCertificado
                                                       {
                                                           IdGrado = b.Key.idGrado,
                                                           NotaFinalArea = notasList
                                                                               .Where(w => w.idGrado == b.Key.idGrado && w.idArea == g.Key.idArea)
                                                                               .Select(h => h.notaFinalArea)
                                                                               .FirstOrDefault(),
                                                           Estado = notasList.Where(w => w.idGrado.Trim() == b.Key.idGrado.Trim() && w.idArea.Trim() == g.Key.idArea.Trim() && w.notaFinalArea != null).Count() > 0 ? "1" : "0"
                                                       }).Where(x=>x.NotaFinalArea==null).Count() > 0 ? "1" :"0"*/
                                   })
                                   .OrderBy(y => y.DscArea)
                                   .ToList();

            return constanciaNotasList;
        }

        private async Task<List<Models.Certificado.ObservacionCertificadoModel>> SiagieObservacionesEstudiante(string siagie, Models.Certificado.EstudianteModalidadNivelRequest2 request)
        {
            try
            {
                var decrypted = new
                {
                    token = siagie,
                    idPersona = request.idPersona.Length>10
                                ? _encryptionServerSecurity.Decrypt<int>(request.idPersona, 0) : Convert.ToInt32(request.idPersona),
                    idModalidad = request.idModalidad.Length > 5
                                ? ReactEncryptationSecurity.Decrypt<string>(request.idModalidad, "00"): request.idModalidad,
                    idNivel = request.idNivel.Length > 5
                                ? ReactEncryptationSecurity.Decrypt<string>(request.idNivel, "00") : request.idNivel,
                    idSistema = "1"
                };

                var statusResponse = await _siagieService
                        .GetServiceByQueryAndToken<StatusResponse,
                        object>(decrypted.token, "pdf/observaciones", decrypted);

                //Información de grados del estudiante en Siagie: FAIL?
                if (!statusResponse.Success)
                {
                    return null;
                }

                //Información de grados del estudiante en Siagie: OK
                var result = JsonConvert
                    .DeserializeObject<List<Models.Certificado.ObservacionCertificadoModel>>(statusResponse.Data.ToString())
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<StatusResponse> InsertRegistroSolicitud(Models.Certificado.ParametroModel objetoEncriptado)
        {//CertificadoRequest
            var result = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var request = JsonConvert.DeserializeObject<Models.Certificado.CertificadoRequest>(desencriptarObjeto);

            int idSolicitud = 0;
            bool flagEstMan = false;
            var siagie = _encryptionServerSecurity.Decrypt<string>(
                ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                , "");

            var estadoEstudiante = 1;

            var personaRequest = new Models.PersonaRequest()
            {
                tipoDocumento = request.estudiante.tipoDocEstudiante,
                nroDocumento = ReactEncryptationSecurity.Decrypt<string>(request.estudiante.nroDocEstudiante, "00")
            };

            var estudianteInfoRequest = new Models.Siagie.EstudianteInfoPorCodModularRequest()
            {
                codMod = request.solicitud.codigoModular,
                anexo  = request.solicitud.anexo
            };

            var estudianteRequest = await _siagieService
            .GetServiceByQueryAndToken<StatusResponse, Models.PersonaRequest>(siagie, "estudiante", personaRequest);

            var siagie2 = _encryptionServerSecurity.Decrypt<string>(
             ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
             , "");

            var estudianteInfoPorCodModular = await _siagieService
            .GetServiceByQueryAndToken<StatusResponse, Models.Siagie.EstudianteInfoPorCodModularRequest>(siagie2, "datosalumno", estudianteInfoRequest);

            Models.ReniecPersona estudiantePersona = await _reniecService.ReniecConsultarPersona(ReactEncryptationSecurity.Decrypt<string>(request.estudiante.nroDocEstudiante, "00"));
            if (estudiantePersona == null)
            {
                flagEstMan = true;
            }

            int idPersonaEstudiante = 0;

            if (estudianteRequest.Data == null)
            {
                idPersonaEstudiante = 0;
            }
            else
            {
                var responseEstudiante = JsonConvert
                .DeserializeObject<List<Models.Siagie.EstudiantePersonaResponse>>(estudianteRequest.Data.ToString())
                .First();
                idPersonaEstudiante = responseEstudiante.idPersonaEstudiante;
            }

            string idModalidad, abrModalidad, dscModalidad, idNivel, dscNivel, idGrado, dscGrado, director;
            int anioCulminacion;

            if (estudianteInfoPorCodModular.Data == null)
            {
                idModalidad = null;
                abrModalidad = null;
                dscModalidad = null;
                idNivel = null;
                dscNivel = null;
                idGrado = null;
                dscGrado = null;
                director = "";
            }
            else
            {
                var responseInfoEstudiante = JsonConvert
                .DeserializeObject<List<Models.Siagie.EstudianteInfoPorCodModularResponse>>(estudianteInfoPorCodModular.Data.ToString())
                .First();

                idModalidad = responseInfoEstudiante.idModalidad;
                abrModalidad = responseInfoEstudiante.abrModalidad;
                dscModalidad = responseInfoEstudiante.dscModalidad;
                idNivel = responseInfoEstudiante.idModalidad == "03" ? ReactEncryptationSecurity.Decrypt<string>(request.solicitud.idNivel, "00") : responseInfoEstudiante.idNivel;
                dscNivel = responseInfoEstudiante.dscNivel;
                idGrado = responseInfoEstudiante.idGrado;
                dscGrado = responseInfoEstudiante.dscGrado;
                director = responseInfoEstudiante.director;
                if (idPersonaEstudiante != 0)
                {
                    //Evaluar estado de notas del estudiante
                    var siagie4 = _encryptionServerSecurity.Decrypt<string>(
                 ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                 , "");
                    var decrypted = new Models.Certificado.EstudianteModalidadNivelRequest2()
                    {
                        idPersona = idPersonaEstudiante.ToString(),
                        idModalidad = responseInfoEstudiante.idModalidad,
                        idNivel = responseInfoEstudiante.idModalidad == "03" ? ReactEncryptationSecurity.Decrypt<string>(request.solicitud.idNivel, "00") : responseInfoEstudiante.idNivel,
                        idSistema = "1"
                    };

                    var numeroNotasPendientes = await _siagieService.GetServiceByQueryAndToken<StatusResponse, Models.Certificado.EstudianteModalidadNivelRequest2>(siagie4, "pdf/grados", decrypted);

                    if (numeroNotasPendientes == null)
                    {
                        estadoEstudiante = 1;
                    }
                    else
                    {
                        if (numeroNotasPendientes.Data == null)
                        {
                            estadoEstudiante = 1;
                        }
                        else
                        {
                            var resultado = JsonConvert
                               .DeserializeObject<List<Models.Siagie.EstudianteModalidadNivelResponse>>(numeroNotasPendientes.Data.ToString())
                               .ToList();

                            var informacionCompleta = resultado.Where(x => x.estado == 1);
                            var sinInformacion = resultado.Where(x => x.estado == 0);

                            if (resultado.Count() == sinInformacion.Count())
                            {
                                estadoEstudiante = 1;
                            }
                            else if (resultado.Count() == informacionCompleta.Count())
                            {
                                estadoEstudiante = 3;
                            }
                            else
                            {
                                estadoEstudiante = 2;
                            }
                        }
                    }

                }
            }

            try
            {
                _unitOfWork.BeginTransaction();

                int resultEstudiante = 0;
                Models.Certificado.EstudianteCertificado estudianteConstancia = new Models.Certificado.EstudianteCertificado();

                if (flagEstMan)
                {
                    resultEstudiante = await _unitOfWork.InsertarEstudianteCertificado(Mappers.Certificado.EstudianteMapper.Map(new Models.Certificado.EstudianteCertificadoModel()
                    {
                        idEstudiante = 0,
                        idPersona = idPersonaEstudiante,
                        idTipoDocumento = request.estudiante.tipoDocEstudiante,
                        numeroDocumento = ReactEncryptationSecurity.Decrypt<string>(request.estudiante.nroDocEstudiante, "00"),
                        apellidoPaterno = request.estudiante.apellidoPaternoEstudiante,
                        apellidoMaterno = request.estudiante.apellidoMaternoEstudiante,
                        nombres = request.estudiante.nombresEstudiante,
                        ubigeo = null,
                        departamento = null,
                        provincia = null,
                        distrito = null
                    }));
                }
                else
                {
                    resultEstudiante = await _unitOfWork.InsertarEstudianteCertificado(Mappers.Certificado.EstudianteMapper.Map(new Models.Certificado.EstudianteCertificadoModel()
                    {
                        idEstudiante = 0,
                        idPersona = idPersonaEstudiante,
                        idTipoDocumento = request.estudiante.tipoDocEstudiante,
                        numeroDocumento = ReactEncryptationSecurity.Decrypt<string>(request.estudiante.nroDocEstudiante, "00"),
                        apellidoPaterno = estudiantePersona.apellidoPaterno,
                        apellidoMaterno = estudiantePersona.apellidoMaterno,
                        nombres = estudiantePersona.nombres,
                        ubigeo = estudiantePersona.ubigeoDomicilio,
                        departamento = estudiantePersona.dptoDomicilio,
                        provincia = estudiantePersona.provDomicilio,
                        distrito = estudiantePersona.distDomicilio
                    }));
                }

                int resultSolicitante = 0;

                if (request.solicitud.solicitante == "apoderado")
                {
                    resultSolicitante = await _unitOfWork.InsertarSolicitanteCertificado(Mappers.Certificado.SolicitanteMapper.Map(new Models.Certificado.SolicitanteCertificadoModel()
                    {
                        idSolicitante = 0,
                        idPersona = 0,
                        idTipoDocumento = request.solicitante.tipoDocApoderado,
                        numeroDocumento = ReactEncryptationSecurity.Decrypt<string>(request.solicitante.nroDocApoderado, "00"),
                        apellidoPaterno = request.solicitante.apellidoPaternoApoderado,
                        apellidoMaterno = request.solicitante.apellidoMaternoApoderado,
                        nombres = request.solicitante.nombresApoderado,
                        telefonoCelular = request.solicitud.telefonoContacto,
                        correoElectronico = request.solicitud.correoElectronico,
                        ubigeo =request.estudiante.ubigeoEstudiante,
                        departamento = request.estudiante.departamentoEstudiante,
                        provincia = request.estudiante.provinciaEstudiante,
                        distrito = request.estudiante.ubigeoEstudiante
                        //apellidoPaterno = estudiantePersona.apellidoPaterno,
                        //apellidoMaterno = estudiantePersona.apellidoMaterno,
                        //nombres = estudiantePersona.nombres,
                        //telefonoCelular = request.solicitud.telefonoContacto,
                        //correoElectronico = request.solicitud.correoElectronico,                  
                        //ubigeo = estudiantePersona.ubigeoDomicilio,
                        //departamento = estudiantePersona.dptoDomicilio,
                        //provincia = estudiantePersona.provDomicilio,
                        //distrito = estudiantePersona.distDomicilio
                    }));
                }
                else
                {

                    if (flagEstMan)
                    {
                        resultSolicitante = await _unitOfWork.InsertarSolicitanteCertificado(Mappers.Certificado.SolicitanteMapper.Map(new Models.Certificado.SolicitanteCertificadoModel()
                        {
                            idSolicitante = 0,
                            idPersona = idPersonaEstudiante,
                            idTipoDocumento = request.solicitante.tipoDocApoderado,
                            numeroDocumento = ReactEncryptationSecurity.Decrypt<string>(request.solicitante.nroDocApoderado, "00"),
                            apellidoPaterno = request.solicitante.apellidoPaternoApoderado,
                            apellidoMaterno = request.solicitante.apellidoMaternoApoderado,
                            nombres = request.solicitante.nombresApoderado,
                            telefonoCelular = request.solicitud.telefonoContacto,
                            correoElectronico = request.solicitud.correoElectronico,
                            ubigeo = request.solicitante.ubigeoApoderado, // CAMBIAR
                            departamento = null,
                            provincia = null,
                            distrito = null

                        }));
                    }
                    else
                    {

                        resultSolicitante = await _unitOfWork.InsertarSolicitanteCertificado(Mappers.Certificado.SolicitanteMapper.Map(new Models.Certificado.SolicitanteCertificadoModel()
                        {
                            idSolicitante = 0,
                            idPersona = idPersonaEstudiante,
                            idTipoDocumento = request.solicitante.tipoDocApoderado,
                            numeroDocumento = ReactEncryptationSecurity.Decrypt<string>(request.solicitante.nroDocApoderado, "00"),
                            //apellidoPaterno = request.solicitante.apellidoPaternoApoderado, // CAMBIAR
                            //apellidoMaterno = request.solicitante.apellidoMaternoApoderado, // CAMBIAR
                            //nombres = request.solicitante.nombresApoderado, // CAMBIAR
                            apellidoPaterno = estudiantePersona.apellidoPaterno,
                            apellidoMaterno = estudiantePersona.apellidoMaterno,
                            nombres = estudiantePersona.nombres,
                            telefonoCelular = request.solicitud.telefonoContacto,
                            correoElectronico = request.solicitud.correoElectronico,
                            //ubigeo = request.solicitante.ubigeoApoderado, // CAMBIAR
                            ubigeo = estudiantePersona.ubigeoDomicilio,
                            departamento = estudiantePersona.dptoDomicilio,
                            provincia = estudiantePersona.provDomicilio,
                            distrito = estudiantePersona.distDomicilio
                        }));
                    }
                }


                idSolicitud = await _unitOfWork.InsertarSolicitudCertificado(Mappers.Certificado.SolicitudMapper.Map(new Models.Certificado.SolicitudCertificadoModel()
                {
                    idSolicitud = 0,
                    idEstudiante = resultEstudiante,
                    idSolicitante = resultSolicitante,
                    idMotivo = _encryptionServerSecurity.Decrypt<int>(request.solicitud.idMotivo, 0),
                    idModalidad = idModalidad,
                    abrModalidad = abrModalidad,
                    dscModalidad = dscModalidad,
                    idNivel = idNivel,
                    dscNivel = dscNivel,
                    idGrado = request.estudiante.idGrado != null ? request.estudiante.idGrado : idGrado,
                    dscGrado = request.estudiante.dscGrado != null ? request.estudiante.dscGrado : dscGrado,
                    anioCulminacion = request.solicitud.anioCulminacion,
                    estadoSolicitud = request.solicitud.estadoSolicitud,
                    codigoModular = request.solicitud.codigoModular,
                    anexo = request.solicitud.anexo,
                    estadoEstudiante = estadoEstudiante.ToString(),
                    ciclo = request.solicitud.ciclo,
                    director = director,
                    motivoOtros = request.solicitud.motivo.ToUpper()
                }));

                if (idSolicitud <= 0)
                {
                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add("Ocurrió un problema al registrar su solicitud.");
                    return result;
                }
                
                if (estadoEstudiante == 3 && idPersonaEstudiante != 0 && idModalidad != "" && idNivel != "")
                {
                    var estudianteModalidadNivelPersona = new Models.Certificado.EstudianteModalidadNivelPersonaRequest2()
                    {
                        idPersona = idPersonaEstudiante.ToString(),
                        idModalidad = idModalidad.ToString(),
                        idNivel = idNivel.ToString(),
                        idTipoDocumento = request.estudiante.tipoDocEstudiante,
                        numeroDocumento = ReactEncryptationSecurity.Decrypt<string>(request.estudiante.nroDocEstudiante,"00"),
                    };

                    var siagie3 = _encryptionServerSecurity.Decrypt<string>(ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", ""), "");

                    estudianteConstancia = await SiagieEstudiante(siagie3, estudianteModalidadNivelPersona);

                    if (estudianteConstancia != null)
                    {
                        int countGrados = 0;
                        foreach (Models.Certificado.GradoCertificadoModel grado in estudianteConstancia.grados)
                        {
                            grado.idSolicitud = idSolicitud;
                            var idConstanciaGrado = await _unitOfWork.InsertarGradoCertificado(Mappers.Certificado.GradoCertificadoMapper.Map(grado));

                            if (idConstanciaGrado > 0)
                            {
                                countGrados++;
                            }
                        }

                        if (countGrados != estudianteConstancia.grados.Count)
                        {
                            result.Success = false;
                            result.Data = null;
                            result.Messages.Add("Ocurrió un problema al registrar su solicitud (2).");
                            return result;
                        }

                        int countNotas = 0;
                        foreach (Models.Certificado.NotaCertificadoModel nota in estudianteConstancia.notas)
                        {
                            nota.idSolicitud = idSolicitud;
                            var idConstanciaNota = await _unitOfWork.ActualizarNotaCertificado(Mappers.Certificado.NotaCertificadoMapper.Map(nota));

                            if (idConstanciaNota > 0)
                            {
                                countNotas++;
                            }
                        }

                        if (countNotas != estudianteConstancia.notas.Count())
                        {
                            result.Success = false;
                            result.Data = null;
                            result.Messages.Add("Ocurrió un problema al registrar su solicitud (3).");
                            return result;
                        }

                        if (estudianteConstancia.observaciones != null)
                        {
                            int countObs = 0;
                            foreach (Models.Certificado.ObservacionCertificadoModel obs in estudianteConstancia.observaciones)
                            {
                                obs.idSolicitud = idSolicitud;
                                var idConstanciaObservacion = await _unitOfWork.InsertarObservacionCertificado(ObservacionMapper.Map(obs));

                                if (idConstanciaObservacion > 0)
                                {
                                    countObs++;
                                }
                            }

                            if (countObs != estudianteConstancia.observaciones.Count())
                            {
                                result.Success = false;
                                result.Data = null;
                                result.Messages.Add("Ocurrió un problema al registrar su solicitud (4).");
                                return result;
                            }
                        }
                                                                         
                    }
                }

                _unitOfWork.Commit();

                var resultSolicitud = await _unitOfWork.ObtenerSolicitudCertificado(Mappers.Certificado.SolicitudCertificadoMapper.Map(new Models.Certificado.SolicitudCertificadoModel()
                {
                    idSolicitud = idSolicitud
                }));

                Models.Certificado.SolicitudCertificadoModel solicitud = Mappers.Certificado.SolicitudCertificadoMapper.Map(resultSolicitud.FirstOrDefault());
                var nombresCompleto = estudiantePersona.apellidoPaterno + " " + estudiantePersona.apellidoMaterno + ", " + estudiantePersona.nombres;
                if (request.solicitud.correoElectronico.Trim() != "")
                {
                    var correo = PrepararCorreo(solicitud, nombresCompleto,request.solicitud.correoElectronico.Trim());
                    var correoResult = await EnviarCorreo(correo);
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Ocurrió un problema al registrar su solicitud.");
                return result;
            }
            //var result = await _unitOfWork.ObtenerSolicitudCert(CertificadoMapper.Map(new CertificadoModel()
            //{
            //    idSolicitud = resultSolicitud
            //}));
            //CertificadoModel solicitud = CertificadoMapper.Map(result.FirstOrDefault());

            //var correo = PrepararCorreo(solicitud, request);
            //var correoResult = await EnviarCorreo(correo);

            var responseSolicitud = new Models.Certificado.CertificadoResponse()
            {
                idSolicitud = _encryptionServerSecurity.Encrypt(idSolicitud.ToString()),
                estado = estadoEstudiante.ToString(),
                idPersona = ReactEncryptationSecurity.Encrypt(idPersonaEstudiante.ToString())
            };

            result.Success = true;
            result.Data = ReactEncryptationSecurity.Encrypt(JsonConvert.SerializeObject(responseSolicitud));
            result.Messages.Add("Solicitud registrada exitosamente.");
            return result;

        }

        public async Task<StatusResponse> ValidarDatosReniec(string nroDocumento, string idNivel)
        {
            var response = new StatusResponse();
            var personaModel = new Models.Certificado.PersonaModel();
            try
            {

                var siagie2 = _encryptionServerSecurity.Decrypt<string>(
                            ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                            , "");

                var estudianteRequest = new BusinessLogic.Models.Certificado.EstudianteRequest2()
                {
                    codEstudiante = nroDocumento,
                    idNivel = ReactEncryptationSecurity.Decrypt<string>(idNivel, "00")
                };

                var estudianteSucess = await _siagieService
                .GetServiceByQueryAndToken<StatusResponse, BusinessLogic.Models.Certificado.EstudianteRequest2>(siagie2, "estudiantecodigo", estudianteRequest);

                if (estudianteSucess.Data != null)
                {
                    var responseEstudiante = JsonConvert
                        .DeserializeObject<List<BusinessLogic.Models.Siagie.EstudianteMatriculaPorCodigoResponse>>(estudianteSucess.Data.ToString())
                        .First();



                    bool flagNivel = false;
                    if (estudianteRequest.idNivel == responseEstudiante.idNivel)
                    {
                        flagNivel = true;
                    }
                    else
                    {
                        flagNivel = false;
                    }

                    personaModel.numDoc = responseEstudiante.numeroDocumento.Trim();
                    personaModel.apellidoMaterno = responseEstudiante.apellidoMaterno.Trim();
                    personaModel.apellidoPaterno = responseEstudiante.apellidoPaterno.Trim();
                    personaModel.nombres = responseEstudiante.nombres.Trim();
                    personaModel.fecNacimiento = responseEstudiante.fecNacimiento.Trim().Substring(0, 10);
                    personaModel.nombrePadre = null;
                    personaModel.nombreMadre = null;
                    personaModel.dptoDomicilio = null;
                    personaModel.ubigeoDomicilio = responseEstudiante.ubigeoDomicilio.Trim();
                    personaModel.provDomicilio = null;
                    personaModel.distDomicilio = null;
                    personaModel.ultimoAnio = responseEstudiante.ultimoAnio;
                    personaModel.idPersona = responseEstudiante.idPersona;
                    personaModel.idModalidad = responseEstudiante.idModalidad;
                    personaModel.codModular = responseEstudiante.codModular;
                    personaModel.idNivel = responseEstudiante.idNivel;
                    personaModel.IdemNivel = flagNivel;
                    personaModel.idGrado = responseEstudiante.idGrado;
                    personaModel.dscGrado = responseEstudiante.dscGrado;
                    response.Data = personaModel;
                    response.Success = true;
                    response.Messages.Add("Consulta exitosa SIAGIE.");
                }
                else
                {

                    var consultaPersona = await _reniecService.ReniecConsultarPersona(nroDocumento);

                    personaModel.numDoc = consultaPersona.numDoc;
                    personaModel.apellidoMaterno = consultaPersona.apellidoMaterno.Trim();
                    personaModel.apellidoPaterno = consultaPersona.apellidoPaterno.Trim();
                    personaModel.nombres = consultaPersona.nombres.Trim();
                    personaModel.fecNacimiento = consultaPersona.fecNacimiento.Trim().Substring(0, 10);
                    personaModel.nombrePadre = consultaPersona.nombrePadre.Trim();
                    personaModel.nombreMadre = consultaPersona.nombreMadre.Trim();
                    personaModel.dptoDomicilio = consultaPersona.dptoDomicilio.Trim();
                    personaModel.ubigeoDomicilio = consultaPersona.ubigeoDomicilio.Trim();
                    personaModel.provDomicilio = consultaPersona.provDomicilio;
                    personaModel.distDomicilio = consultaPersona.distDomicilio;
                    personaModel.ultimoAnio = 0;
                    /*personaModel.idPersona = responseEstudiante.idPersona;
                    personaModel.idModalidad = responseEstudiante.idModalidad;
                    personaModel.codModular = responseEstudiante.codModular;
                    personaModel.idNivel = responseEstudiante.idNivel;
                    personaModel.IdemNivel = flagNivel;
                    personaModel.idGrado = responseEstudiante.idGrado;
                    personaModel.dscGrado = responseEstudiante.dscGrado;*/


                    if (consultaPersona != null)
                    {
                        response.Data = personaModel;
                        response.Success = true;
                        response.Messages.Add("Consulta exitosa RENIEC.");
                    }
                    else
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Messages.Add("No se encontraron datos de la Persona en RENIEC.");
                    }
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Messages.Add("Ocurrió un error al consultar datos del usuario.");
            }

            return response;
        }

        public async Task<int> InsertUpdateInstitucion(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var model = JsonConvert.DeserializeObject<Models.Certificado.IEModel>(desencriptarObjeto);

            var rows = default(int);

            try
            {
                model.codigoModular = model.codigoModular;
                model.anexo = model.anexo;
                model.centroEducativo = model.centroEducativo;
                model.nivel = ReactEncryptationSecurity.Decrypt<string>(model.nivel, "00");
                model.estado = model.estado;

                _unitOfWork.BeginTransaction();

                rows = await _unitOfWork.InsertUpdateInstitucion(Mappers.Certificado.IEMapper.Map(new Models.Certificado.IEModel()
                {
                    codigoModular = model.codigoModular,
                    anexo = model.anexo,
                    centroEducativo = model.centroEducativo,
                    nivel = model.nivel,
                    estado = model.estado
                }));

                if (rows > 0)
                {
                    _unitOfWork.Commit();
                }
                else
                {
                    _unitOfWork.Rollback();
                }
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
            }

            return rows;
        }

        public async Task<StatusResponse> PostInstitucion(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var result = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var model = JsonConvert.DeserializeObject<Models.Certificado.IEModel>(desencriptarObjeto);

            try
            {
                var institucion = await _unitOfWork.PostInstitucion(Mappers.Certificado.IEMapper.Map(model));

                var respuesta = institucion.Select(x=>new { ESTADO = x.ESTADO }).ToList();

                if (institucion != null)
                {

                    result.Success = true;
                    result.Data = ReactEncryptationSecurity.Encrypt(JsonConvert.SerializeObject(respuesta));
                    result.Messages.Add("Solicitud exitosa.");
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Se presentó un inconveniente al validar la información en el sistema.");
                return result;
            }
        }

        #region Roles
        public async Task<StatusResponse> ObtenerUsuariosPorIE(Models.Certificado.ParametroModel objetoEncriptado)
        {
            StatusResponse response = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var model = JsonConvert.DeserializeObject<Models.Certificado.ColegioRequest2>(desencriptarObjeto);

            var idSistema = _configuration.GetSection("SeguridadService:IdSistemaSiagie").Value.ToString();
            var responseSiagie = await _seguridadService.UsuarioPermisoListarSede(model.modular, model.anexo, idSistema);

            if (responseSiagie == null)
            {
                response.Success = false;
                response.Data = null;
                response.Messages.Add("No se obtuvo la relación de usuarios.");
                return response;
            }

            idSistema = _configuration.GetSection("SeguridadService:IdSistemaCertificado").Value.ToString();
            var responseCertificado = await _seguridadService.UsuarioPermisoListarSede(model.modular, model.anexo, idSistema);

            List<Models.Certificado.UsuarioPermisoResponse> result = new List<Models.Certificado.UsuarioPermisoResponse>();

            result = responseSiagie.Select(x => new Models.Certificado.UsuarioPermisoResponse()
            {
                usr_login = x.usr_login,
                id_sistema = x.id_sistema,
                tipo_sede = x.tipo_sede,
                id_sede = x.id_sede,
                id_sede_anx = x.id_sede_anx,
                por_defecto = x.por_defecto,
                nivel = x.nivel,
                cen_edu = x.cen_edu,
                descentralizado_up = x.descentralizado_up,

                fullname = x.fullname,
                rolDescripcion = x.rolDescripcion,
                certificado = responseCertificado.Where(z => z.usr_login == x.usr_login).Count() > 0
            }).ToList();


            response.Success = true;
            response.Data = ReactEncryptationSecurity.Encrypt(JsonConvert.SerializeObject(result));
            response.Messages.Add("Usuarios cargados exitosamente.");
            return response;
        }

        public async Task<StatusResponse> ActualizarUsuariosPorIE(Models.Certificado.ParametroModel objetoEncriptado)
        {
            StatusResponse response = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var request = JsonConvert.DeserializeObject<Models.Certificado.UsuariosRolPermisoRequest>(desencriptarObjeto);

            try
            {
                foreach (Models.Certificado.UsuarioPermisoResponse usuario in request.usuarios)
                {
                    usuario.id_sistema = _configuration.GetSection("SeguridadService:IdSistemaCertificado").Value.ToString();

                    var usuarioPermisoCertificado = await _seguridadService.UsuarioPermisoLeerPorKey(usuario);

                    if (usuarioPermisoCertificado == null)
                    {
                        usuario.id_sistema = _configuration.GetSection("SeguridadService:IdSistemaSiagie").Value.ToString();

                        var usuarioPermisoSiagie = await _seguridadService.UsuarioPermisoLeerPorKey(usuario);

                        var reqUsuarioPermisoSiagie = new Models.Certificado.UsuarioPermisoRequest()
                        {
                            usr_login = usuarioPermisoSiagie.usr_login,
                            id_sistema = _configuration.GetSection("SeguridadService:IdSistemaCertificado").Value.ToString(),
                            tipo_sede = usuarioPermisoSiagie.tipo_sede,
                            id_sede = usuarioPermisoSiagie.id_sede,
                            id_sede_anx = usuarioPermisoSiagie.id_sede_anx,
                            codigo = usuarioPermisoSiagie.codigo,
                            por_defecto = usuarioPermisoSiagie.por_defecto,
                            nivel = usuarioPermisoSiagie.nivel,
                            verificacion = usuarioPermisoSiagie.verificacion,
                            fechaprimeringreso = usuarioPermisoSiagie.fechaprimeringreso,
                            idrol = "032",
                            doc_referencia = usuarioPermisoSiagie.doc_referencia,
                            estado_usuario_permiso = 1,
                            cen_edu = usuarioPermisoSiagie.cen_edu,
                            descentralizado_up = usuarioPermisoSiagie.descentralizado_up,
                            usuario_registro = "CERTIFICADO",
                            fecha_registro = DateTime.Now,
                            usuario_modificador = null,
                            fecha_modificacion = null,

                            id_sistema_id = _configuration.GetSection("SeguridadService:IdSistemaCertificado").Value.ToString()
                        };

                        _seguridadService.UsuarioPermisoInsertar(reqUsuarioPermisoSiagie);
                    }
                    else
                    {
                        var reqUsuarioPermisoCertificado = new Models.Certificado.UsuarioPermisoRequest()
                        {
                            usr_login = usuarioPermisoCertificado.usr_login,
                            id_sistema = usuarioPermisoCertificado.id_sistema,
                            tipo_sede = usuarioPermisoCertificado.tipo_sede,
                            id_sede = usuarioPermisoCertificado.id_sede,
                            id_sede_anx = usuarioPermisoCertificado.id_sede_anx,
                            codigo = usuarioPermisoCertificado.codigo,
                            por_defecto = usuarioPermisoCertificado.por_defecto,
                            nivel = usuarioPermisoCertificado.nivel,
                            verificacion = usuarioPermisoCertificado.verificacion,
                            fechaprimeringreso = usuarioPermisoCertificado.fechaprimeringreso,
                            idrol = usuarioPermisoCertificado.idrol,
                            doc_referencia = usuarioPermisoCertificado.doc_referencia,
                            estado_usuario_permiso = Convert.ToInt16(usuario.certificado ? 1 : 0),
                            cen_edu = usuarioPermisoCertificado.cen_edu,
                            descentralizado_up = usuarioPermisoCertificado.descentralizado_up,
                            usuario_registro = usuarioPermisoCertificado.usuario_registro,
                            fecha_registro = usuarioPermisoCertificado.fecha_registro,
                            usuario_modificador = usuarioPermisoCertificado.usuario_modificador,
                            fecha_modificacion = usuarioPermisoCertificado.fecha_modificacion,

                            id_sistema_id = usuarioPermisoCertificado.id_sistema
                        };

                        _seguridadService.UsuarioPermisoActualizar(reqUsuarioPermisoCertificado);
                    }
                }

                response.Success = true;
                response.Messages.Add("Se actualizaron los usuarios correctamente.");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Messages.Add("Ocurrió un problema durante el proceso de actualización de usuarios.");
            }
            return response;
        }
        #endregion Roles

        public async Task<StatusResponse> ConsultarSolicitudRechazada(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var result = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var model = JsonConvert.DeserializeObject<Models.Certificado.CertificadoModel>(desencriptarObjeto);

            if (model.pageNumber == 0)
            {
                model.pageNumber = 1;
            }

            try
            {
                var certificado = await _unitOfWork.GetSolicitudRechazada(Mappers.Certificado.CertificadoMapper.Map(model));

                certificado.ToList();

                if (certificado != null)
                {
                    var respuesta = certificado.Select(x => new
                    {
                        ID_SOLICITUD = _encryptionServerSecurity.Encrypt(x.ID_SOLICITUD.ToString()),
                        TIPO_DOCUMENTO = x.TIPO_DOCUMENTO,
                        NUMERO_DOCUMENTO = x.NUMERO_DOCUMENTO,
                        NOMBRE_ESTUDIANTE = x.NOMBRE_ESTUDIANTE,
                        APELLIDO_PATERNO = x.APELLIDO_PATERNO,
                        APELLIDO_MATERNO = x.APELLIDO_MATERNO,
                        NOMBRE = x.NOMBRE,
                        FECHA_SOLICITUD = x.FECHA_SOLICITUD,
                        ESTADO_SOLICITUD = x.ESTADO_SOLICITUD,
                        ID_ESTUDIANTE = _encryptionServerSecurity.Encrypt(x.ID_ESTUDIANTE.ToString()),
                        ID_SOLICITANTE = _encryptionServerSecurity.Encrypt(x.ID_SOLICITANTE.ToString()),
                        ID_MOTIVO = _encryptionServerSecurity.Encrypt(x.ID_MOTIVO.ToString()),
                        DSC_MOTIVO = x.DSC_MOTIVO,
                        ID_MODALIDAD = _encryptionServerSecurity.Encrypt(x.ID_MODALIDAD.ToString()),
                        ID_NIVEL = _encryptionServerSecurity.Encrypt(x.ID_NIVEL.ToString()),
                        DSC_NIVEL = x.DSC_NIVEL,
                        ID_GRADO = x.ID_GRADO,
                        DSC_GRADO = x.DSC_GRADO,
                        ID_PERSONA = x.ID_PERSONA,
                        ULTIMO_ANIO = x.ULTIMO_ANIO,
                        TotalRegistros = x.TotalRegistros

                    }).ToList();
                    result.Success = true;
                    result.Data = ReactEncryptationSecurity.Encrypt(JsonConvert.SerializeObject(respuesta));
                    result.Messages.Add("Solicitud exitosa.");
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Se presentó un inconveniente al validar la información en el sistema.");
                return result;
            }
        }

        public async Task<StatusResponse> ConsultarCertificadosEmitidos(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var result = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var model = JsonConvert.DeserializeObject<Models.Certificado.CertificadoModel>(desencriptarObjeto);

            if (model.pageNumber == 0)
            {
                model.pageNumber = 1;
            }

            try
            {
                var certificado = await _unitOfWork.GetCertificadosEmitidos(Mappers.Certificado.CertificadoMapper.Map(model));

                certificado.ToList();

                if (certificado != null)
                {
                    var respuesta = certificado.Select(x => new
                    {
                        ID_SOLICITUD = _encryptionServerSecurity.Encrypt(x.ID_SOLICITUD.ToString()),
                        TIPO_DOCUMENTO = x.TIPO_DOCUMENTO,
                        NUMERO_DOCUMENTO = x.NUMERO_DOCUMENTO,
                        NOMBRE_ESTUDIANTE = x.NOMBRE_ESTUDIANTE,
                        APELLIDO_PATERNO = x.APELLIDO_PATERNO,
                        APELLIDO_MATERNO = x.APELLIDO_MATERNO,
                        NOMBRE = x.NOMBRE,
                        FECHA_SOLICITUD = x.FECHA_SOLICITUD,
                        ESTADO_SOLICITUD = x.ESTADO_SOLICITUD,
                        ID_ESTUDIANTE = _encryptionServerSecurity.Encrypt(x.ID_ESTUDIANTE.ToString()),
                        ID_SOLICITANTE = _encryptionServerSecurity.Encrypt(x.ID_SOLICITANTE.ToString()),
                        ID_MOTIVO = _encryptionServerSecurity.Encrypt(x.ID_MOTIVO.ToString()),
                        DSC_MOTIVO = x.DSC_MOTIVO,
                        ID_MODALIDAD = _encryptionServerSecurity.Encrypt(x.ID_MODALIDAD.ToString()),
                        ID_NIVEL = _encryptionServerSecurity.Encrypt(x.ID_NIVEL.ToString()),
                        DSC_NIVEL = x.DSC_NIVEL,
                        ID_GRADO = x.ID_GRADO,
                        DSC_GRADO = x.DSC_GRADO,
                        ID_PERSONA = _encryptionServerSecurity.Encrypt(x.ID_PERSONA.ToString()),
                        ULTIMO_ANIO = x.ULTIMO_ANIO,
                        CODIGO_VIRTUAL = x.CODIGO_VIRTUAL,
                        CODIGO_MODULAR = x.CODIGO_MODULAR,
                        ANEXO = x.ANEXO,
                        TotalRegistros = x.TotalRegistros

                    }).ToList();

                    result.Success = true;
                    result.Data = ReactEncryptationSecurity.Encrypt(JsonConvert.SerializeObject(respuesta));
                    result.Messages.Add("Solicitud exitosa.");
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Se presentó un inconveniente al validar la información en el sistema.");
                return result;
            }
        }

        public async Task<StatusResponse> ConsultarSolicitudPendiente(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var result = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var model = JsonConvert.DeserializeObject<Models.Certificado.CertificadoModel>(desencriptarObjeto);

            List<Entities.Certificado.SolicitudExtend> listaSolicitudes = new List<Entities.Certificado.SolicitudExtend>();

            if (model.pageNumber == 0)
            {
                model.pageNumber = 1;
            }

            try
            {
                var estadoInformacion = 0;
                var siagie = _encryptionServerSecurity.Decrypt<string>(
                    ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                    , "");

                var certificado = await _unitOfWork.GetSolicitudesPendientes(Mappers.Certificado.CertificadoMapper.Map(model));
                var certificadoModelo = certificado.Select(x => new
                {
                    idSolicitud = _encryptionServerSecurity.Encrypt(x.ID_SOLICITUD.ToString()),
                    idTipoDocumento = x.ID_TIPO_DOCUMENTO,
                    tipoDocumento = x.TIPO_DOCUMENTO,
                    numeroDocumento = x.NUMERO_DOCUMENTO,
                    nombreEstudiante = x.NOMBRE_ESTUDIANTE,
                    apellidoPaterno = x.APELLIDO_PATERNO,
                    apellidoMaterno = x.APELLIDO_MATERNO,
                    nombre = x.NOMBRE,
                    fechaSolicitud = x.FECHA_SOLICITUD,
                    estadoSolicitud = x.ESTADO_SOLICITUD,
                    idEstudiante = ReactEncryptationSecurity.Encrypt(x.ID_ESTUDIANTE.ToString()),
                    idSolicitante = ReactEncryptationSecurity.Encrypt(x.ID_SOLICITANTE.ToString()),
                    idMotivo = _encryptionServerSecurity.Encrypt(x.ID_MOTIVO.ToString()),
                    dscMotivo = x.DSC_MOTIVO,
                    idModalidad = ReactEncryptationSecurity.Encrypt(x.ID_MODALIDAD.ToString()),
                    idNivel = ReactEncryptationSecurity.Encrypt(x.ID_NIVEL.ToString()),
                    dscNivel = x.DSC_NIVEL,
                    idGrado = x.ID_GRADO,
                    dscGrado = x.DSC_GRADO,
                    idPersona = ReactEncryptationSecurity.Encrypt(x.ID_PERSONA.ToString()),
                    ultimoAnio = x.ULTIMO_ANIO,
                    dscEstadoSolicitud = x.DSC_ESTADO_SOLICITUD,
                    totalRegistros = x.TotalRegistros,
                    estadoInformacion = x.ESTADO_INFORMACION,
                    estadoEstudiante = x.ESTADO_ESTUDIANTE,
                    descripcionEstudiante = x.DSC_ESTADO_ESTUDIANTE,
                    ciclo = x.CICLO
                }).ToList();
                result.Success = true;
                result.Data = ReactEncryptationSecurity.Encrypt(JsonConvert.SerializeObject(certificadoModelo.ToList()));
                
                result.Messages.Add("Solicitud exitosa.");
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Se presentó un inconveniente al validar la información en el sistema.");
                return result;
            }
        }

        public async Task<StatusResponse> InsertDatosAcademicos(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var result = new StatusResponse();
            var rows = default(int);
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var request = JsonConvert.DeserializeObject<Models.Certificado.DatosAcademicosModel>(desencriptarObjeto);
            int IdSolicitud = _encryptionServerSecurity.Decrypt<int>(request.codigoSolicitud, 0);
            var siagie = _encryptionServerSecurity.Decrypt<string>(ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", ""), "");

            Entities.Certificado.SolicitudExtend certificadoEntity = new Entities.Certificado.SolicitudExtend();
            certificadoEntity.ID_SOLICITUD = IdSolicitud;
            certificadoEntity.ESTADO_SOLICITUD = "";

            try
            {
                _unitOfWork.BeginTransaction();

                int resultadoNotas = 0;
                int resultadoGrados = 0;
                int resultadoObservaciones = 0;
                var lstSolicitudes = await _unitOfWork.GetSolicitudCertificado(certificadoEntity);
                var entidadSolicitud = lstSolicitudes.FirstOrDefault();
                var listaNotasPorSolicitud = await _unitOfWork.ObtenerNotasPorSolicitud(IdSolicitud);
                var listaGradosPorSolicitud = await _unitOfWork.ObtenerGradosPorSolicitud(IdSolicitud);

                var EstudianteModalidadNivel = new Models.Certificado.EstudianteModalidadNivelRequest2
                {
                    idPersona = entidadSolicitud == null ? "1" : (entidadSolicitud.ID_PERSONA == 0 ? "1" : entidadSolicitud.ID_PERSONA.ToString()),
                    idModalidad = entidadSolicitud.ID_MODALIDAD,
                    idNivel = entidadSolicitud.ID_NIVEL
                };

                var listaGradosSiagie = await SiagieGradosEstudiante(siagie, EstudianteModalidadNivel);

                var listaNotasSiagie = await SiagieNotasEstudiante(siagie, EstudianteModalidadNivel);

                var grados = (from gradoRequest in request.grados
                              join gradoSiagie in listaGradosSiagie
                              on gradoRequest.idGrado equals gradoSiagie.idGrado
                              select new Models.Certificado.GradoCertificadoModel
                              {
                                  idConstanciaGrado = gradoRequest.idConstanciaGrado,
                                  idGrado = gradoSiagie.estado == 1 ? gradoSiagie.idGrado : gradoRequest.idGrado,
                                  idSolicitud = gradoRequest.idSolicitud,
                                  dscGrado = gradoSiagie.estado == 1 ? gradoSiagie.dscGrado : gradoRequest.dscGrado,
                                  codMod = gradoSiagie.estado == 1 ? gradoSiagie.codMod : gradoRequest.codMod,
                                  anexo = gradoSiagie.estado == 1 ? gradoSiagie.anexo : gradoRequest.anexo,
                                  corrEstadistica = gradoSiagie.estado == 1 ? gradoSiagie.corrEstadistica : gradoRequest.corrEstadistica,
                                  estado = gradoSiagie.estado == 1 ? gradoSiagie.estado : 0,
                                  situacionFinal = gradoSiagie.estado == 1 ? gradoSiagie.situacionFinal : gradoRequest.situacionFinal,
                                  idAnio = gradoSiagie.estado == 1 ? gradoSiagie.idAnio : gradoRequest.idAnio,
                                  ciclo = entidadSolicitud.ID_MODALIDAD != "03" ? 0 : gradoRequest.ciclo
                              }).ToList();

                if (grados == null || grados.Count() == 0)
                {
                    grados = listaGradosSiagie;
                }

                foreach (var item in request.notas)
                {
                    if (item.IdGrado.Trim() == entidadSolicitud.ID_GRADO.Trim())
                    {
                        item.CodigoModular = entidadSolicitud.CODIGO_MODULAR;
                        item.Anexo = entidadSolicitud.ANEXO;
                        item.DscGrado = entidadSolicitud.DSC_GRADO;
                    }
                    item.IdNivel = ReactEncryptationSecurity.Decrypt<string>(item.IdNivel, "00");

                    item.IdSolicitud = IdSolicitud;

                    if (listaNotasPorSolicitud != null)
                    {
                        if (listaNotasPorSolicitud.Count() > 0)
                        {
                            var notaPorSolicitud = listaNotasPorSolicitud
                                                   .Where(x => x.ID_SOLICITUD == IdSolicitud
                                                       //&& x.ID_ANIO == item.IdAnio
                                                       && x.ID_TIPO_AREA == item.IdTipoArea
                                                       && x.ID_AREA.Trim() == item.IdArea
                                                       && x.ID_GRADO == item.IdGrado).FirstOrDefault();

                            if (notaPorSolicitud != null)
                            {
                                item.IdConstanciaNota = notaPorSolicitud.ID_CONSTANCIA_NOTA;
                                item.Activo = true;
                            }
                        }
                    }

                    if (item.Estado == "1")
                    {
                        if (listaNotasSiagie != null)
                        {
                            var notaSiagie = listaNotasSiagie
                                            .Where(x => x.idAnio == item.IdAnio && x.idGrado == item.IdGrado
                                                && x.codMod == item.CodigoModular && x.anexo == item.Anexo
                                                && x.idNivel.Trim() == item.IdNivel.Trim() && x.idArea.TrimEnd() == item.IdArea.Trim()
                                                && x.idTipoArea.Trim() == item.IdTipoArea.Trim()).FirstOrDefault();
                            if (notaSiagie == null)
                            {
                                item.NotaFinal = "";
                            }
                            else
                            {
                                item.NotaFinal = notaSiagie.notaFinalArea;
                            }

                        }
                    }
                    else
                    {
                        item.Estado = "0";
                    }
                    rows = await _unitOfWork.InsertarNotaCertificado(Mappers.Certificado.NotasMapper.Map(item));
                    if (rows > 0)
                    {
                        resultadoNotas++;
                    }
                }

                if (resultadoNotas != request.notas.Length)
                {
                    _unitOfWork.Rollback();
                    result.Success = false;
                    result.Data = 0;
                    result.Messages.Add("Ocurrió un problema al registrar las notas académicas.");
                    return result;
                }

                foreach (var item in grados)
                {

                    item.idSolicitud = IdSolicitud;

                    if (listaGradosPorSolicitud != null)
                    {
                        if (listaGradosPorSolicitud.Count() > 0)
                        {
                            var gradoPorSolicitud = listaGradosPorSolicitud
                                                    .Where(x => x.ID_SOLICITUD == IdSolicitud && x.ID_GRADO == item.idGrado
                                                        && x.CORR_ESTADISTICA == item.corrEstadistica).FirstOrDefault();

                            item.idConstanciaGrado = gradoPorSolicitud.ID_CONSTANCIA_GRADO;
                        }
                    }
                    rows = await _unitOfWork.InsertarGradoCertificado(Mappers.Certificado.GradoCertificadoMapper.Map(item));
                    if (rows > 0)
                    {
                        resultadoGrados++;
                    }
                }

                if (resultadoGrados != request.grados.Length)
                {
                    _unitOfWork.Rollback();
                    result.Success = false;
                    result.Data = 0;
                    result.Messages.Add("Ocurrió un problema al registrar los grados.");
                    return result;
                }

                var observacionPorSolicitud = await _unitOfWork.ObtenerCertificadoObservacionesValidadas(IdSolicitud);

                foreach (var item in observacionPorSolicitud.ToList())
                {
                    var registroObservaciones = request.observaciones.Where(x => x.idNivel == item.ID_NIVEL && x.idAnio == item.ID_ANIO && x.resolucion == item.RESOLUCION).FirstOrDefault();
                    if (registroObservaciones == null)
                    {
                        rows = await _unitOfWork.DeleteObservacionCertificado(item.ID_CERTIFICADO_OBSERVACION);
                        if (rows > 0)
                        {

                        }
                    }

                }

                if (request.observaciones.Length > 0)
                {
                    foreach (var item in observacionPorSolicitud.ToList())
                    {
                        var registroObservaciones = request.observaciones.Where(x => x.idNivel == item.ID_NIVEL && x.idAnio == item.ID_ANIO && x.resolucion == item.RESOLUCION).FirstOrDefault();
                        if (registroObservaciones == null)
                        {
                            rows = await _unitOfWork.DeleteObservacionCertificado(item.ID_CERTIFICADO_OBSERVACION);
                            if (rows > 0)
                            {

                            }
                        }

                    }

                    foreach (var item in request.observaciones)
                    {
                        item.idSolicitud = IdSolicitud;
                        item.idNivel = ReactEncryptationSecurity.Decrypt<string>(item.idNivel, "00");

                        var registroObservaciones = observacionPorSolicitud.Where(x => x.ID_NIVEL == item.idNivel && x.ID_ANIO == item.idAnio && x.RESOLUCION == item.resolucion).FirstOrDefault();
                        if (registroObservaciones == null)
                        {
                            rows = await _unitOfWork.InsertarObservacionCertificado(Mappers.Certificado.ObservacionMapper.Map(item));
                            if (rows > 0)
                            {
                                resultadoObservaciones++;
                            }
                        }
                        else
                        {
                            rows = await _unitOfWork.InsertarObservacionCertificado(registroObservaciones);
                            if (rows > 0)
                            {
                                resultadoObservaciones++;
                            }
                        }


                    }

                    if (resultadoObservaciones != request.observaciones.Length)
                    {
                        _unitOfWork.Rollback();
                        result.Success = false;
                        result.Data = 0;
                        result.Messages.Add("Ocurrió un problema al registrar los grados.");
                        return result;
                    }
                }


                if (request.observaciones.Length == 0)
                {
                    if (observacionPorSolicitud.Count() > 0)
                    {   //delete observaciones for idsolicitud
                        var rowsDelAll = await _unitOfWork.DeleteObservacionCertificadoAll(IdSolicitud);
                    }
                }

                if (request.eliminados != null)
                {
                    if (request.eliminados.Length > 0)
                    {
                        var listaNotasxSolicitud = await _unitOfWork.ObtenerNotasPorSolicitud(IdSolicitud);

                        for (int i = 0; i < request.eliminados.Length; i++)
                        {
                            var noEliminados = request.notas.Where(x => x.IdArea == request.eliminados[i].IdArea && x.IdTipoArea == request.eliminados[i].IdTipoArea).ToList();
                            if (noEliminados.Count() == 0)
                            {
                                var notasPorInactivar = listaNotasxSolicitud.Where(x => x.ID_AREA == request.eliminados[i].IdArea && x.ACTIVO).ToList();
                                if (notasPorInactivar.Count() > 0)
                                {
                                    foreach (var notasUpdate in notasPorInactivar)
                                    {
                                        var inactivarNota = await _unitOfWork.InactivarNota(notasUpdate.ID_CONSTANCIA_NOTA, notasUpdate.ID_SOLICITUD);
                                    }
                                }
                            }

                        }
                    }
                }




                int numRows = await _unitOfWork.UpdateEstadoEstudianteCertificado(IdSolicitud, "4");

                if (numRows > 0)
                {
                    _unitOfWork.Commit();
                    result.Success = true;
                    result.Data = 1;
                    result.Messages.Add("Los Datos Academicos fueron registrados correctamente");
                    return result;
                }
                else
                {
                    _unitOfWork.Rollback();
                    result.Success = false;
                    result.Data = 0;
                    result.Messages.Add("Ocurrió un problema al registrar los datos academicos.");
                    return result;
                }

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                result.Success = false;
                result.Data = 0;
                result.Messages.Add("Se presentó un inconveniente al validar la información en el sistema.");
                return result;
            }
        }

        private Models.Certificado.NivelResponse ConsultarNivelEducativo(string IdNivel)
        {

            string DescripcionNivel = "";

            switch (IdNivel)
            {
                case "A1":
                    DescripcionNivel = "Inicial  Cuna";
                    break;
                case "A2":
                    DescripcionNivel = "Inicial - Jardín";
                    break;
                case "A3":
                    DescripcionNivel = "Inicial - Cuna-Jardín";
                    break;
                case "A5":
                    DescripcionNivel = "Inicial  Prog No Escolariz";
                    break;
                case "B0":
                    DescripcionNivel = "Primaria";
                    break;
                case "C0":
                    DescripcionNivel = "Primaria de Adultos";
                    break;
                case "D0":
                    DescripcionNivel = "Educación Básica Alternativa";
                    break;
                case "D1":
                    DescripcionNivel = "EBA - Inicial e Intermedio";
                    break;
                case "D2":
                    DescripcionNivel = "Básica Alternativa - Avanzado";
                    break;
                case "E0":
                    DescripcionNivel = "Educación Básica Especial";
                    break;
                case "E1":
                    DescripcionNivel = "Básica Especial - Inicial";
                    break;
                case "E2":
                    DescripcionNivel = "Básica Especial - Primaria";
                    break;
                case "F0":
                    DescripcionNivel = "Secundaria";
                    break;
                case "G0":
                    DescripcionNivel = "Secundaria de Adultos";
                    break;
                default:
                    DescripcionNivel = "";
                    break;
            }


            var nivel_educativo = new Models.Certificado.NivelResponse
            {
                idNivel = IdNivel,
                descripcionNivel = DescripcionNivel
            };

            return nivel_educativo;
        }


        public async Task<StatusResponse> ValdarNotasPendientes(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var result = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var model = JsonConvert.DeserializeObject<Models.Certificado.CertificadoModel>(desencriptarObjeto);

            var siagie = _encryptionServerSecurity.Decrypt<string>(
                   ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                   , "");
            var mensaje = "";

            try
            {
                var decrypted = new Models.Certificado.EstudianteModalidadNivelRequest2()
                {
                    idPersona = ReactEncryptationSecurity.Decrypt<string>(model.CodigoPersona, "00"),
                    idModalidad = model.IdModalidad.Length > 12 ? ReactEncryptationSecurity.Decrypt<string>(model.IdModalidad, "00") : model.IdModalidad.ToString(),
                    idNivel = ReactEncryptationSecurity.Decrypt<string>(model.IdNivel, "00"),
                    idSistema = "1"
                };

                //var numeroNotasPendientes = await _siagieService.GetServiceByQueryAndToken<StatusResponse, Models.Constancia.EstudianteModalidadNivelRequest>(siagie, "notaspendientes", decrypted);
                var numeroNotasPendientes = await _siagieService.GetServiceByQueryAndToken<StatusResponse, Models.Certificado.EstudianteModalidadNivelRequest2>(siagie, "pdf/grados", decrypted);

                if (numeroNotasPendientes == null)
                {
                    result.Success = false;
                    result.Data = ReactEncryptationSecurity.Encrypt("0");
                    result.Messages.Add("El estudiante no cuenta con un registro en SIAGIE. Se requiere ingresar notas.");//Registrar Notas
                    return result;
                }
                else
                {
                    if (numeroNotasPendientes.Data == null)
                    {
                        result.Success = false;
                        result.Data = ReactEncryptationSecurity.Encrypt("0");
                        result.Messages.Add("El estudiante no cuenta con un registro en SIAGIE. Se requiere ingresar notas.");//Registrar Notas
                        return result;
                    }
                    else
                    {
                        var resultado = JsonConvert
                                   .DeserializeObject<List<Models.Siagie.EstudianteModalidadNivelResponse>>(numeroNotasPendientes.Data.ToString())
                                   .ToList();

                        var conNotas = resultado.Where(x => x.estado == 1).ToList();
                        var faltaNotas = resultado.Where(x => x.estado == 0).ToList();
                        //var faltaNotasRegularizar = resultado.Where(x => x.registroNota == "3").ToList();

                        if (resultado.Count() == faltaNotas.Count())
                        {
                            result.Success = true;
                            result.Data = ReactEncryptationSecurity.Encrypt("1");
                            result.Messages.Add("El estudiante no cuenta con las notas completas puede continuar para registrar las notas en el Certificado de Estudios, sino guardar la solicitud para el registro posterior de notas en el Certificado de Estudios.");//Registrar notas


                        }
                        if (conNotas.Count() < resultado.Count() && faltaNotas.Count() < resultado.Count())
                        {
                            result.Success = true;
                            result.Data = ReactEncryptationSecurity.Encrypt("2");
                            result.Messages.Add("El estudiante debe regularizar su información con la Institución Educativa correspondiente.");//Solo mensaje con el boton "OK".

                        }
                        if (resultado.Count() == conNotas.Count())
                        {
                            result.Success = true;
                            result.Data = ReactEncryptationSecurity.Encrypt("3");
                            result.Messages.Add("El estudiante cuenta con información completa en Siagie. Puede realizar su emisión de Certificado de Estudios.");//Generar certificado

                        }
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Se presentó un inconveniente al validar la información en el sistema.");
                return result;
            }
        }

        public async Task<StatusResponse> EstudiantesPorAnioGradoSeccion(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var result = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var request = JsonConvert.DeserializeObject<Models.Certificado.EstudianteAnioGradoSeccionRequest>(desencriptarObjeto);

            try
            {
                var siagie = _encryptionServerSecurity.Decrypt<string>(
                   ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                   , "");

                var decrypted = new
                {
                    token = siagie,
                    codMod = request.codMod,
                    idAnio = request.idAnio,
                    idNivel = ReactEncryptationSecurity.Decrypt<string>(request.idNivel, "00"),
                    idGrado = request.idGrado,
                    idSeccion = request.idSeccion,
                    anexo = request.anexo,
                    numeroDocumento = request.numeroDocumento,
                    nombresEstudiante = request.nombresEstudiante
                };

                var statusResponse = await _siagieService
                        .GetServiceByQueryAndToken<StatusResponse,
                        object>(decrypted.token, "estudiantesporaniogradoseccion", decrypted);

                //Información de notas del estudiante en Siagie: FAIL?
                if (!statusResponse.Success)
                {
                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add("No se logró obtener los estudiantes actuales");
                    return result;
                }

                //Información de notas del estudiante en Siagie: OK
                var objEstudianteAnioGradoSeccion = JsonConvert
                    .DeserializeObject<List<Models.Certificado.EstudianteAnioGradoSeccion>>(statusResponse.Data.ToString())
                    .ToList();

                foreach (var item in objEstudianteAnioGradoSeccion)
                {
                    var decrypted2 = new Models.Certificado.EstudianteModalidadNivelRequest2()
                    {
                        idPersona = item.idPersona.ToString(),
                        idModalidad = item.idModalidad,
                        idNivel = item.idNivel,
                        idSistema = "1"
                    };

                    var solicitudesPendientes = await _unitOfWork.ConsultarSolicitudPorPersona(item.idPersona);
                    if (solicitudesPendientes>0)
                    {
                        item.estado = 1;
                    }
                    else
                    {
                        item.estado = 0;
                        var numeroNotasPendientes = await _siagieService.GetServiceByQueryAndToken<StatusResponse, Models.Certificado.EstudianteModalidadNivelRequest2>(siagie, "pdf/grados", decrypted2);
                        if (numeroNotasPendientes == null)
                        {
                            item.descEstadoSolicitud = "SIN INFORMACIÓN";
                            item.estadoInformacion = 1;
                        }
                        else
                        {
                            if (numeroNotasPendientes.Data == null)
                            {
                                item.descEstadoSolicitud = "SIN INFORMACIÓN";
                                item.estadoInformacion = 1;
                            }
                            else
                            {
                                var resultado = JsonConvert
                                   .DeserializeObject<List<Models.Siagie.EstudianteModalidadNivelResponse>>(numeroNotasPendientes.Data.ToString())
                                   .ToList();
                                //var Informacion = resultado.Where(x => x.idAnio == 0).ToList();

                                //var conNotas = resultado.Where(x => x.registroNota == "1");
                                //var faltaNotas = resultado.Where(x => x.registroNota == "2");
                                //var faltaNotasRegularizar = resultado.Where(x => x.registroNota == "3");
                                var sinNotas = resultado.Where(x => x.estado == 0);
                                var conNotas = resultado.Where(x => x.estado == 1);
                                //if (faltaNotas.Count() == 0 && faltaNotasRegularizar.Count() == 0 && conNotas.Count() > 0)
                                if (resultado.Count() == sinNotas.Count())
                                {
                                    item.descEstadoSolicitud = "SIN INFORMACION";
                                    item.estadoInformacion = 1;
                                }
                                else if (resultado.Count() == conNotas.Count())
                                {
                                    item.descEstadoSolicitud = "INFORMACION COMPLETA";
                                    item.estadoInformacion = 3;
                                }
                                else                          //if (faltaNotas.Count() > 0)
                                {
                                    item.descEstadoSolicitud = "INFORMACIÓN INCOMPLETA";
                                    item.estadoInformacion = 2;
                                }
                            }
                        }
                    }

                    

                    item.codPersona = _encryptionServerSecurity.Encrypt(item.idPersona.ToString());
                    item.idNivel = ReactEncryptationSecurity.Encrypt(item.idNivel);
                    item.idModalidad = ReactEncryptationSecurity.Encrypt(item.idModalidad);
                    item.idPersona = 0;
                }

                var respuesta = objEstudianteAnioGradoSeccion.Where(x => x.estado == 0);

                result.Success = true;
                result.Data = ReactEncryptationSecurity.Encrypt(JsonConvert.SerializeObject(respuesta));
                result.Messages.Add("Estudiantes actuales, conforme");
                return result;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Se presentó un inconveniente al procesar su solicitud.");
                return result;
            }
        }

        public async Task<byte[]> VistaPreviaPDFCertificado(Models.Certificado.ParametroModel objetoEncriptado)
        {
            try
            {
                var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
                var request = JsonConvert.DeserializeObject<Models.Certificado.EstudianteModalidadNivelPersonaRequest2>(desencriptarObjeto);


                var siagie = _encryptionServerSecurity.Decrypt<string>(
                    ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                    , "");

                var estudianteConstancia = await SiagieEstudiante(siagie, request);
                if (estudianteConstancia == null)
                {
                    return null;
                }
                estudianteConstancia.solicitud.estadoSolicitud = "1";
               
                var observacionRequest = new Models.Certificado.EstudianteModalidadNivelRequest2
                {
                    idPersona = request.idPersona.Length < 12
                    ? request.idPersona
                    : _encryptionServerSecurity.Decrypt<int>(request.idPersona, 0).ToString(),
                    idModalidad = request.idModalidad.Length < 5
                    ? request.idModalidad
                    : ReactEncryptationSecurity.Decrypt<string>(request.idModalidad, "00"),
                    idNivel = ReactEncryptationSecurity.Decrypt<string>(request.idNivel, "00"),
                    idSistema = "1"
                };

                if (estudianteConstancia.solicitud.idSolicitud == 0)
                {//Siagie

                    var observacionesResponse = await _siagieService
                            .GetServiceByQueryAndToken<StatusResponse,
                            Models.Certificado.EstudianteModalidadNivelRequest2>(siagie, "pdf/observaciones", observacionRequest);
                    if (observacionesResponse.Data != null)
                    {
                        estudianteConstancia.observaciones = JsonConvert
                                                           .DeserializeObject<List<Models.Certificado.ObservacionCertificadoModel>>(observacionesResponse.Data.ToString());
                    }


                }
                else
                {//Certificado
                    var resultObservaciones = await _unitOfWork.ObtenerObservacionesCertificadoValidadas(estudianteConstancia.solicitud.idSolicitud);
                    if (resultObservaciones != null)
                    {
                        var observacionesResponse = resultObservaciones.ToList();
                        estudianteConstancia.observaciones = observacionesResponse.Select(ObservacionCertificadoMapper.Map).ToList();
                    }

                }

                return PDFCertificadoInit(true, estudianteConstancia/*, institucionEducativa*/);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private byte[] PDFCertificadoInit(bool vistaPrevia, Models.Certificado.EstudianteCertificado estudianteConstancia/*, InstitucionEducativaPorDreUgelResponse institucionEducativa*/)
        {
            List<Models.Certificado.PDFNotaCertificado> cursoList = OrdenarNotasEstudiante(estudianteConstancia.notas, estudianteConstancia.grados, "001");
            List<Models.Certificado.PDFNotaCertificado> competenciaList = OrdenarNotasEstudiante(estudianteConstancia.notas, estudianteConstancia.grados, "003");
            List<Models.Certificado.PDFNotaCertificado> tallerList = OrdenarNotasEstudiante(estudianteConstancia.notas, estudianteConstancia.grados, "002");
            //string obs = ConcatenarObservaciones(estudianteConstancia.observaciones);
            List<Models.Certificado.ObservacionCertificadoModel> listaObservaciones = estudianteConstancia.observaciones;

            byte[] pdf = PDFCertificado(vistaPrevia, estudianteConstancia, cursoList, competenciaList, tallerList, listaObservaciones/*, institucionEducativa*/).ToArray();

            return pdf;
        }

        private MemoryStream PDFCertificado(bool vistaPrevia, Models.Certificado.EstudianteCertificado estudianteConstancia, List<Models.Certificado.PDFNotaCertificado> cursoList, List<Models.Certificado.PDFNotaCertificado> competenciaList, List<Models.Certificado.PDFNotaCertificado> tallerList, List<Models.Certificado.ObservacionCertificadoModel> obs/*, InstitucionEducativaPorDreUgelResponse institucionEducativa*/)
        {
            /* Variables */
            List<Models.Certificado.GradoCertificadoModel> gradosList = estudianteConstancia.grados;

            string correlativo = String.Format("{0:D8}", estudianteConstancia.solicitud.idSolicitud);
            string hashQRCode = _configuration.GetSection("PDF:QRCodeUrlCertificado").Value + "/validate/" + estudianteConstancia.solicitud.codigoVirtual;
            //string hashQRCode = "http://192.168.210.152:8050/validate/" + estudianteConstancia.solicitud.codigoVirtual;

            int totalGrados = gradosList.Count;
            var gradosCursados = gradosList.Where(w => w.idAnio > 0).Select(x => x.dscGrado).ToArray();
            string gradosConcatenados = string.Join(", ", gradosCursados);

            int cursos = cursoList.Count;
            int competencias = competenciaList.Count;
            int talleres = tallerList.Count; //16

            //string ubigeo = "Lima";
            string fechaSolicitud = estudianteConstancia.solicitud.fechaCreacion.ToString("dd 'de' MMMM 'de' yyyy", CultureInfo.CreateSpecificCulture("es-PE"));
            //string fechaSolicitud = DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy", CultureInfo.CreateSpecificCulture("es-PE"));
            string horaSolicitud = estudianteConstancia.solicitud.fechaCreacion.ToString("HH:mm 'horas'", CultureInfo.CreateSpecificCulture("es-PE"));
            //string horaSolicitud = DateTime.Now.ToString("HH:mm 'horas'", CultureInfo.CreateSpecificCulture("es-PE"));

            using (MemoryStream memoryStream = new MemoryStream())
            using (PdfWriter writer = new PdfWriter(memoryStream))
            using (PdfDocument pdf = new PdfDocument(writer))
            using (Document document2 = new Document(pdf, PageSize.A4, false))
            using (Document document = new Document(pdf, PageSize.A4, false))
            {
                float sizeFont = 7;
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.COURIER);
                PdfFont fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                #region LogoMinedu
                byte[] imgdata = null;

                var PathNotProfile = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images", "logo_minedu.png");
                imgdata = File.ReadAllBytes(PathNotProfile);

                // Load image from disk
                ImageData imageData = ImageDataFactory.Create(imgdata);
                // Create layout image object and provide parameters. Page number = 1
                Image image = new Image(imageData)
                    //.SetAutoScale(true);
                    .ScaleToFit(100, 75)
                    .SetPaddingBottom(25)
                    .SetHorizontalAlignment(HorizontalAlignment.CENTER);
                #endregion LogoMinedu

                //pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, new StartPageEventHandler(document));
                document.SetMargins(18, 18, 89 + 18, 18);

                #region Header
                Table table = new Table(UnitValue.CreatePercentArray(new float[] { 20, 60, 20 }));
                table.SetWidth(UnitValue.CreatePercentValue(100));
                table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table.AddCell(new Cell(4, 1).Add(image).SetBorder(Border.NO_BORDER));
                table.AddCell(PDFMinedu.getCell("MINISTERIO DE EDUCACIÓN", 9, false, 1, 1, TextAlignment.CENTER, null, false));
                table.AddCell(PDFMinedu.getCell(" ", 9, false, 1, 1, TextAlignment.CENTER, null, false));
                table.AddCell(PDFMinedu.getCell("CERTIFICADO OFICIAL DE ESTUDIOS", 10, true, 1, 1, TextAlignment.CENTER, null, false));
                table.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : "CÓDIGO VIRTUAL", 9, false, 1, 1, TextAlignment.CENTER, null, false));
                table.AddCell(PDFMinedu.getCell(estudianteConstancia.solicitud.dscModalidad, 9, false, 1, 1, TextAlignment.CENTER, null, false));
                table.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : estudianteConstancia.solicitud.codigoVirtual, 11, true, 1, 1, TextAlignment.CENTER, null, false));
                table.AddCell(PDFMinedu.getCell((estudianteConstancia.solicitud.idModalidad == "03" ? "CICLO " : "NIVEL ") + (estudianteConstancia.solicitud.idNivel == "D1" ? "INICIAL - INTERMEDIO" : estudianteConstancia.solicitud.dscNivel), 9, false, 1, 1, TextAlignment.CENTER, null, false));
                table.AddCell(PDFMinedu.getCell(" ", 9, false, 1, 1, TextAlignment.CENTER, null, false));
                document.Add(table);
                #endregion Header

                PDFMinedu.addEspacios(document, sizeFont);

                #region SubHeader
                table = new Table(UnitValue.CreatePercentArray(new float[] { 100 }));
                table.SetWidth(UnitValue.CreatePercentValue(100));
                table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                var nivelPDF = estudianteConstancia.solicitud.idNivel == "D1" ? "INICIAL - INTERMEDIO " : estudianteConstancia.solicitud.dscNivel.Trim();
                var modalidadPDF = estudianteConstancia.solicitud.idModalidad == "03" ? ",ciclo " : ", nivel de educación ";
                //table.AddCell(PDFMinedu.getCell("EL MINISTERIO DE EDUCACIÓN DEL PERÚ", 9, true, 1, 1, TextAlignment.CENTER, null, false));
                table.AddCell(PDFMinedu.getCell("Que: " + estudianteConstancia.estudiante.nombres + " " + estudianteConstancia.estudiante.apellidoPaterno + " " + estudianteConstancia.estudiante.apellidoMaterno + " " +
                    "con DNI/Código del estudiante Nº " + estudianteConstancia.estudiante.numeroDocumento.Trim() + ", ha concluido estudios " +
                    "correspondiente(s) a: " + gradosConcatenados + " Grado(s) de " + estudianteConstancia.solicitud.abrModalidad + modalidadPDF +
                    nivelPDF + " con los siguientes resultados, según consta en las actas de evaluación respectivas: ",
                    8, false, 1, 1, TextAlignment.JUSTIFIED, null, false));
                //table.AddCell(PDFMinedu.getCell("Hello World!", 10, false, 1, 1).SetFont(font));
                document.Add(table);
                #endregion SubHeader

                PDFMinedu.addEspacios(document, sizeFont);

                #region Body

                #region Notas_Parametros
                float[] withColumns = new float[totalGrados + 3];

                withColumns[0] = 3;
                withColumns[1] = 25;

                int withGrados = 54 / totalGrados;
                for (int i = 2; i < totalGrados + 3; i++)
                {
                    withColumns[i] = withGrados;
                }
                withColumns[(totalGrados + 3) - 1] = withGrados;
                #endregion Notas_Parametros

                table = new Table(UnitValue.CreatePercentArray(withColumns));
                table.SetWidth(UnitValue.CreatePercentValue(100));
                table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                #region Notas_Header
                table.AddCell(PDFMinedu.getCell("Año Lectivo", sizeFont, false, 1, 3, TextAlignment.LEFT));

                for (int i = 1; i <= totalGrados; i++)
                {

                    string anio = "";
                    if (gradosList[i - 1].ciclo == 0)
                    {
                        anio = gradosList[i - 1].idAnio.ToString();
                    }
                    else
                    {
                        anio = gradosList[i - 1].idAnio.ToString() + "-" + gradosList[i - 1].ciclo.ToString();
                    }

                    table.AddCell(PDFMinedu.getCell(anio.Equals("0") ? "-" : anio, sizeFont, true));
                }

                //table.AddCell(PDFMinedu.getCell("Observaciones", sizeFont, false, 3));
                var gradosCell = estudianteConstancia.solicitud.dscNivel.Contains("INICIAL") ? "Edad" : "Grado";
                table.AddCell(PDFMinedu.getCell(gradosCell, sizeFont, false, 1, 3, TextAlignment.LEFT));

                for (int i = 1; i <= totalGrados; i++)
                {
                    table.AddCell(PDFMinedu.getCell(switchTextoGrado(gradosList[i - 1].dscGrado), sizeFont));
                }

                table.AddCell(PDFMinedu.getCell("Código Modular de I.E.", sizeFont, false, 1, 3, TextAlignment.LEFT));

                for (int i = 1; i <= totalGrados; i++)
                {
                    string codModAnexo = (gradosList[i - 1].idAnio == 0) ? "-" : gradosList[i - 1].codMod + "-" + gradosList[i - 1].anexo;
                    table.AddCell(PDFMinedu.getCell(codModAnexo, sizeFont));
                }
                #endregion Notas_Header

                #region Notas_Cursos
                int rows = cursos;

                table.AddCell(PDFMinedu.getCell("Área Curricular", sizeFont, false, rows));

                for (int x = 1; x <= rows; x++)
                {
                    table.AddCell(PDFMinedu.getCell(cursoList[x - 1].DscArea, sizeFont, false, 1, 2, TextAlignment.LEFT));

                    for (int i = 1; i <= totalGrados; i++)
                    {
                        string nota = cursoList[x - 1].GradoNotas[i - 1].NotaFinalArea;

                        table.AddCell(PDFMinedu.getCell(nota == null ? "-" : nota, sizeFont));
                    }

                    //Adición de observaciones
                    //if (x == 1) table.AddCell(PDFMinedu.getCell(obs, sizeFont, false, cursos + competencias + talleres));
                }
                #endregion Notas_Cursos

                #region Notas_Competencias
                if (competencias > 0)
                {
                    rows = competencias;

                    table.AddCell(PDFMinedu.getCell("Competencias Transversales", sizeFont, false, rows));

                    for (int x = 1; x <= rows; x++)
                    {
                        table.AddCell(PDFMinedu.getCell(competenciaList[x - 1].DscArea, sizeFont, false, 1, 2, TextAlignment.LEFT));

                        for (int i = 1; i <= totalGrados; i++)
                        {
                            //int randomNota = new Random().Next(1, 20);
                            //string nota = (randomNota < 10) ? ("0" + randomNota) : randomNota.ToString();
                            string nota = competenciaList[x - 1].GradoNotas[i - 1].NotaFinalArea;
                            table.AddCell(PDFMinedu.getCell(nota == null ? "-" : nota, sizeFont, false, 1, 1));
                        }
                    }
                }
                #endregion Notas_Competencias

                #region Notas_Talleres
                if (talleres > 0)
                {
                    rows = talleres;

                    table.AddCell(PDFMinedu.getCell("Talleres", sizeFont, false, rows));

                    for (int x = 1; x <= rows; x++)
                    {
                        table.AddCell(PDFMinedu.getCell(tallerList[x - 1].DscArea, sizeFont, false, 1, 2, TextAlignment.LEFT));

                        for (int i = 1; i <= totalGrados; i++)
                        {
                            //int randomNota = new Random().Next(1, 20);
                            //string nota = (randomNota < 10) ? ("0" + randomNota) : randomNota.ToString();
                            string nota = tallerList[x - 1].GradoNotas[i - 1].NotaFinalArea;
                            table.AddCell(PDFMinedu.getCell(nota == null ? "-" : nota, sizeFont, false, 1, 1));
                        }
                    }
                }
                #endregion Notas_Talleres

                #region Notas_Final
                table.AddCell(PDFMinedu.getCell("Situación Final", sizeFont, true, 1, 3));

                for (int i = 1; i <= totalGrados; i++)
                {
                    table.AddCell(PDFMinedu.getCell(gradosList[i - 1].situacionFinal, 6, true));
                }
                #endregion Notas_Final

                document.Add(table);

                #endregion Body

                PDFMinedu.addEspacios(document, sizeFont);

                pdf.AddNewPage(PageSize.A4);
                document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

                #region SegundaPagina

                document.SetMargins(18, 18, 89 + 18, 18);
                #region Header
                Table table2 = new Table(UnitValue.CreatePercentArray(new float[] { 20, 60, 20 }));
                table2.SetWidth(UnitValue.CreatePercentValue(100));
                table2.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table2.AddCell(new Cell(4, 1).Add(image).SetBorder(Border.NO_BORDER));
                table2.AddCell(PDFMinedu.getCell("MINISTERIO DE EDUCACIÓN", 9, false, 1, 1, TextAlignment.CENTER, null, false));
                table2.AddCell(PDFMinedu.getCell(" ", 9, false, 1, 1, TextAlignment.CENTER, null, false));
                table2.AddCell(PDFMinedu.getCell("CERTIFICADO OFICIAL DE ESTUDIOS", 10, true, 1, 1, TextAlignment.CENTER, null, false));
                table2.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : "CÓDIGO VIRTUAL", 9, false, 1, 1, TextAlignment.CENTER, null, false));
                table2.AddCell(PDFMinedu.getCell(estudianteConstancia.solicitud.dscModalidad, 9, false, 1, 1, TextAlignment.CENTER, null, false));
                table2.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : estudianteConstancia.solicitud.codigoVirtual, 11, true, 1, 1, TextAlignment.CENTER, null, false));
                table2.AddCell(PDFMinedu.getCell((estudianteConstancia.solicitud.idModalidad == "03" ? "CICLO " : "NIVEL ") + (estudianteConstancia.solicitud.idNivel == "D1" ? "INICIAL - INTERMEDIO" : estudianteConstancia.solicitud.dscNivel), 9, false, 1, 1, TextAlignment.CENTER, null, false));
                table2.AddCell(PDFMinedu.getCell(" ", 9, false, 1, 1, TextAlignment.CENTER, null, false));
                document.Add(table2);
                #endregion Header

                PDFMinedu.addEspacios(document, 10);

                #region Observaciones
                table2 = new Table(UnitValue.CreatePercentArray(new float[] { 60, 40 }));
                table2.SetWidth(UnitValue.CreatePercentValue(100));
                table2.SetHorizontalAlignment(HorizontalAlignment.CENTER);


                table2.AddCell(PDFMinedu.getCell("", 8, false, 1, 3, TextAlignment.JUSTIFIED, null, false));
                table2.AddCell(PDFMinedu.getCell("", 8, false, 1, 3, TextAlignment.JUSTIFIED, null, false));
                table2.AddCell(PDFMinedu.getCell("Observaciones: ", 9, false, 1, 3, TextAlignment.JUSTIFIED, null, false));
                table2.AddCell(PDFMinedu.getCell("", 8, false, 1, 3, TextAlignment.JUSTIFIED, null, false));

                if (obs != null)
                {
                    if (obs.Count() > 0)
                    {
                        var obsRetiro = obs.Where(x => x.tipoObs == 1).ToList();
                        if (obsRetiro.Count() > 0)
                        {
                            table2.AddCell(PDFMinedu.getCell("Retiro(s):", 7, false, obsRetiro.Count(), 1, TextAlignment.LEFT, null, false));
                            for (int i = 1; i <= obsRetiro.Count(); i++)
                            {
                                table2.AddCell(PDFMinedu.getCell(obsRetiro[i - 1].idAnio + "-" + obsRetiro[i - 1].resolucion + "-" + obsRetiro[i - 1].motivo, 6, true, 1, 1, TextAlignment.JUSTIFIED, null, false));
                            }
                        }
                        else
                        {
                            table2.AddCell(PDFMinedu.getCell("Retiro(s):", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                            table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                            table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                        }

                        var obsTraslado = obs.Where(x => x.tipoObs == 2).ToList();
                        if (obsTraslado.Count() > 0)
                        {
                            table2.AddCell(PDFMinedu.getCell("Traslado(s):", 7, false, obsTraslado.Count(), 1, TextAlignment.LEFT, null, false));
                            for (int i = 1; i <= obsTraslado.Count(); i++)
                            {
                                table2.AddCell(PDFMinedu.getCell(obsTraslado[i - 1].idAnio + "-" + obsTraslado[i - 1].resolucion + "-" + obsTraslado[i - 1].motivo, 6, true, 1, 1, TextAlignment.JUSTIFIED, null, false));
                            }
                        }
                        else
                        {
                            table2.AddCell(PDFMinedu.getCell("Traslado(s):", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                            table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                            table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                        }

                        var obsPrueba = obs.Where(x => x.tipoObs == 3).ToList();
                        if (obsPrueba.Count() > 0)
                        {
                            table2.AddCell(PDFMinedu.getCell("Prueba(s) de Ubicación:", 7, false, obsPrueba.Count(), 1, TextAlignment.LEFT, null, false));
                            for (int i = 1; i <= obsPrueba.Count(); i++)
                            {
                                table2.AddCell(PDFMinedu.getCell(obsPrueba[i - 1].idAnio + "-" + obsPrueba[i - 1].resolucion + "-" + obsPrueba[i - 1].motivo, 6, true, 1, 1, TextAlignment.JUSTIFIED, null, false));
                            }
                        }
                        else
                        {
                            table2.AddCell(PDFMinedu.getCell("Prueba(s) de Ubicación:", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                            table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                            table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                        }

                        var obsValidacion = obs.Where(x => x.tipoObs == 4).ToList();
                        if (obsValidacion.Count() > 0)
                        {
                            table2.AddCell(PDFMinedu.getCell("Revalidación / Convalidación:", 7, false, obsValidacion.Count(), 1, TextAlignment.LEFT, null, false));
                            for (int i = 1; i <= obsValidacion.Count(); i++)
                            {
                                table2.AddCell(PDFMinedu.getCell(obsValidacion[i - 1].idAnio + "-" + obsValidacion[i - 1].resolucion + "-" + obsValidacion[i - 1].motivo, 6, true, 1, 1, TextAlignment.JUSTIFIED, null, false));
                            }
                        }
                        else
                        {
                            table2.AddCell(PDFMinedu.getCell("Revalidación / Convalidación:", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                            table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                            table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                        }
                    }
                    else
                    {
                        table2.AddCell(PDFMinedu.getCell("Retiro(s):", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                        table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                        table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));

                        table2.AddCell(PDFMinedu.getCell("Traslado(s):", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                        table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                        table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));

                        table2.AddCell(PDFMinedu.getCell("Prueba(s) de Ubicación:", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                        table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                        table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));

                        table2.AddCell(PDFMinedu.getCell("Revalidación / Convalidación:", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                        table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                        table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                    }
                }
                else
                {
                    table2.AddCell(PDFMinedu.getCell("Retiro(s):", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                    table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                    table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));

                    table2.AddCell(PDFMinedu.getCell("Traslado(s):", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                    table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                    table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));

                    table2.AddCell(PDFMinedu.getCell("Prueba(s) de Ubicación:", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                    table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                    table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));

                    table2.AddCell(PDFMinedu.getCell("Revalidación / Convalidación:", 7, false, 2, 1, TextAlignment.LEFT, null, false));
                    table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                    table2.AddCell(PDFMinedu.getCell(" - ", 8, false, 1, 2, TextAlignment.JUSTIFIED, null, false));
                }

                document.Add(table2);
                #endregion Observaciones

                PDFMinedu.addEspacios(document, sizeFont);

                #endregion SegundaPagina

                #region DateTime
                #endregion DateTime

                #region Footer
                withColumns = new float[] { 10, 70, 20 };
                Table tableFooter = new Table(UnitValue.CreatePercentArray(withColumns));
                tableFooter.SetWidth(UnitValue.CreatePercentValue(100));
                tableFooter.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                //842 - 36 - 36
                //595 - 36 - 36
                //tableFooter.SetWidth(523);
                #region QRImage
                IDictionary<EncodeHintType, Object> hints = new Dictionary<EncodeHintType, object>();
                hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L);
                //hints.Add(EncodeHintType.CHARACTER_SET, "");

                BarcodeQRCode barcodeQRCode = new BarcodeQRCode(hashQRCode, hints);
                PdfFormXObject xObject = barcodeQRCode.CreateFormXObject(PDFMinedu.getColor("BLACK"), pdf);
                Image qrImage = new Image(xObject);
                qrImage.ScaleToFit(75, 75);
                qrImage.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                //image.SetFixedPosition(10f, 10f);
                #endregion QRImage

                //cell.SetBackgroundColor(ColorConstants.ORANGE);
                Cell qrCell = new Cell(7, 1);
                if (!vistaPrevia)
                {
                    qrCell.Add(qrImage);
                }
                else
                {
                    qrCell.Add(PDFMinedu.getParagraph(" ", 10, false));
                }
                qrCell.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER);
                tableFooter.AddCell(qrCell);

                var nombreDirector = estudianteConstancia.solicitud.director == null ? "" : estudianteConstancia.solicitud.director;

                tableFooter.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : "Fecha de Emisión:", sizeFont, false, 1, 1, TextAlignment.RIGHT, null, false));
                tableFooter.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : (fechaSolicitud), sizeFont, false, 1, 1, TextAlignment.CENTER, null, false));

                tableFooter.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : "Hora de Emisión:", sizeFont, false, 1, 1, TextAlignment.RIGHT, null, false));
                tableFooter.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : horaSolicitud, sizeFont, false, 1, 1, TextAlignment.CENTER, null, false));

                tableFooter.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : "Firma del director:", sizeFont, false, 1, 1, TextAlignment.RIGHT, null, false));
                tableFooter.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : "", sizeFont, false, 1, 1, TextAlignment.CENTER, null, false));

                tableFooter.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : "", sizeFont, false, 1, 1, TextAlignment.RIGHT, null, false));
                tableFooter.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : nombreDirector, sizeFont, false, 1, 1, TextAlignment.CENTER, null, false));

                tableFooter.AddCell(PDFMinedu.getCell("* Este certificado de estudios puede ser verificado en el sitio web del " +
                "Ministerio de Educación (" + _configuration.GetSection("PDF:QRCodeUrlCertificado").Value + "/validate), " +
                "utilizando lectora de códigos QR o teléfono celular enfocando al código QR, el celular debe " +
                "de poseer un software gratuito descargado de internet.", 6, false, 1, 2, TextAlignment.JUSTIFIED, null, false));

                tableFooter.AddCell(PDFMinedu.getCell("* EXO: exoneración del área de educación religiosa a solicitud del padre o madre de familia, tutor legal o apoderado.", 6, false, 1, 2, TextAlignment.JUSTIFIED, null, false));

                /*tableFooter.AddCell(PDFMinedu.getCell(vistaPrevia ? " " : "N° " + correlativo, 8, true, 1, 1, TextAlignment.CENTER, null, false));

                tableFooter.AddCell(PDFMinedu.getCell("Calle del Comercio 193, San Borja - Lima, Perú / (511) 615 - 5800",
                    7, false, 1, 2, TextAlignment.CENTER, null, false));*/
                pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new EndPageEventHandler(tableFooter, document));

                #endregion Footer

                //estudianteConstancia.solicitud.estadoSolicitud = "1";

                #region WatherMark
                if (!estudianteConstancia.solicitud.estadoSolicitud.Equals("2"))
                {
                    WaterMarkPDF(pdf);

                }
                #endregion WatherMark

                return memoryStream;
            }
        }

        private class EndPageEventHandler : IEventHandler
        {
            private Document doc;
            private Table table;

            public EndPageEventHandler(Table table, Document doc)
            {
                this.table = table;
                this.doc = doc;
            }

            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
                new Canvas(canvas, pdfDoc, new Rectangle(doc.GetLeftMargin(),
                    18,
                    page.GetPageSize().GetWidth() - doc.GetLeftMargin() - doc.GetRightMargin(),
                    95)).Add(table);
            }
        }

        private string switchTextoGrado(string grado)
        {
            switch (grado)
            {
                case "PRIMERO":
                    return "1ro";
                case "SEGUNDO":
                    return "2do";
                case "TERCERO":
                    return "3ro";
                case "CUARTO":
                    return "4to";
                case "QUINTO":
                    return "5to";
                case "SEXTO":
                    return "6to";
                default:
                    return grado;
            }
        }

        void WaterMarkPDF(PdfDocument pdfDoc)
        {
            //string waterMarkText = _configuration.GetSection("PDF:WaterMarkTextCertificado").Value;
            string waterMarkText = "DOCUMENTO NO OFICIAL";
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            float fontSize = Convert.ToInt32(_configuration.GetSection("PDF:FontSize").Value);

            Rectangle pageSize;
            PdfCanvas canvas;
            int n = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                PdfPage page = pdfDoc.GetPage(i);
                pageSize = page.GetPageSize();
                canvas = new PdfCanvas(page);

                //canvas.SetColor(ColorConstants.GRAY, true);
                Paragraph p = new Paragraph(waterMarkText).SetFont(font).SetFontSize(fontSize);
                canvas.SaveState();
                PdfExtGState gs1 = new PdfExtGState();
                gs1.SetFillOpacity(0.6f);
                canvas.SetExtGState(gs1);
                canvas.Fill();
                new Canvas(canvas, pdfDoc, pdfDoc.GetDefaultPageSize()).SetFontColor(WebColors.GetRGBColor("GRAY"))
                    .ShowTextAligned(p, pageSize.GetWidth() / 2, pageSize.GetHeight() / 2, 1, TextAlignment.CENTER, VerticalAlignment.MIDDLE, 45);
                canvas.RestoreState();
                canvas.Release();

            }
        }

        //EstudianteModalidadNivelPersonaRequest
        public async Task<StatusResponse> GenerarPDFCertificado(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var result = new StatusResponse();
            var informacionIE = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var request = JsonConvert.DeserializeObject<Models.Certificado.EstudianteModalidadNivelPersonaRequest2>(desencriptarObjeto);

            Models.Certificado.EstudianteCertificado estudianteCertificado = new Models.Certificado.EstudianteCertificado();
            var informacionIEResponse = new Models.Siagie.EstudianteInfoPorCodModularResponse();

            int idSolicitud = 0;
            int zeroPersona = 0;
            if (request.idPersona == "0")
            {
                zeroPersona = 0;
            }

            if (request.idModalidad == "01")
            {
                request.idModalidad = ReactEncryptationSecurity.Encrypt(request.idModalidad);
            }

            if (request.idPersona.Length < 12)
            {
                request.idPersona = _encryptionServerSecurity.Encrypt(request.idPersona);
            }
            if (request.idSolicitud != null)
            {
                if (request.idSolicitud.Length < 12)
                {
                    request.idSolicitud = _encryptionServerSecurity.Encrypt(request.idSolicitud);
                }
            }

            var siagie = _encryptionServerSecurity.Decrypt<string>(
                ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                , "");

            try
            {
                //Validación de Estudiante
                var personaRequest = new Models.PersonaRequest()
                {
                    tipoDocumento = request.idTipoDocumento,
                    nroDocumento = request.numeroDocumento
                };

                var informacionIERequest = new Models.Siagie.EstudianteInfoPorCodModularRequest()
                {
                    codMod = request.codigoModular,
                    anexo = request.anexo
                };

                var statusResponseE = await _siagieService
                    .GetServiceByQueryAndToken<StatusResponse, Models.PersonaRequest>(siagie, "estudiante", personaRequest);

                informacionIE = await _siagieService
               .GetServiceByQueryAndToken<StatusResponse, Models.Siagie.EstudianteInfoPorCodModularRequest>(siagie, "datosalumno", informacionIERequest);

                informacionIEResponse = JsonConvert
                .DeserializeObject<List<Models.Siagie.EstudianteInfoPorCodModularResponse>>(informacionIE.Data.ToString())
                .FirstOrDefault();

                //Validación de estudiante en Siagie: FAIL?
                if (!statusResponseE.Success)
                {
                    string message = (statusResponseE.Messages.Count > 0)
                        ? statusResponseE.Messages[0]
                        : "El estudiante no cuenta con matrícula(s) registrada(s) a partir del 2013 en el SIAGIE, " +
                        "deberá de comunicarse con la Institución Educativa.";

                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add(message);
                    return result;
                }
                else
                {
                    if (zeroPersona == 0)
                    {
                        var personaEstudiante = JsonConvert
                        .DeserializeObject<List<Models.Siagie.EstudiantePersonaResponse>>(statusResponseE.Data.ToString());

                        request.idPersona = _encryptionServerSecurity.Encrypt(personaEstudiante.FirstOrDefault().idPersonaEstudiante.ToString());

                    }
                }


                if (request.tieneApoderado == 1)
                {
                    //Validación de Apoderado
                    personaRequest = new Models.PersonaRequest()
                    {
                        tipoDocumento = request.tipoDocumentoApoderado,
                        nroDocumento = request.numeroDocumentoApoderado
                    };

                    var statusResponseA = await _siagieService
                        .GetServiceByQueryAndToken<StatusResponse, Models.PersonaRequest>(siagie, "apoderado", personaRequest);


                    //Validación de apoderado en Siagie: FAIL?
                    if (!statusResponseA.Success)
                    {
                        string message = (statusResponseA.Messages.Count > 0)
                            ? statusResponseA.Messages[0]
                            : "Usted no se encuentra registrado como apoderado en el SIAGIE, " +
                            "deberá de comunicarse con la Institución Educativa.";

                        result.Success = false;
                        result.Data = null;
                        result.Messages.Add(message);
                        return result;
                    }
                    else
                    {
                        var personaApoderado = JsonConvert
                            .DeserializeObject<List<Models.Siagie.ApoderadoEstudianteResponse>>(statusResponseE.Data.ToString());

                        request.idPersonaApoderado = personaApoderado.FirstOrDefault().idPersonaApoderado;
                    }

                    //Validación de Relación entre Apoderado y Estudiante
                    var apoderadoEstudianteRequest = new Models.Certificado.ApoderadoEstudianteRequest()
                    {
                        idPersonaApoderado = request.idPersonaApoderado,
                        idPersonaEstudiante = _encryptionServerSecurity.Decrypt<int>(request.idPersona, 0),
                    };

                    var statusResponseEA = await _siagieService
                        .GetServiceByQueryAndToken<StatusResponse,
                        Models.Certificado.ApoderadoEstudianteRequest>(siagie, "apoderado/estudiante", apoderadoEstudianteRequest);

                    //Validación de relación apoderado-estudiante en Siagie: FAIL?
                    if (!statusResponseEA.Success)
                    {
                        string message = (statusResponseEA.Messages.Count > 0)
                            ? statusResponseEA.Messages[0]
                            : "El estudiante no tiene vinculo registrado con el apoderado indicado. " +
                            "Por favor asegúrese de haber ingresado los datos correctos del apoderado o del estudiante, " +
                            "si persiste el inconveniente deberá de comunicarse con la Institución Educativa.";

                        result.Success = false;
                        result.Data = null;
                        result.Messages.Add(message);
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Ocurrió un problema al procesar la información del solicitante y/o estudiante.");
                return result;
            }

            estudianteCertificado = await SiagieEstudiante(siagie, request);

            Models.ReniecPersona estudiantePersona = await _reniecService.ReniecConsultarPersona(request.numeroDocumento);
            if (estudiantePersona == null)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Ocurrió un problema en la validación con RENIEC.");
                return result;
            }

            try
            {
                //Traer solicitud, en caso exista
                _unitOfWork.BeginTransaction();
                int resultEstudiante = await _unitOfWork.InsertarEstudianteCertificado(Mappers.Certificado.EstudianteMapper.Map(new Models.Certificado.EstudianteCertificadoModel()
                {
                    idEstudiante = 0,
                    idPersona = request.idPersona.Length < 12 ? Convert.ToInt32(request.idPersona) : _encryptionServerSecurity.Decrypt<int>(request.idPersona, 0),
                    idTipoDocumento = request.idTipoDocumento,
                    numeroDocumento = request.numeroDocumento,
                    apellidoPaterno = estudiantePersona.apellidoPaterno,
                    apellidoMaterno = estudiantePersona.apellidoMaterno,
                    nombres = estudiantePersona.nombres,
                    ubigeo = estudiantePersona.ubigeoDomicilio,
                    departamento = estudiantePersona.dptoDomicilio,
                    provincia = estudiantePersona.provDomicilio,
                    distrito = estudiantePersona.distDomicilio
                }));


                if (resultEstudiante <= 0)
                {
                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add("Ocurrió un problema al registrar la información del estudiante.");
                    return result;
                }

                if (request.tieneApoderado == 1)
                {
                    int resultSolicitante = await _unitOfWork.InsertarSolicitanteCertificado(Mappers.Certificado.SolicitanteMapper.Map(new Models.Certificado.SolicitanteCertificadoModel()
                    {
                        idSolicitante = 0,
                        //idPersona = request.solicitante.idPersonaApoderado,
                        idPersona = request.idPersonaApoderado,
                        idTipoDocumento = request.tipoDocumentoApoderado,
                        numeroDocumento = request.numeroDocumentoApoderado,
                        apellidoPaterno = "",
                        apellidoMaterno = "",
                        nombres = "",
                        telefonoCelular = "",
                        correoElectronico = "",
                        ubigeo = "",
                        departamento = "",
                        provincia = "",
                        distrito = ""
                    }));

                    if (resultSolicitante <= 0)
                    {
                        result.Success = false;
                        result.Data = null;
                        result.Messages.Add("Ocurrió un problema al registrar la información del solicitante.");
                        return result;
                        //throw new InvalidOperationException("Ocurrió un problema al registrar la información del solicitante.");
                    }
                }

                //Estado de Estudiante
                var estadoEstudiante = 1;

                if (_encryptionServerSecurity.Decrypt<int>(request.idPersona, 0) > 0)
                {
                    //Evaluar estado de notas del estudiante
                    var siagie4 = _encryptionServerSecurity.Decrypt<string>(
                 ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                 , "");

                    var decrypted = new Models.Certificado.EstudianteModalidadNivelRequest2()
                    {
                        idPersona = _encryptionServerSecurity.Decrypt<string>(request.idPersona, "00"),

                        idModalidad = ReactEncryptationSecurity.Decrypt<string>(request.idModalidad, "00"),
                        idNivel = ReactEncryptationSecurity.Decrypt<string>(request.idNivel, "00"),
                        idSistema = "1"
                    };

                    var numeroNotasPendientes = await _siagieService.GetServiceByQueryAndToken<StatusResponse, Models.Certificado.EstudianteModalidadNivelRequest2>(siagie4, "pdf/grados", decrypted);

                    if (numeroNotasPendientes == null)
                    {
                        estadoEstudiante = 1;
                    }
                    else
                    {
                        if (numeroNotasPendientes.Data == null)
                        {
                            estadoEstudiante = 1;
                        }
                        else
                        {
                            var resultado = JsonConvert
                               .DeserializeObject<List<Models.Siagie.EstudianteModalidadNivelResponse>>(numeroNotasPendientes.Data.ToString())
                               .ToList();

                            var Informacion = resultado.Where(x => x.idAnio == 0).ToList();

                            if (resultado.Count() == Informacion.Count())
                            {
                                estadoEstudiante = 1;
                            }
                            if (Informacion.Count() > 0)
                            {
                                estadoEstudiante = 2;
                            }
                            if (Informacion.Count() == 0)
                            {
                                estadoEstudiante = 3;
                            }
                        }
                    }

                }

                idSolicitud = await _unitOfWork.InsertUpdateSolicitudCertificado(Mappers.Certificado.SolicitudMapper.Map(new Models.Certificado.SolicitudCertificadoModel()
                {
                    idSolicitud = request.idSolicitud == null ? 0 : _encryptionServerSecurity.Decrypt<int>(request.idSolicitud, 0),
                    idEstudiante = resultEstudiante,
                    idSolicitante = request.idSolicitante == null ? 0 : (request.idSolicitante.Length < 5 ? Convert.ToInt32(request.idSolicitante) : _encryptionServerSecurity.Decrypt<int>(request.idSolicitante, 0)),
                    idMotivo = request.idMotivo == null ? 0 : (request.idMotivo.Length < 5 ? Convert.ToInt32(request.idMotivo) : _encryptionServerSecurity.Decrypt<int>(request.idMotivo, 0)),
                    idModalidad = estudianteCertificado.solicitud.idModalidad,
                    abrModalidad = estudianteCertificado.solicitud.abrModalidad,
                    dscModalidad = estudianteCertificado.solicitud.dscModalidad,
                    idNivel = estudianteCertificado.solicitud.idNivel,
                    dscNivel = estudianteCertificado.solicitud.dscNivel,
                    idGrado = estudianteCertificado.solicitud.idGrado,
                    dscGrado = estudianteCertificado.solicitud.dscGrado,
                    anioCulminacion = request.anioCulminacion,
                    codigoModular = request.codigoModular,
                    anexo = request.anexo,
                    estadoEstudiante = estadoEstudiante.ToString(),
                    estadoSolicitud = request.estadoSolicitud == null ? "1" : request.estadoSolicitud,
                    director = informacionIEResponse.director
                }));

                if (idSolicitud <= 0)
                {
                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add("Ocurrió un problema al registrar su solicitud (1).");
                    return result;
                }

                int countGrados = 0;
                foreach (Models.Certificado.GradoCertificadoModel grado in estudianteCertificado.grados)
                {
                    grado.idSolicitud = idSolicitud;
                    var idConstanciaGrado = await _unitOfWork.InsertarGradoCertificado(Mappers.Certificado.GradoCertificadoMapper.Map(grado));

                    if (idConstanciaGrado > 0)
                    {
                        countGrados++;
                    }
                }

                if (countGrados != estudianteCertificado.grados.Count)
                {
                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add("Ocurrió un problema al registrar su solicitud (2).");
                    return result;
                    //throw new InvalidOperationException("Ocurrió un problema al registrar su solicitud (2).");
                }

                int countNotas = 0;
                foreach (Models.Certificado.NotaCertificadoModel nota in estudianteCertificado.notas)
                {
                    nota.idSolicitud = idSolicitud;
                    var idConstanciaNota = await _unitOfWork.InsertarNotaCertificado(Mappers.Certificado.NotaCertificadoMapper.Map(nota));

                    if (idConstanciaNota > 0)
                    {
                        countNotas++;
                    }
                }

                if (countNotas != estudianteCertificado.notas.Count)
                {
                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add("Ocurrió un problema al registrar su solicitud (3).");
                    return result;
                }

                if (estudianteCertificado.observaciones != null)
                {
                    int countObs = 0;
                    foreach (Models.Certificado.ObservacionCertificadoModel obs in estudianteCertificado.observaciones)
                    {
                        obs.idSolicitud = idSolicitud;
                        var idConstanciaObservacion = await _unitOfWork.InsertarObservacionCertificado(Mappers.Certificado.ObservacionCertificadoMapper.Map(obs));

                        if (idConstanciaObservacion > 0)
                        {
                            countObs++;
                        }
                    }

                    if (countObs != estudianteCertificado.observaciones.Count)
                    {
                        result.Success = false;
                        result.Data = null;
                        result.Messages.Add("Ocurrió un problema al registrar su solicitud (4).");
                        return result;
                    }
                }

                _unitOfWork.SolicitudEstadoEmitido(idSolicitud
                    , _encryptionServerSecurity.Decrypt<int>(request.idPersona, 0)
                    , estudianteCertificado.solicitud.idModalidad
                    , estudianteCertificado.solicitud.idNivel);


                var resultSolicitud = await _unitOfWork.ObtenerSolicitudCertificado(Mappers.Certificado.SolicitudCertificadoMapper.Map(new Models.Certificado.SolicitudCertificadoModel()
                {
                    idSolicitud = idSolicitud
                }));

                Models.Certificado.SolicitudCertificadoModel solicitud = Mappers.Certificado.SolicitudCertificadoMapper.Map(resultSolicitud.FirstOrDefault());
                var nombresCompleto = estudiantePersona.apellidoPaterno + " " + estudiantePersona.apellidoMaterno + ", " + estudiantePersona.nombres;
                if (request.correo != "")
                {
                    var correo = PrepararCorreo(solicitud, nombresCompleto, request.correo);
                    var correoResult = await EnviarCorreo(correo);
                }


                result.Data = 1;
                result.Success = true;
                result.Messages.Add("Se emitió el certificado correspondiente.");
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Ocurrió un problema al registrar su solicitud.");
                return result;
            }

            return result;
        }

        public async Task<StatusResponse> ValidarSolicitud(Models.Certificado.CertificadoRequest request)
        {

            var result = new StatusResponse();
            int num = 0;


            try
            {
                var respuesta = await _unitOfWork.GetValidarSolicitud(request.estudiante.tipoDocEstudiante, request.estudiante.nroDocEstudiante, "", "", 0);
                var solicitud = respuesta.FirstOrDefault();


                if (solicitud.NUM_SOLICITUDES == 0)
                {
                    result.Success = true;
                    result.Data = 0;
                    result.Messages.Add("Solicitud validación exitosamente.");
                }
                else
                {

                    result.Success = true;
                    result.Data = 1;
                    result.Messages.Add("Usted actualmente tiene una Solicitud Pendiente por revisar. Diríjase a la Bandeja de Solicitudes Pendientes");

                }

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Ocurrió un problema al validar su solicitud.");
                return result;
            }



        }

        public async Task<byte[]> ValidarPDFCertificado(Models.Certificado.ParametroModel objetoEncriptado)
        {
            try
            {
                var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
                var request = JsonConvert.DeserializeObject<Models.Certificado.VerificacionDocumentoRequest>(desencriptarObjeto);


                Models.Certificado.EstudianteCertificado estudianteConstancia = new Models.Certificado.EstudianteCertificado();

                var resultSolicitud = await _unitOfWork.ObtenerSolicitudCertificadoPorCodigoVirtual2(request.codigoVirtual, request.tipoDocumento, request.numeroDocumento);
                if (resultSolicitud == null || resultSolicitud.ToList().Count == 0)
                {
                    return null;
                }
                estudianteConstancia.solicitud = Mappers.Certificado.SolicitudCertificadoMapper.Map(resultSolicitud.FirstOrDefault());

                var resultEstudiante = await _unitOfWork.ObtenerEstudianteCertificadoValidado(estudianteConstancia.solicitud.idEstudiante);
                if (resultEstudiante == null || resultEstudiante.ToList().Count == 0)
                {
                    return null;
                }
                estudianteConstancia.estudiante = Mappers.Certificado.EstudianteCertificadoMapper.Map(resultEstudiante.FirstOrDefault());

                var resultGrados = await _unitOfWork.ObtenerGradosCertificadoValidados(estudianteConstancia.solicitud.idSolicitud);
                if (resultGrados == null || resultGrados.ToList().Count == 0)
                {
                    return null;
                }
                estudianteConstancia.grados = resultGrados.Select(Mappers.Certificado.GradoCertificadoMapper.Map).ToList();

                var resultNotas = await _unitOfWork.ObtenerNotasCertificadoValidadas(estudianteConstancia.solicitud.idSolicitud);
                if (resultNotas == null || resultNotas.ToList().Count == 0)
                {
                    return null;
                }
                estudianteConstancia.notas = resultNotas.Where(x => x.ACTIVO).Select(Mappers.Certificado.NotaCertificadoMapper.Map).ToList();

                var resultObservaciones = await _unitOfWork.ObtenerObservacionesCertificadoValidadas(estudianteConstancia.solicitud.idSolicitud);
                if (resultObservaciones != null)
                {
                    estudianteConstancia.observaciones = resultObservaciones.Select(Mappers.Certificado.ObservacionCertificadoMapper.Map).ToList();
                }

                /*var InstitucionEducativa = new Models.Certificado.InstitucionEducativaPorDreUgelRequest
                {
                    CodigoModular = request.codigoModular,
                    anexo = request.anexo
                };

                var siagie = _encryptionServerSecurity.Decrypt<string>(
                   ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                   , "");

                var institucionResponse = await _siagieService
                        .GetServiceByQueryAndToken<StatusResponse,
                        Models.Certificado.InstitucionEducativaPorDreUgelRequest>(siagie, "datosinstitucioneducativa", InstitucionEducativa);

                var institucionEducativa = JsonConvert
                               .DeserializeObject<List<InstitucionEducativaPorDreUgelResponse>>(institucionResponse.Data.ToString())
                               .FirstOrDefault();*/

                return PDFCertificadoInit(false, estudianteConstancia/*, institucionEducativa*/);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<StatusResponse> RegistroMasivoEstudiantes(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var result = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var request = JsonConvert.DeserializeObject<List<Models.Certificado.EstudianteModalidadNivelPersonaRequest2>>(desencriptarObjeto);

            List<Models.Certificado.EstudianteModalidadNivelPersonaRequest2> listaRechazados = new List<Models.Certificado.EstudianteModalidadNivelPersonaRequest2>();

            try
            {
                foreach (var item in request)
                {
                    item.idPersona = item.codPersona;
                    item.numeroDocumento = item.numeroDocumento.Trim();
                    item.idTipoDocumento = item.idTipoDocumento == null ? "2" : item.idTipoDocumento;

                    var response = await RegistroIndividualEstudiante(item);

                    if (response.Data == null)
                    {
                        listaRechazados.Add(item);
                    }

                }
                var mensaje = request.Count() > 1 ?
                                "FINALIZÓ CON ÉXITO LA GENERACIÓN DE CERTIFICADOS DE LOS ESTUDIANTES."
                                : "FINALIZÓ CON ÉXITO LA GENERACIÓN DE CERTIFICADOS DEL ESTUDIANTE.";

                result.Success = true;
                result.Data = listaRechazados;
                result.Messages.Add(mensaje);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Se presentó un inconveniente al registrar la información en el sistema.");
            }
            return result;
        }

        private async Task<StatusResponse> RegistroIndividualEstudiante(Models.Certificado.EstudianteModalidadNivelPersonaRequest2 request)
        {
            var result = new StatusResponse();
            Models.Certificado.EstudianteCertificado estudianteCertificado = new Models.Certificado.EstudianteCertificado();
            int idSolicitud = 0;
            var estadoEstudiante = 1;

            var siagie = _encryptionServerSecurity.Decrypt<string>(
                ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                , "");

            try
            {
                //Validación de Estudiante
                var personaRequest = new Models.PersonaRequest()
                {
                    tipoDocumento = request.idTipoDocumento == null ? "2" : request.idTipoDocumento,
                    nroDocumento = request.numeroDocumento
                };

                var statusResponseE = await _siagieService
                    .GetServiceByQueryAndToken<StatusResponse, Models.PersonaRequest>(siagie, "estudiante", personaRequest);

                //Validación de estudiante en Siagie: FAIL?
                if (!statusResponseE.Success)
                {
                    string message = (statusResponseE.Messages.Count > 0)
                        ? statusResponseE.Messages[0]
                        : "El estudiante no cuenta con matrícula(s) registrada(s) a partir del 2013 en el SIAGIE, " +
                        "deberá de comunicarse con la Institución Educativa.";

                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add(message);
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Ocurrió un problema al procesar la información del solicitante y/o estudiante.");
                return result;
            }




            estudianteCertificado = await SiagieEstudiante(siagie, request);

            Models.ReniecPersona estudiantePersona = await _reniecService.ReniecConsultarPersona(request.numeroDocumento);
            if (estudiantePersona == null)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Ocurrió un problema en la validación con RENIEC.");
                return result;
            }

            try
            {
                var requestIE = new Models.Siagie.EstudianteInfoPorCodModularRequest()
                {
                    codMod = request.codigoModular == null ? request.codMod : request.codigoModular,
                    anexo = request.anexo
                };

                var responseIE = await _siagieService
               .GetServiceByQueryAndToken<StatusResponse, Models.Siagie.EstudianteInfoPorCodModularRequest>(siagie, "datosalumno", requestIE);

                var informacionEstudiante = JsonConvert
                .DeserializeObject<List<Models.Siagie.EstudianteInfoPorCodModularResponse>>(responseIE.Data.ToString())
                .FirstOrDefault();

                //Traer solicitud, en caso exista

                if (_encryptionServerSecurity.Decrypt<int>(request.idPersona.ToString(), 0) != 0)
                {
                    //Evaluar estado de notas del estudiante
                    var siagie4 = _encryptionServerSecurity.Decrypt<string>(
                 ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                 , "");
                    var decrypted = new Models.Certificado.EstudianteModalidadNivelRequest2()
                    {
                        idPersona = _encryptionServerSecurity.Decrypt<string>(request.idPersona.ToString(), "00"),
                        idModalidad = ReactEncryptationSecurity.Decrypt<string>(request.idModalidad, "00"),
                        idNivel = ReactEncryptationSecurity.Decrypt<string>(request.idNivel, "00"),
                        idSistema = "1"
                    };

                    var numeroNotasPendientes = await _siagieService.GetServiceByQueryAndToken<StatusResponse, Models.Certificado.EstudianteModalidadNivelRequest2>(siagie4, "pdf/grados", decrypted);

                    if (numeroNotasPendientes == null)
                    {
                        estadoEstudiante = 1;
                    }
                    else
                    {
                        if (numeroNotasPendientes.Data == null)
                        {
                            estadoEstudiante = 1;
                        }
                        else
                        {
                            var resultado = JsonConvert
                               .DeserializeObject<List<Models.Siagie.EstudianteModalidadNivelResponse>>(numeroNotasPendientes.Data.ToString())
                               .ToList();

                            var Informacion = resultado.Where(x => x.idAnio == 0).ToList();
                            //var informacionIncompleta = resultado.Where(x => x.idAnio == 2).ToList();
                            //var informacioncompleta = resultado.Where(x => x.idAnio == 3).ToList();

                            //  if (sinInformacion.Count() == 0 && faltaNotasRegularizar.Count() == 0 && conNotas.Count() > 0)
                            if (resultado.Count() == Informacion.Count())
                            {
                                estadoEstudiante = 1;
                            }
                            if (Informacion.Count() > 0)
                            {
                                estadoEstudiante = 2;
                            }
                            if (Informacion.Count() == 0)
                            {
                                estadoEstudiante = 3;
                            }
                        }
                    }

                }



                _unitOfWork.BeginTransaction();

                int resultEstudiante = await _unitOfWork.InsertarEstudianteCertificado(Mappers.Certificado.EstudianteMapper.Map(new Models.Certificado.EstudianteCertificadoModel()
                {
                    idEstudiante = 0,
                    idPersona = _encryptionServerSecurity.Decrypt<int>(request.idPersona, 0),
                    idTipoDocumento = request.idTipoDocumento == null ? "2" : request.idTipoDocumento,
                    numeroDocumento = request.numeroDocumento,
                    apellidoPaterno = estudiantePersona.apellidoPaterno,
                    apellidoMaterno = estudiantePersona.apellidoMaterno,
                    nombres = estudiantePersona.nombres,
                    ubigeo = estudiantePersona.ubigeoDomicilio,
                    departamento = estudiantePersona.dptoDomicilio,
                    provincia = estudiantePersona.provDomicilio,
                    distrito = estudiantePersona.distDomicilio
                }));


                if (resultEstudiante <= 0)
                {
                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add("Ocurrió un problema al registrar la información del estudiante.");
                    return result;
                }


                var solicitudGenerada = await _unitOfWork.ObtenerSolicitudesPendientes(estudianteCertificado.estudiante.idTipoDocumento, estudianteCertificado.estudiante.numeroDocumento);
                Entities.Certificado.SolicitudExtend entidadSolicitud = new Entities.Certificado.SolicitudExtend();

                if (solicitudGenerada != null && solicitudGenerada.Count() > 0)
                {
                    entidadSolicitud = solicitudGenerada.FirstOrDefault();
                }

                idSolicitud = await _unitOfWork.InsertarSolicitudCertificado(Mappers.Certificado.SolicitudMapper.Map(new Models.Certificado.SolicitudCertificadoModel()
                {
                    idSolicitud = (solicitudGenerada == null && solicitudGenerada.Count() > 0) ? entidadSolicitud.ID_SOLICITUD : 0,
                    idEstudiante = resultEstudiante,
                    idSolicitante = 0,
                    idMotivo = 13,
                    idModalidad = estudianteCertificado.solicitud.idModalidad,
                    abrModalidad = estudianteCertificado.solicitud.abrModalidad,
                    dscModalidad = estudianteCertificado.solicitud.dscModalidad,
                    idNivel = estudianteCertificado.solicitud.idNivel,
                    dscNivel = estudianteCertificado.solicitud.dscNivel,
                    idGrado = estudianteCertificado.solicitud.idGrado,
                    dscGrado = estudianteCertificado.solicitud.dscGrado,
                    anioCulminacion = (solicitudGenerada == null && solicitudGenerada.Count() > 0) ? entidadSolicitud.ULTIMO_ANIO : request.idAnio,
                    anexo = request.anexo,
                    codigoModular = request.codMod,
                    estadoEstudiante = estadoEstudiante.ToString(),
                    estadoSolicitud = request.estadoSolicitud,
                    director = informacionEstudiante.director
                }));

                if (idSolicitud <= 0)
                {
                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add("Ocurrió un problema al registrar su solicitud (1).");
                    return result;
                }

                int countGrados = 0;
                foreach (Models.Certificado.GradoCertificadoModel grado in estudianteCertificado.grados)
                {
                    grado.idSolicitud = idSolicitud;
                    var idConstanciaGrado = await _unitOfWork.InsertarGradoCertificado(Mappers.Certificado.GradoCertificadoMapper.Map(grado));

                    if (idConstanciaGrado > 0)
                    {
                        countGrados++;
                    }
                }

                if (countGrados != estudianteCertificado.grados.Count)
                {
                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add("Ocurrió un problema al registrar su solicitud (2).");
                    return result;
                    //throw new InvalidOperationException("Ocurrió un problema al registrar su solicitud (2).");
                }

                int countNotas = 0;
                foreach (Models.Certificado.NotaCertificadoModel nota in estudianteCertificado.notas)
                {
                    nota.idSolicitud = idSolicitud;
                    var idConstanciaNota = await _unitOfWork.InsertarNotaCertificado(Mappers.Certificado.NotaCertificadoMapper.Map(nota));

                    if (idConstanciaNota > 0)
                    {
                        countNotas++;
                    }
                }

                if (countNotas != estudianteCertificado.notas.Count)
                {
                    result.Success = false;
                    result.Data = null;
                    result.Messages.Add("Ocurrió un problema al registrar su solicitud (3).");
                    return result;
                }

                if (estudianteCertificado.observaciones != null)
                {
                    int countObs = 0;
                    foreach (Models.Certificado.ObservacionCertificadoModel obs in estudianteCertificado.observaciones)
                    {
                        obs.idSolicitud = idSolicitud;
                        var idConstanciaObservacion = await _unitOfWork.InsertarObservacionCertificado(Mappers.Certificado.ObservacionCertificadoMapper.Map(obs));

                        if (idConstanciaObservacion > 0)
                        {
                            countObs++;
                        }
                    }

                    if (countObs != estudianteCertificado.observaciones.Count)
                    {
                        result.Success = false;
                        result.Data = null;
                        result.Messages.Add("Ocurrió un problema al registrar su solicitud (4).");
                        return result;
                    }
                }

                _unitOfWork.SolicitudEstadoEmitido(idSolicitud
                    , _encryptionServerSecurity.Decrypt<int>(request.idPersona, 0)
                    , estudianteCertificado.solicitud.idModalidad
                    , estudianteCertificado.solicitud.idNivel);


                result.Data = 1;
                result.Success = true;
                result.Messages.Add("Se emitió el certificado correspondiente.");
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Ocurrió un problema al registrar su solicitud.");
                return result;
            }

            return result;
        }

        public async Task<StatusResponse> ValidarDatosReniecApoderado(string nroDocumento)
        {
            var response = new StatusResponse();

            try
            {
                var numeroDocumento = ReactEncryptationSecurity.Decrypt<string>(nroDocumento, "00");

                var consultaPersona = await _reniecService.ReniecConsultarPersona(numeroDocumento);

                if (consultaPersona != null)
                {
                    consultaPersona.numDoc = ReactEncryptationSecurity.Encrypt(consultaPersona.numDoc);
                    response.Data = consultaPersona;
                    response.Success = true;
                    response.Messages.Add("Consulta exitosa RENIEC.");
                }
                else
                {
                    response.Data = null;
                    response.Success = false;
                    response.Messages.Add("No se encontraron datos del usuario.");
                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Messages.Add("Ocurrió un error al consultar datos del usuario.");
            }

            return response;
        }

        public async Task<StatusResponse> ValidarArea(Models.Certificado.AreaRequest request)
        {

            var result = new StatusResponse();
            int num = 0;


            try
            {

                var respuesta = await _unitOfWork.GetValidarArea(Mappers.Certificado.AreaMapper.Map(new Models.Certificado.AreaModel()
                {
                    nivel = ReactEncryptationSecurity.Decrypt<string>(request.nivel, "00"),
                    codigoTipoArea = request.CodigoTipoArea,
                    descripcionArea = request.DescripcionArea
                }));
                var area = respuesta.FirstOrDefault();



                if (area.NUM_AREAS == 0)
                {
                    result.Success = true;
                    result.Data = 0;
                    result.Messages.Add("Area validación exitosa.");
                    return result;
                }
                else
                {

                    result.Success = true;
                    result.Data = 1;
                    result.Messages.Add("El area existe en la base de datos");
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Messages.Add("Ocurrió un problema al validar el Area.");
                return result;
            }



        }
        public async Task<StatusResponse> ObtenerAreasPorDisenio(Models.Certificado.AreaPorDisenioRequest request)
        {
            var response = new StatusResponse();

            request.IdNivel = ReactEncryptationSecurity.Decrypt<string>(request.IdNivel, "00");

            try
            {
                var result = await _unitOfWork.ObtenerAreasPorDisenio(request.IdAnio, request.IdNivel);

                if (result != null)
                {
                    response.Data = result.ToList();
                    response.Success = true;
                    response.Messages.Add("Consulta exitosa.");
                }
                else
                {
                    response.Data = null;
                    response.Success = false;
                    response.Messages.Add("No se encontraron datos.");
                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Messages.Add("Ocurrió un error al consultar datos.");
            }

            return response;
        }

        public async Task<byte[]> VistaPreviaPDF(Models.Certificado.ParametroModel objetoEncriptado)
        {            
            byte[] pdf;
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var request = JsonConvert.DeserializeObject<Models.Certificado.CodigoSolicitudCertificadoRequest>(desencriptarObjeto);

            try
            {
                var idSolicitud = _encryptionServerSecurity.Decrypt<int>(request.codigoSolicitud, 0);

                var siagie = _encryptionServerSecurity.Decrypt<string>(
                ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                , "");

                Entities.Certificado.SolicitudExtend certificadoEntity = new Entities.Certificado.SolicitudExtend();
                certificadoEntity.ID_SOLICITUD = idSolicitud;
                var response = await _unitOfWork.GetSolicitud(certificadoEntity);
                var solicitud = response.FirstOrDefault();
                Models.Certificado.EstudianteCertificado estudianteCertificado = new Models.Certificado.EstudianteCertificado();
                Models.Certificado.EstudianteCertificadoModel estudianteModel = new Models.Certificado.EstudianteCertificadoModel();
                Models.Certificado.SolicitudCertificadoModel solicitudModel = new Models.Certificado.SolicitudCertificadoModel();

                estudianteModel.idTipoDocumento = solicitud.ID_TIPO_DOCUMENTO;
                estudianteModel.numeroDocumento = solicitud.NUMERO_DOCUMENTO;
                estudianteModel.apellidoPaterno = solicitud.APELLIDO_PATERNO;
                estudianteModel.apellidoMaterno = solicitud.APELLIDO_MATERNO;
                estudianteModel.nombres = solicitud.NOMBRE;
                estudianteModel.idPersona = solicitud.ID_PERSONA;

                solicitudModel.anioCulminacion = solicitud.ULTIMO_ANIO;
                solicitudModel.idModalidad = solicitud.ID_MODALIDAD;
                solicitudModel.idNivel = solicitud.ID_NIVEL;
                solicitudModel.dscGrado = solicitud.DSC_GRADO;
                solicitudModel.codigoModular = solicitud.CODIGO_MODULAR;
                solicitudModel.anexo = solicitud.ANEXO;
                solicitudModel.abrModalidad = solicitud.ABR_MODALIDAD;
                solicitudModel.dscModalidad = solicitud.DSC_MODALIDAD;
                solicitudModel.dscNivel = solicitud.DSC_NIVEL;
                solicitudModel.codigoVirtual = solicitud.CODIGO_VIRTUAL;
                solicitudModel.director = solicitud.DIRECTOR;
                solicitudModel.estadoSolicitud = solicitud.ESTADO_SOLICITUD;
                //Llamar a notas
                estudianteCertificado.solicitud = solicitudModel;
                estudianteCertificado.estudiante = estudianteModel;

                //var notasList = await _unitOfWork.ObtenerNotasPorSolicitud(request.idSolicitud);
                //var gradosList = await _unitOfWork.ObtenerGradosPorSolicitud(request.idSolicitud);

                List<Models.Certificado.PDFNotaCertificado> constanciaNotasList = new List<Models.Certificado.PDFNotaCertificado>();
                List<Models.Certificado.PDFNotaCertificado> competenciaList = new List<Models.Certificado.PDFNotaCertificado>();
                List<Models.Certificado.PDFNotaCertificado> tallerList = new List<Models.Certificado.PDFNotaCertificado>();
                List<Models.Certificado.ObservacionCertificadoModel> listaObservaciones = new List<Models.Certificado.ObservacionCertificadoModel>();

                var resultadoNotas = await _unitOfWork.ObtenerNotasPorSolicitud(idSolicitud);
                if (resultadoNotas == null || resultadoNotas.ToList().Count == 0)
                {
                    return null;
                }
                estudianteCertificado.notas = resultadoNotas.Where(x => x.ACTIVO).Select(Mappers.Certificado.NotaCertificadoMapper.Map).ToList();

                var resultadoGrados = await _unitOfWork.ObtenerGradosPorSolicitud(idSolicitud);
                if (resultadoGrados == null || resultadoGrados.ToList().Count == 0)
                {
                    return null;
                }
                estudianteCertificado.grados = resultadoGrados.Select(Mappers.Certificado.GradoCertificadoMapper.Map).ToList();


                /*var InstitucionEducativa = new Models.Certificado.InstitucionEducativaPorDreUgelRequest
                {
                    CodigoModular = request.codigoModular,
                    anexo = request.anexo
                };

                var institucionResponse = await _siagieService
                        .GetServiceByQueryAndToken<StatusResponse,
                        Models.Certificado.InstitucionEducativaPorDreUgelRequest>(siagie, "datosinstitucioneducativa", InstitucionEducativa);

                var institucionEducativa = JsonConvert
                               .DeserializeObject<List<Models.Siagie.InstitucionEducativaPorDreUgelResponse>>(institucionResponse.Data.ToString())
                               .FirstOrDefault();

                */
                var observacionRequest = new Models.Certificado.EstudianteModalidadNivelRequest2
                {
                    idPersona = estudianteModel.idPersona.ToString(),
                    idModalidad = solicitudModel.idModalidad,
                    idNivel = solicitudModel.idNivel,
                    idSistema = "1"
                };

                if (idSolicitud == 0)
                {//Siagie

                    var observacionesResponse = await _siagieService
                            .GetServiceByQueryAndToken<StatusResponse,
                            Models.Certificado.EstudianteModalidadNivelRequest2>(siagie, "pdf/observaciones", observacionRequest);


                    estudianteCertificado.observaciones = JsonConvert
                                   .DeserializeObject<List<Models.Certificado.ObservacionCertificadoModel>>(observacionesResponse.Data.ToString());
                }
                else
                {//Certificado*/
                    var resultObservaciones = await _unitOfWork.ObtenerObservacionesCertificadoValidadas(idSolicitud);
                    if (resultObservaciones != null)
                    {
                        var observacionesResponse = resultObservaciones.ToList();
                        estudianteCertificado.observaciones = observacionesResponse.Select(Mappers.Certificado.ObservacionCertificadoMapper.Map).ToList();
                    }

                }

                //pdf= PDFCertificado(true, estudianteConstancia, constanciaNotasList, competenciaList, tallerList, listaObservaciones, institucionEducativa).ToArray();
                return PDFCertificadoInit(true, estudianteCertificado/*, institucionEducativa*/);
            }
            catch (Exception ex)
            {
                throw;
            }
            //return ;
            // return pdf;
        }

        public async Task<int> UpdateEstadoCertificado(Models.Certificado.ParametroModel objetoEncriptado)
        {            
            var rows = default(int);
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var model = JsonConvert.DeserializeObject<Models.Certificado.CodigoSolicitudCertificadoRequest>(desencriptarObjeto);

            try
            {

                var idSolicitud = _encryptionServerSecurity.Decrypt<int>(model.codigoSolicitud, 0);

                _unitOfWork.BeginTransaction();

                rows = await _unitOfWork.UpdateEstadoCertificado(idSolicitud, model.estadoSolicitud, model.estadoEstudiante);

                if (rows > 0)
                {
                    _unitOfWork.Commit();
                }
                else
                {
                    _unitOfWork.Rollback();
                }
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
            }

            return rows;
        }

        public async Task<StatusResponse> ObtenerDatosSolicitud(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var response = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var request = JsonConvert.DeserializeObject<Models.Certificado.CodigoSolicitudCertificadoRequest>(desencriptarObjeto);
            List<Models.Certificado.GradoCertificadoModel> listaGrados = new List<Models.Certificado.GradoCertificadoModel>();
            List<Models.Certificado.NotaCertificadoModel> listaNotas = new List<Models.Certificado.NotaCertificadoModel>();
            List<Entities.Certificado.ObservacionCertificadoEntity> listaObservaciones = new List<Entities.Certificado.ObservacionCertificadoEntity>();

            var idSolicitud = _encryptionServerSecurity.Decrypt<int>(request.codigoSolicitud, 0);

            try
            {
                var aGrados = await _unitOfWork.ObtenerGradosPorSolicitud(idSolicitud);
                if (aGrados != null)
                {
                    listaGrados = aGrados.Select(x => new Models.Certificado.GradoCertificadoModel()
                    {
                        idConstanciaGrado = x.ID_CONSTANCIA_GRADO,
                        idSolicitud = x.ID_SOLICITUD,
                        idGrado = x.ID_GRADO,
                        idAnio = x.ID_ANIO,
                        codMod = x.COD_MOD,
                        anexo = x.ANEXO,
                        dscGrado = x.DSC_GRADO,
                        corrEstadistica = x.CORR_ESTADISTICA,
                        situacionFinal = x.SITUACION_FINAL,
                        ciclo = x.CICLO
                    }).ToList();
                }

                var aNotas = await _unitOfWork.ObtenerNotasPorSolicitud(idSolicitud);
                if (aNotas != null)
                {
                    listaNotas = aNotas.Where(x => x.ACTIVO).Select(x => new Models.Certificado.NotaCertificadoModel()
                    {
                        idConstanciaNota = x.ID_CONSTANCIA_NOTA,
                        idSolicitud = x.ID_SOLICITUD,
                        idTipoArea = x.ID_TIPO_AREA,
                        idArea = x.ID_AREA,
                        idAnio = x.ID_ANIO,
                        idGrado = x.ID_GRADO,
                        codMod = x.COD_MOD,
                        anexo = x.ANEXO,
                        idNivel = x.ID_NIVEL,
                        dscArea = x.DSC_AREA,
                        dscGrado = x.DSC_GRADO,
                        dscNivel = x.DSC_NIVEL,
                        dscTipoArea = x.DSC_TIPO_AREA,
                        esconducta = x.ESCONDUCTA,
                        notaFinalArea = x.NOTA_FINAL_AREA,
                        estado = x.ESTADO,
                        ciclo = x.CICLO,
                        esAreaSiagie = x.ES_AREA_SIAGIE
                    }).ToList();
                }

                var aObservaciones = await _unitOfWork.ObtenerObservacionesCertificadoValidadas(idSolicitud);
                if (aObservaciones != null)
                {
                    listaObservaciones = aObservaciones.ToList();
                }

                List<Models.Certificado.PDFNotaCertificado> cursoList = OrdenarNotasEstudiante(listaNotas, listaGrados, "001");
                List<Models.Certificado.PDFNotaCertificado> tallerList = OrdenarNotasEstudiante(listaNotas, listaGrados, "002");
                List<Models.Certificado.PDFNotaCertificado> competenciaList = OrdenarNotasEstudiante(listaNotas, listaGrados, "003");

                var lista = new Models.Certificado.DatosAcademicosResponse()
                {
                    grados = listaGrados,
                    cursoList = cursoList,
                    tallerList = tallerList,
                    competenciaList = competenciaList,
                    observaciones = listaObservaciones
                };

                response.Data = ReactEncryptationSecurity.Encrypt(JsonConvert.SerializeObject(lista));
                response.Success = true;
                response.Messages.Add("Consulta exitosa.");

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Messages.Add("Ocurrió un error al consultar datos.");
            }

            return response;
        }

        public async Task<StatusResponse> ActualizarDatosAcademicos(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var result = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var request = JsonConvert.DeserializeObject<Models.Certificado.DatosAcademicosModel>(desencriptarObjeto);
            var rows = default(int);
            int IdSolicitud = _encryptionServerSecurity.Decrypt<int>(request.codigoSolicitud, 0); ;

            try
            {

                _unitOfWork.BeginTransaction();

                int resultadoNotas = 0;
                int resultadoGrados = 0;
                int resultadoObservaciones = 0;

                var notasPorSolicitud = await _unitOfWork.ObtenerNotasPorSolicitud(IdSolicitud);
                var gradosPorSolicitud = await _unitOfWork.ObtenerGradosPorSolicitud(IdSolicitud);
                var observacionPorSolicitud = await _unitOfWork.ObtenerObservacionesCertificadoValidadas(IdSolicitud);

                foreach (var item in request.notas)
                {
                    item.IdSolicitud = IdSolicitud;
                    item.IdNivel = ReactEncryptationSecurity.Decrypt<string>(item.IdNivel, "00");
                    item.Activo = true;
                    var registroNota = notasPorSolicitud.Where(x => x.ID_SOLICITUD == IdSolicitud && x.ID_NIVEL == item.IdNivel
                                                     && x.ID_GRADO == item.IdGrado && x.ID_TIPO_AREA == item.IdTipoArea && x.ID_AREA == item.IdArea).FirstOrDefault();

                    if (registroNota == null)
                    {
                        rows = await _unitOfWork.InsertarNotaCertificado(Mappers.Certificado.NotasMapper.Map(item));
                    }
                    else
                    {
                        registroNota.ANEXO = item.Anexo;
                        registroNota.COD_MOD = item.CodigoModular;
                        registroNota.ID_ANIO = item.IdAnio;
                        registroNota.NOTA_FINAL_AREA = item.NotaFinal;
                        registroNota.ACTIVO = true;
                        rows = await _unitOfWork.InsertarNotaCertificado(registroNota);//Actualizando notas
                    }

                    if (rows > 0)
                    {
                        resultadoNotas++;
                    }
                }

                if (resultadoNotas != request.notas.Length)
                {
                    _unitOfWork.Rollback();
                    result.Success = false;
                    result.Data = 0;
                    result.Messages.Add("Ocurrió un problema al registrar las notas académicas.");
                    return result;
                }

                //foreach (var item in request.grados.ToList())
                for (int z = 0; z < request.grados.Length; z++)
                {

                    request.grados[z].idSolicitud = IdSolicitud;
                    //var registroGrados = gradosPorSolicitud.Where(x=>x.ID_GRADO== request.grados[z].idGrado && x.CORR_ESTADISTICA== request.grados[z].corrEstadistica && x.ACTIVO==true).FirstOrDefault();
                    var registroGrados = gradosPorSolicitud.Where(x => x.ID_GRADO == request.grados[z].idGrado && x.ACTIVO == true).FirstOrDefault();

                    registroGrados.ID_ANIO = request.grados[z].idAnio;
                    registroGrados.COD_MOD = request.grados[z].codMod;
                    registroGrados.ANEXO = request.grados[z].anexo;
                    registroGrados.SITUACION_FINAL = request.grados[z].situacionFinal;

                    rows = await _unitOfWork.InsertarGradoCertificado(registroGrados);
                    if (rows > 0)
                    {
                        resultadoGrados++;
                    }
                }

                if (resultadoGrados != request.grados.Length)
                {
                    _unitOfWork.Rollback();
                    result.Success = false;
                    result.Data = 0;
                    result.Messages.Add("Ocurrió un problema al registrar los grados.");
                    return result;
                }

                if (request.observaciones.Length > 0)
                {



                    foreach (var item in observacionPorSolicitud.Select(Mappers.Certificado.ObservacionMapper.Map).ToList())
                    {
                        var registroObservaciones = request.observaciones.Where(x => x.idNivel == item.idNivel && x.idAnio == item.idAnio && x.resolucion == item.resolucion).FirstOrDefault();
                        if (registroObservaciones == null)
                        {
                            rows = await _unitOfWork.DeleteObservacionCertificado(item.idCertificadoObservacion);
                            if (rows > 0)
                            {

                            }
                        }

                    }


                    foreach (var item in request.observaciones)
                    {
                        item.idSolicitud = IdSolicitud;
                        item.idNivel = ReactEncryptationSecurity.Decrypt<string>(item.idNivel, "00");


                        var registroObservaciones = observacionPorSolicitud.Where(x => x.ID_NIVEL == item.idNivel && x.ID_ANIO == item.idAnio && x.RESOLUCION == item.resolucion).FirstOrDefault();
                        if (registroObservaciones == null)
                        {
                            rows = await _unitOfWork.InsertarObservacionCertificado(Mappers.Certificado.ObservacionCertificadoMapper.Map(item));
                            if (rows > 0)
                            {
                                resultadoObservaciones++;
                            }
                        }
                        else
                        {
                            rows = await _unitOfWork.InsertarObservacionCertificado(registroObservaciones);
                            if (rows > 0)
                            {
                                resultadoObservaciones++;
                            }
                        }
                    }

                    if (resultadoObservaciones != request.observaciones.Length)
                    {
                        _unitOfWork.Rollback();
                        result.Success = false;
                        result.Data = 0;
                        result.Messages.Add("Ocurrió un problema al registrar los grados.");
                        return result;
                    }
                }

                if (request.observaciones.Length == 0)
                {

                    if (observacionPorSolicitud.Count() > 0)
                    {
                        //delete observaciones for idsolicitud


                        var rowsDelAll = await _unitOfWork.DeleteObservacionCertificadoAll(IdSolicitud);

                    }


                }
                if (request.eliminados.Length > 0)
                {
                    var listaNotasxSolicitud = await _unitOfWork.ObtenerNotasPorSolicitud(IdSolicitud);

                    for (int i = 0; i < request.eliminados.Length; i++)
                    {
                        var noEliminados = request.notas.Where(x => x.IdArea == request.eliminados[i].IdArea && x.IdTipoArea == request.eliminados[i].IdTipoArea).ToList();
                        if (noEliminados.Count() == 0)
                        {
                            var notasPorInactivar = listaNotasxSolicitud.Where(x => x.ID_AREA == request.eliminados[i].IdArea && x.ACTIVO).ToList();
                            if (notasPorInactivar.Count() > 0)
                            {
                                foreach (var notasUpdate in notasPorInactivar)
                                {
                                    var inactivarNota = await _unitOfWork.InactivarNota(notasUpdate.ID_CONSTANCIA_NOTA, notasUpdate.ID_SOLICITUD);
                                }
                            }
                        }
                    }
                }


                int numRows = await _unitOfWork.UpdateEstadoEstudianteCertificado(request.notas[0].IdSolicitud, "4");

                if (numRows > 0)
                {
                    _unitOfWork.Commit();
                    result.Success = true;
                    result.Data = 1;
                    result.Messages.Add("Los Datos Academicos fueron registrados correctamente");
                    return result;
                }
                else
                {
                    _unitOfWork.Rollback();
                    result.Success = false;
                    result.Data = 0;
                    result.Messages.Add("Ocurrió un problema al registrar los datos academicos.");
                    return result;
                }

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                result.Success = false;
                result.Data = 0;
                result.Messages.Add("Se presentó un inconveniente al validar la información en el sistema.");
                return result;
            }
        }

        public async Task<StatusResponse> ObtenerGradosPorNivelPersona(Models.Certificado.ParametroModel objetoEncriptado)
        {
            var response = new StatusResponse();

            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro, "");
            var request = JsonConvert.DeserializeObject<Models.Certificado.EstudianteModalidadNivelRequest2>(desencriptarObjeto);

            var listaGrados = new List< Models.Certificado.GradoCertificadoResponse> ();

            int IdPersona = 0;

            if (request.idPersona.Length > 12)
            {
                IdPersona = ReactEncryptationSecurity.Decrypt<int>(request.idPersona, 0);
            }

            if (request.idModalidad.Length > 10)
            {
                request.idModalidad = ReactEncryptationSecurity.Decrypt<string>(request.idModalidad, "0");
            }

            if (request.idNivel.Length > 10)
            {
                request.idNivel = ReactEncryptationSecurity.Decrypt<string>(request.idNivel, "0");
            }

            try
            {

                var siagie = _encryptionServerSecurity.Decrypt<string>(
                ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", "")
                , "");
                var newRequest = new Models.Certificado.EstudianteModalidadNivelRequest2
                {
                    idPersona = (request.idPersona == "1") ? "1" : IdPersona.ToString(),
                    idModalidad = request.idModalidad,
                    idNivel = request.idNivel
                };

                var result = await SiagieGradosEstudiante(siagie, newRequest);
                if(result==null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Messages.Add("Ocurrió un error al consultar datos.");
                    return response;
                }
               
                response.Data = ReactEncryptationSecurity.Encrypt(JsonConvert.SerializeObject(result.ToList()));
                response.Success = true;
                response.Messages.Add("Consulta exitosa.");

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Messages.Add("Ocurrió un error al consultar datos.");
            }

            return response;
        }

        private Models.CorreoModel PrepararCorreo(Models.Certificado.SolicitudCertificadoModel solicitud, string nombresEstudiante, string correoElectronico)
        {
            string mensaje = "";
            if (solicitud.estadoSolicitud == "1")
            {
                mensaje = "<html><body>Estimad@ <strong>" + nombresEstudiante.ToUpper() + "</strong><br/><br/>" +
                   "Su Solicitud de Certificado Oficial de Estudios ha sido generado correctamente con " +
                   "<strong>Código Virtual N° " + solicitud.codigoVirtual + "</strong> a las " +
                   "<strong>" + solicitud.fechaCreacion.ToString("HH:mm", CultureInfo.CreateSpecificCulture("es-PE")) + " Hrs.</strong>, " +
                   "se ha remitido el resumen de su solicitud al correo electrónico registrado.<br/><br/>" +
                   //"para culminar con el proceso deberá de apersonarse a la I.E. luego de recibir el correo electrónico de confirmación de " +
                   //"la generación del Certificado de Estudios por parte del directivo de su IE.<br/><br/>" +
                   "Ministerio de Educación<br/></body></html>";
            }
            else if (solicitud.estadoSolicitud == "2")
            {
                mensaje = "<html><body>Estimad@ <strong>" + nombresEstudiante.ToUpper() + "</strong><br/><br/>" +
                   "Su Certificado Oficial de Estudios ha sido generado correctamente con " +
                   "<strong>Código Virtual N° " + solicitud.codigoVirtual + "</strong> a las " +
                   "<strong>" + solicitud.fechaActualizacion.ToString("HH:mm", CultureInfo.CreateSpecificCulture("es-PE")) + " Hrs.</strong>. " +
                   "Para culminar con el proceso deberá de apersonarse a la I.E. luego de haber recibido el presente correo electrónico de confirmación de " +
                   "la generación del Certificado de Estudios por parte del directivo de su IE.<br/><br/>" +
                   "Ministerio de Educación<br/></body></html>";
            }
            else
            {
                mensaje = "<html><body>Estimad@ <strong>" + nombresEstudiante.ToUpper() + "</strong><br/><br/>" +
                   "Su Certificado Oficial de Estudios ha sido rechazado por el siguiente motivo: " + solicitud.descripcionMotivo + "." +
                   "<strong>Código Virtual N° " + solicitud.codigoVirtual + "</strong> a las " +
                   "<strong>" + solicitud.fechaActualizacion.ToString("HH:mm", CultureInfo.CreateSpecificCulture("es-PE")) + " Hrs.</strong>, " +
                   "se ha remitido el resumen de su solicitud al correo electrónico registrado.<br/><br/>" +
                   "<br/></body></html>";
            }


            mensaje.Replace("'", "''");

            return new Models.CorreoModel()
            {
                para = correoElectronico,
                asunto = "Solicitud de Certificado Oficial de Estudios - MINEDU",
                mensaje = mensaje
            };
        }

        private async Task<bool> EnviarCorreo(Models.CorreoModel model)
        {
            try
            {
                return await _unitOfWork.EnviarCorreo(Mappers.CorreoMapper.Map(model));
            }
            catch
            {
                return false;
            }
        }

        public async Task<StatusResponse> ObtenerDatosEstudiante(Models.Certificado.ParametroModel objetoEncriptado)
        {//Models.Certificado.EstudiantePersonaRequest2 request
            var response = new StatusResponse();
            var desencriptarObjeto = ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametro,"");
            var request = JsonConvert.DeserializeObject<Models.Certificado.EstudiantePersonaRequest2>(desencriptarObjeto);            
            /*LOCALSTORAGE*/
            var estudianteModel = new Models.Certificado.EstudianteModel();
            DateTime fechaNacimiento;

            try
            {
                var estudianteRequest = new BusinessLogic.Models.Certificado.EstudianteRequest2()
                {
                    codEstudiante = request.numeroDocumento,
                    idNivel = ReactEncryptationSecurity.Decrypt<string>(request.idNivel, "00")
                };

                var solicitud = await _unitOfWork.ObtenerSolicitudesPendientes(request.tipoDocumento, request.numeroDocumento);
                if (solicitud.ToList().Where(x => x.ID_NIVEL == estudianteRequest.idNivel).Count() > 0)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Messages.Add("Usted actualmente tiene una Solicitud Pendiente por revisar. Diríjase a la Bandeja de Solicitudes Pendientes.");
                    return response;
                }

                var siagie = _encryptionServerSecurity.Decrypt<string>(
                            ReadRequest.getKeyValue<string>(_httpContextAccessor, "siagie", ""), "");

                var estudianteResponse = await _siagieService
                .GetServiceByQueryAndToken<StatusResponse, BusinessLogic.Models.Certificado.EstudianteRequest2>(siagie, "estudiantecodigo", estudianteRequest);

                if (estudianteResponse.Data != null)
                {
                    Models.ReniecPersona responseReniec = null;
                    var responseEstudiante = JsonConvert
                        .DeserializeObject<List<BusinessLogic.Models.Siagie.EstudianteMatriculaPorCodigoResponse>>(estudianteResponse.Data.ToString())
                        .FirstOrDefault();

                    if (responseEstudiante.codModular.Trim() != request.codigoModular.Trim())
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Messages.Add("La persona no pertenece o no ha culminado sus estudios en la institución educativa.");
                        return response;
                    }

                    if (responseEstudiante.numeroDocumento != null)
                    {
                        responseReniec = await _reniecService.ReniecConsultarPersona(responseEstudiante.numeroDocumento.Trim());
                        if (responseReniec.fecFallecimientoSpecified)
                        {
                            response.Data = null;
                            response.Success = false;
                            response.Messages.Add("No se permite registrar solicitud. El número documento pertenece a una persona fallecida.");
                            return response;
                        }
                        fechaNacimiento = Convert.ToDateTime(responseReniec.fecNacimiento.Trim().Substring(0, 10));
                    }
                    else
                    {
                        fechaNacimiento = Convert.ToDateTime(responseEstudiante.fecNacimiento.Trim().Substring(0, 10));
                    }

                    int edad = DateTime.Today.AddTicks(-fechaNacimiento.Ticks).Year - 1;

                    estudianteModel.numeroDocumento = responseReniec == null ? ReactEncryptationSecurity.Encrypt(responseEstudiante.numeroDocumento.Trim()) : ReactEncryptationSecurity.Encrypt(responseReniec.numDoc.Trim());
                    estudianteModel.codigoEstudiante = responseEstudiante.codigoEstudiante.Trim();
                    estudianteModel.apellidoMaterno = responseReniec == null ? responseEstudiante.apellidoMaterno.Trim() : responseReniec.apellidoMaterno.Trim();
                    estudianteModel.apellidoPaterno = responseReniec == null ? responseEstudiante.apellidoPaterno.Trim() : responseReniec.apellidoPaterno.Trim();
                    estudianteModel.nombres = responseReniec == null ? responseEstudiante.nombres : responseReniec.nombres.Trim();
                    estudianteModel.fechaNacimiento = fechaNacimiento.ToString("yyyy-MM-dd").Substring(0, 10);//responseReniec == null ? responseEstudiante.fecNacimiento : responseReniec.fecNacimiento.Trim().Substring(0, 10);
                    estudianteModel.nombrePadre = responseReniec == null ? "" : responseReniec.nombrePadre.Trim();
                    estudianteModel.nombreMadre = responseReniec == null ? "" : responseReniec.nombreMadre.Trim();
                    estudianteModel.dptoDomicilio = responseReniec == null ? "" : responseReniec.dptoDomicilio.Trim();
                    estudianteModel.ubigeoDomicilio = responseReniec == null ? responseEstudiante.ubigeoDomicilio : responseReniec.ubigeoDomicilio.Trim();
                    estudianteModel.provDomicilio = responseReniec == null ? "" : responseReniec.provDomicilio.Trim();
                    estudianteModel.distDomicilio = responseReniec == null ? "" : responseReniec.distDomicilio.Trim();
                    estudianteModel.ultimoAnio = responseEstudiante.ultimoAnio;
                    estudianteModel.idPersona = responseEstudiante.idPersona.Trim();
                    estudianteModel.idModalidad = responseEstudiante.idModalidad.Trim();
                    estudianteModel.codigoModular = responseEstudiante.codModular.Trim();
                    estudianteModel.idNivel = responseEstudiante.idNivel.Trim();
                    estudianteModel.idGrado = responseEstudiante.idGrado.Trim();
                    estudianteModel.dscGrado = responseEstudiante.dscGrado.Trim();
                    estudianteModel.esMenor = edad < 18 ? 1 : 0;
                }
                else
                {
                    if (request.tipoDocumento == "1")
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Messages.Add("No se encontraron datos de la Persona en SIAGIE.");
                        return response;
                    }
                    var consultaPersona = await _reniecService.ReniecConsultarPersona(request.numeroDocumento.Trim());

                    if (consultaPersona == null)
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Messages.Add("No se encontraron datos de la Persona en RENIEC.");
                        return response;
                    }
                    if (consultaPersona.fecFallecimientoSpecified)
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Messages.Add("No se permite registrar solicitud. El número documento pertenece a una persona fallecida.");
                        return response;
                    }

                    fechaNacimiento = Convert.ToDateTime(consultaPersona.fecNacimiento.Trim().Substring(0, 10));
                    int edad = DateTime.Today.AddTicks(-fechaNacimiento.Ticks).Year - 1;

                    estudianteModel.numeroDocumento = ReactEncryptationSecurity.Encrypt(consultaPersona.numDoc.Trim());
                    estudianteModel.apellidoMaterno = consultaPersona.apellidoMaterno.Trim();
                    estudianteModel.apellidoPaterno = consultaPersona.apellidoPaterno.Trim();
                    estudianteModel.nombres = consultaPersona.nombres.Trim();
                    estudianteModel.fechaNacimiento = consultaPersona.fecNacimiento.Trim();
                    estudianteModel.nombrePadre = consultaPersona.nombrePadre.Trim();
                    estudianteModel.nombreMadre = consultaPersona.nombreMadre.Trim();
                    estudianteModel.dptoDomicilio = consultaPersona.dptoDomicilio.Trim();
                    estudianteModel.ubigeoDomicilio = consultaPersona.ubigeoDomicilio.Trim();
                    estudianteModel.provDomicilio = consultaPersona.provDomicilio.Trim();
                    estudianteModel.distDomicilio = consultaPersona.distDomicilio.Trim();
                    estudianteModel.esMenor = edad < 18 ? 1 : 0;
                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Messages.Add("El documento no se encuentra registrado en RENIEC o SIAGIE.");
                return response;
            }


            response.Data = ReactEncryptationSecurity.Encrypt(JsonConvert.SerializeObject(estudianteModel));
            response.Success = true;
            response.Messages.Add("Consulta exitosa.");
            return response;
        }

    }
}
