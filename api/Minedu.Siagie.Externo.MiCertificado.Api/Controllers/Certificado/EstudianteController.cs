using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Dto;
using Minedu.Siagie.Externo.MiCertificado.Api.Utils;
using Minedu.Siagie.Externo.MiCertificado.Enumerado;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Controllers
{
    [Route("api/certificado")]
    public class EstudianteController : BaseController
    {
        private readonly IEstudianteService _estudianteService;
        private readonly IInstitucionEducativaService _institucionEducativaService;

        public EstudianteController(IEstudianteService estudianteService,
            IInstitucionEducativaService institucionEducativaService)
        {
            _estudianteService = estudianteService;
            _institucionEducativaService = institucionEducativaService;
        }        

        [HttpGet]
        [Route("estudiante")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarEstudiantesMatriculasConcluidas([FromQuery] EstudianteRequestDto estudianteRequestDto)
        {
            var response = new StatusResponse<IEnumerable<EstudianteResponseDto>>();
            ValidateRequest.ValidarDocumento(response, estudianteRequestDto.TipoDocumento, estudianteRequestDto.NumeroDocumento);
            if (response.Validations.Count > 0)
            {
                response.Success = false;
                return BadRequest(response);
            }
            var estudiante = await _estudianteService.Listar(estudianteRequestDto);
            var inicialCuna = estudiante.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(estudiante, inicialCuna, estudianteRequestDto.IdSistema);

            return Ok(respuesta);
        }

        [HttpGet]
        [Route("datosie")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteInfoPorCodModularResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarIePorCodigoModular([FromQuery] EstudianteInfoPorCodModularRequestDto alumnoRequestDto)
        {
            var ConsultarDatosIE = await _institucionEducativaService.ConsultarDatosIECunaJardin(alumnoRequestDto.CodigoModular, alumnoRequestDto.Anexo);
            if (ConsultarDatosIE.Success == false)
            {
                return BadRequest(ConsultarDatosIE);
            }
            var alumnos = await _estudianteService.ListarIePorCodigoModular(alumnoRequestDto);
            var inicialCuna = alumnos.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var response = ExceptResponse.ProcesarRespuesta(alumnos, inicialCuna, "");

            return Ok(response);
        }

        [HttpGet]
        [Route("informacionestudiante")]//EstudianteCodigo
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteMatriculaPorCodigoResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarInformacionEstudiante([FromQuery] EstudianteMatriculaPorCodigoRequestDto estudiantecodigoRequestDto)
        {
            var response = new StatusResponse<IEnumerable<EstudianteMatriculaPorCodigoResponseDto>>();
            ValidateRequest.ValidarDocumento(response, estudiantecodigoRequestDto.TipoDocumento, estudiantecodigoRequestDto.NumeroDocumento);
            if (response.Validations.Count > 0)
            {
                response.Success = false;
                return BadRequest(response);
            }
            var estudiantecodigo = await _estudianteService.ListarInformacionEstudiante(estudiantecodigoRequestDto);
            var inicialCuna = estudiantecodigo.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(estudiantecodigo, inicialCuna, "");

            return Ok(respuesta);
        }

        [HttpGet]
        [Route("estudiantesporaniogradoseccion")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteAnioGradoSeccionResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarEstudiantesPorAnioGradoSeccion([FromQuery] EstudianteAnioGradoSeccionRequestDto estudianteGradoSeccionRequestDto)
        {
            var ConsultarDatosIE = await _institucionEducativaService.ConsultarDatosIECunaJardin(estudianteGradoSeccionRequestDto.CodigoModular, estudianteGradoSeccionRequestDto.Anexo);
            if (ConsultarDatosIE.Success == false)
            {
                return BadRequest(ConsultarDatosIE);
            }
            var estudianteGradoSeccion = await _estudianteService.ListarEstudiantesPorAnioGradoSeccion(estudianteGradoSeccionRequestDto);
            var inicialCuna = estudianteGradoSeccion.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(estudianteGradoSeccion, inicialCuna, ""); ;

            return Ok(respuesta);
        }

        //[HttpGet]
        //[Route("estudiantesporaniogradoseccionEBA")]
        //[ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteAnioGradoSeccionResponseDto>>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> ListarEstudiantesPorAnioGradoSeccionEBA([FromQuery] EstudianteAnioGradoSeccionEBARequestDto estudianteGradoSeccionRequestDto)
        //{
        //    var estudianteGradoSeccion = await _estudianteService.ListarEstudiantesPorAnioGradoSeccionEBA(estudianteGradoSeccionRequestDto);
        //    var response = new StatusResponse<IEnumerable<EstudianteAnioGradoSeccionResponseDto>>()
        //    {
        //        Success = estudianteGradoSeccion.Any() ? true : false,
        //        Data = estudianteGradoSeccion.Any() ? estudianteGradoSeccion : null,
        //        Message = estudianteGradoSeccion.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
        //    };

        //    return Ok(response);
        //}

        /* [HttpGet]
         [Route("notaspendientes")]
         [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteNotaPendienteResponseDto>>), (int)HttpStatusCode.OK)]
         public async Task<IActionResult> ListarNotasPendientesPorEstudiantes([FromQuery]  EstudianteNotaPendienteRequestDto estudianteNotaPendienteRequestDto)
         {
             var estudianteNotaPendiente = await _estudianteService.ListarNotasPendientesPorEstudiantes(estudianteNotaPendienteRequestDto);
             var response = new StatusResponse<IEnumerable<EstudianteNotaPendienteResponseDto>>()
             {
                 Success = estudianteNotaPendiente.Any() ? true : false,
                 Data = estudianteNotaPendiente.Any() ? estudianteNotaPendiente : null,
                 Message = estudianteNotaPendiente.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
             };

             return Ok(response);
         }
         */
        [HttpGet]
        [Route("pdf/info")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteInformacionResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarInformacionEstudiante([FromQuery]  EstudianteInformacionRequestDto estudianteInformacionRequestDto)
        {
            var estudiante = await _estudianteService.ListarInformacionEstudiante(estudianteInformacionRequestDto);
            var inicialCuna = estudiante.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(estudiante, inicialCuna, estudianteInformacionRequestDto.IdSistema);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("pdf/grados")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteGradosResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarGradosEstudiante([FromQuery]  EstudianteGradosRequestDto request)
        {
            var estudiante = await _estudianteService.ListarGradosEstudiante(request);
            var inicialCuna = estudiante.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(estudiante, inicialCuna, request.IdSistema);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("pdf/notas")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteNotasResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarNotasEstudiante([FromQuery]  EstudianteNotasRequestDto request)
        {
            var estudiante = await _estudianteService.ListarNotasEstudiante(request);
            var inicialCuna = estudiante.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(estudiante, inicialCuna, request.IdSistema);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("pdf/observaciones")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteObservacionesResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarObservacionesEstudiante([FromQuery]  EstudianteObservacionesRequestDto request)
        {
            var estudiante = await _estudianteService.ListarObservacionesEstudiante(request);
            var inicialCuna = estudiante.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(estudiante, inicialCuna, request.IdSistema);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("pdf/notas/norma2020")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteNotas2020ResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarNotas2020Estudiante([FromQuery]  EstudianteNotas2020RequestDto request)
        {
            var estudiante = await _estudianteService.ListarNotas2020Estudiante(request);
            var inicialCuna = estudiante.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel==Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(estudiante, inicialCuna,"");
            return Ok(respuesta);
        }
        
        [HttpGet]
        [Route("estudiante/matricula/actual")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteMatriculaActualResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarMatriculaActualEstudiante([FromQuery]  EstudianteMatriculaActualRequestDto request)
        {
            var response = new StatusResponse<IEnumerable<EstudianteMatriculaActualResponseDto>>();
            ValidateRequest.ValidarDocumento(response, request.TipoDocumento, request.NumeroDocumento);
            if (response.Validations.Count > 0)
            {
                response.Success = false;
                return BadRequest(response);
            }
            var ConsultarDatosIE = await _institucionEducativaService.ConsultarDatosIECunaJardin(request.CodigoModular, request.Anexo);
            if (ConsultarDatosIE.Success == false)
            {
                return BadRequest(ConsultarDatosIE);
            }
            var estudiante = await _estudianteService.ListarMatriculaActualEstudiante(request);
            var inicialCuna = estudiante.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(estudiante, inicialCuna,"");

            return Ok(respuesta);
        }

        [HttpGet]
        [Route("estudiante/matricula/concluida")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteMatriculaConcluidaResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarMatriculaConcluidaEstudiante([FromQuery]  EstudianteMatriculaConcluidaRequestDto request)
        {
            var response = new StatusResponse<IEnumerable<EstudianteMatriculaConcluidaResponseDto>>();
            ValidateRequest.ValidarDocumento(response, request.TipoDocumento, request.NumeroDocumento);
            if (response.Validations.Count > 0)
            {
                response.Success = false;
                return BadRequest(response);
            }
            var ConsultarDatosIE = await _institucionEducativaService.ConsultarDatosIECunaJardin(request.CodigoModular, request.Anexo);
            if (ConsultarDatosIE.Success == false)
            {
                return BadRequest(ConsultarDatosIE);
            }
            var estudiante = await _estudianteService.ListarMatriculaConcluidaEstudiante(request);
            var inicialCuna = estudiante.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            if (estudiante.Where(x=>x.ConVida == 0).Count() > 0)
            {
                response.Success = estudiante.Any() ? true : false;
                response.Data = estudiante.Any() ? estudiante.Where(x => x.ConVida == 1) : null;
                response.Message = estudiante.Any() ? (estudiante.Where(x => x.ConVida == 0).Count() > 0 ? "El número de documento pertenece a una persona fallecida" : "Consulta exitosa") : "No se encontró información con los filtros ingresados.";
            }
            else
            {
                var respuesta = ExceptResponse.ProcesarRespuesta(estudiante, inicialCuna,"");
                return Ok(respuesta);
            }                       
            return Ok(response);
        }

        [HttpGet]
        [Route("persona/validarAnioMatricula")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteAnioEstudiosResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarAnioEstudiosEstudiante([FromQuery]  EstudianteAnioEstudiosRequestDto request)
        {
            var estudiante = await _estudianteService.ListarAnioEstudiosEstudiante(request);
            var response = new StatusResponse<IEnumerable<EstudianteAnioEstudiosResponseDto>>()
            {
                Success = estudiante.FirstOrDefault().Estado,
                Data = estudiante.Any() ? estudiante : null,
                Message = estudiante.FirstOrDefault().Estado ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("estudiante/datos/matricula")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteColegioNivelResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarEstudianteDatosMatricula([FromQuery] EstudianteModalidadNivelModularRequestDto colegioNivelRequestDto)
        {
            var response = new StatusResponse<IEnumerable<EstudianteColegioNivelResponseDto>>();
            var ConsultarDatosIE = await _institucionEducativaService.ConsultarDatosIECunaJardin(colegioNivelRequestDto.CodigoModular, colegioNivelRequestDto.Anexo);
            if (ConsultarDatosIE.Success == false)
            {
                return BadRequest(ConsultarDatosIE);
            }
            var colegiosPadrones = await _estudianteService.ListarEstudianteDatosMatriculas(colegioNivelRequestDto);
            response.Success = colegiosPadrones.Any() ? true : false;
            response.Data = colegiosPadrones.Any() ? colegiosPadrones : null;
            response.Message = colegiosPadrones.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados.";
            return Ok(response);
        }

        /*[HttpGet]
        [Route("estudiante/datos/matriculaPorNivel")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteColegioNivelResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarEstudianteDatosMatriculaPorNivel([FromQuery] EstudianteMatriculaNivelRequestDto colegioNivelRequestDto)
        {
            var colegiosPadrones = await _estudianteService.ListarEstudianteDatosMatriculasPorNivel(colegioNivelRequestDto);
            var response = new StatusResponse<IEnumerable<EstudianteColegioNivelResponseDto>>()
            {
                Success = colegiosPadrones.Any() ? true : false,
                Data = colegiosPadrones.Any() ? colegiosPadrones : null,
                Message = colegiosPadrones.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }
        */
        
        
        [HttpGet]
        [Route("estudiante/datospersonales")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteDatosPersonalesResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarEstudianteDatosPersonales([FromQuery] EstudianteDatosPersonalesRequestDto estudianteDatosPersonalesRequestDto)
        {
            var estudiante = await _estudianteService.ListarEstudianteDatosPersonales(estudianteDatosPersonalesRequestDto);
            var response = new StatusResponse<IEnumerable<EstudianteDatosPersonalesResponseDto>>()
            {
                Success = estudiante.Any() ? true : false,
                Data = estudiante.Any() ? estudiante : null,
                Message = estudiante.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };
            
            return Ok(response);
        }

    }
}