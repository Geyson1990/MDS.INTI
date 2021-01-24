using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Controllers
{
    [Route("api/certificado")]
    public class GradoNivelController : BaseController
    {
        private readonly IGradoService _gradosService;
        private readonly IInstitucionEducativaService _institucionEducativaService;

        public GradoNivelController(IGradoService gradosService,
            IInstitucionEducativaService institucionEducativaService)
        {
            _gradosService = gradosService;
            _institucionEducativaService = institucionEducativaService;
        }

        [HttpGet]
        [Route("grado")]//maestro/grado --> PostGrados
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<GradoNivelResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarGradosNiveles([FromQuery] GradoNivelRequestDto gradonivelRequestDto)
        {
            var gradosniveles = await _gradosService.ListarNivel(gradonivelRequestDto);
            var response = new StatusResponse<IEnumerable<GradoNivelResponseDto>>()
            {
                Success = gradosniveles.Any() ? true : false,
                Data = gradosniveles.Any() ? gradosniveles : null,
                Message = gradosniveles.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("gradoseccion")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<GradoSeccionResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarGradosSecciones([FromQuery] GradoSeccionRequestDto gradoseccionRequestDto)
        {
            var ConsultarDatosIE = await _institucionEducativaService.ConsultarDatosIECunaJardin(gradoseccionRequestDto.CodigoModular, gradoseccionRequestDto.Anexo);
            if (ConsultarDatosIE.Success == false)
                return BadRequest(ConsultarDatosIE);
            
            var gradossecciones = await _gradosService.ListarSeccion(gradoseccionRequestDto);
            var response = new StatusResponse<IEnumerable<GradoSeccionResponseDto>>()
            {
                Success = gradossecciones.Any() ? true : false,
                Data = gradossecciones.Any() ? gradossecciones : null,
                Message = gradossecciones.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }

        //[HttpGet]
        //[Route("gradoseccionEBA")]
        //[ProducesResponseType(typeof(StatusResponse<IEnumerable<GradoSeccionResponseDto>>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> ListarGradosSecciones([FromQuery] GradoSeccionEBARequestDto gradoseccionEBARequestDto)
        //{
        //    var gradossecciones = await _gradosService.ListarSeccionEBA(gradoseccionEBARequestDto);
        //    var response = new StatusResponse<IEnumerable<GradoSeccionResponseDto>>()
        //    {
        //        Success = gradossecciones.Any() ? true : false,
        //        Data = gradossecciones.Any() ? gradossecciones : null,
        //        Message = gradossecciones.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
        //    };

        //    return Ok(response);
        //}
    }
}