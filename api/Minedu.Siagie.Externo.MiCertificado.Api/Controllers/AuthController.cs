using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Api.Utils;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAnioInstitucionService _anioinstitucionService;
        private readonly IInstitucionEducativaService _institucionEducativaService;

        public AuthController(IAnioInstitucionService anioinstitucionService, 
            IInstitucionEducativaService institucionEducativaService)
        {
            _anioinstitucionService = anioinstitucionService;
            _institucionEducativaService = institucionEducativaService;
        }

        [HttpGet]
        [Route("login")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<AnioInstitucionResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarAniosPorInstitucionEducativa([FromQuery] AnioInstitucionRequestDto anioinstitucionRequestDto)
        {
            var response = new StatusResponse<IEnumerable<AnioInstitucionResponseDto>>();
            var ConsultarDatosIE = await _institucionEducativaService.ConsultarDatosIECunaJardin(anioinstitucionRequestDto.CodigoModular, anioinstitucionRequestDto.Anexo);
            if (ConsultarDatosIE.Success == false)
            {                
                return BadRequest(ConsultarDatosIE);
            }
            var aniosinstitucion = await _anioinstitucionService.Listar(anioinstitucionRequestDto);
            response.Success = aniosinstitucion.Any() ? true : false;
            response.Data = aniosinstitucion.Any() ? aniosinstitucion : null;
            response.Message = aniosinstitucion.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados.";
            return Ok(response);
        }
    }
}