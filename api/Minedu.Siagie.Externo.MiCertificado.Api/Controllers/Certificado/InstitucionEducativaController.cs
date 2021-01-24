using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Api.Utils;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Dto;
using Minedu.Siagie.Externo.MiCertificado.Enumerado;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Controllers
{
    [Route("api/certificado")]
    public class InstitucionEducativaController : BaseController
    {
        private readonly IInstitucionEducativaService _institucioneducativaService;

        public InstitucionEducativaController(IInstitucionEducativaService institucioneducativaService)
        {
            _institucioneducativaService = institucioneducativaService;
        }

        /*[HttpGet]
        [Route("institucioneducativa")]//institucioneducativa
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<InstitucionEducativaResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarInstitucionesEducativas([FromQuery] InstitucionEducativaRequestDto institucioneducativaRequestDto)
        {
            var institucioneseducativas = await _institucioneducativaService.Listar(institucioneducativaRequestDto);
            var response = new StatusResponse<IEnumerable<InstitucionEducativaResponseDto>>()
            {
                Success = true,
                Data = institucioneseducativas
            };

            return Ok(response);
        }*/

        [HttpGet]
        [Route("datosinstitucioneducativa")]//datosinstitucioneducativa
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<InstitucionEducativaPorCodigoResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarDatosInstitucionesEducativas([FromQuery] InstitucionEducativaPorCodigoRequestDto institucioneducativaRequestDto)
        {
            var ConsultarDatosIE = await _institucioneducativaService.ConsultarDatosIECunaJardin(institucioneducativaRequestDto.CodigoModular, institucioneducativaRequestDto.anexo);
            if (ConsultarDatosIE.Success == false)
            {
                return BadRequest(ConsultarDatosIE);
            }
            var datosinstitucioneseducativas = await _institucioneducativaService.ListarDatosInstitucionesEducativas(institucioneducativaRequestDto);
            var inicialCuna = datosinstitucioneseducativas.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(datosinstitucioneseducativas, inicialCuna, "");

            return Ok(respuesta);
        }

        [HttpGet]
        [Route("institucioneducativa/datos")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<InstitucionEducativaPorDreUgelResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarInstitucionEducativaPorCodigoModular([FromQuery] InstitucionEducativaPorDreUgelRequestDto institucioneducativaRequestDto)
        {
            var response = new StatusResponse<IEnumerable<InstitucionEducativaPorDreUgelResponseDto>>();
            ValidateRequest.ValidarInputIE(response, institucioneducativaRequestDto);
            if (response.Validations.Count > 0)
            {
                response.Success = false;
                return BadRequest(response);
            }
            var ConsultarDatosIE = await _institucioneducativaService.ConsultarDatosIECunaJardin(institucioneducativaRequestDto.CodigoModular, institucioneducativaRequestDto.Anexo);
            if (ConsultarDatosIE.Success == false)
            {
                return BadRequest(ConsultarDatosIE);
            }
            var datosinstitucioneseducativas = await _institucioneducativaService.ListarInstitucionEducativaPorCodigoModular(institucioneducativaRequestDto);
            var inicialCuna = datosinstitucioneseducativas.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(datosinstitucioneseducativas, inicialCuna, "");

            return Ok(respuesta);
        }

        [HttpGet]
        [Route("colegio/datos")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<EstudianteColegioNivelResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarInstitucionEducativaNiveles([FromQuery] EstudianteModalidadNivelRequestDto colegioRequestDto)
        {
            var colegios = await _institucioneducativaService.ListarInstitucionEducativaNiveles(colegioRequestDto);
            var inicialCuna = colegios.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(colegios, inicialCuna, colegioRequestDto.IdSistema);

            return Ok(respuesta);
        }

        [HttpGet]
        [Route("iiee")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<ColegioPadronResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarInstitucionEducativaPorPadrones([FromQuery] ColegioRequestDto colegioRequestDto)
        {
            var response = new StatusResponse<IEnumerable<ColegioPadronResponseDto>>();
            ValidateRequest.ValidarIngresoIE(response, colegioRequestDto);
            if (response.Validations.Count > 0)
            {
                response.Success = false;
                return BadRequest(response);
            }
            var ConsultarDatosIE = await _institucioneducativaService.ConsultarDatosIECunaJardin(colegioRequestDto.CodigoModular, colegioRequestDto.Anexo);
            if (ConsultarDatosIE.Success == false)
            {
                return BadRequest(ConsultarDatosIE);
            }
            var colegiosPadrones = await _institucioneducativaService.ListarInstitucionEducativaPorPadrones(colegioRequestDto);
            var inicialCuna = colegiosPadrones.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(colegiosPadrones, inicialCuna, "");

            return Ok(respuesta);
        }        

    }
}