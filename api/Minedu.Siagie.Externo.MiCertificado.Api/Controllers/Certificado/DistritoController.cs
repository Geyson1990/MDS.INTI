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
    public class DistritoController : BaseController
    {
        private readonly IDistritoService _distritoService;

        public DistritoController(IDistritoService distritoService)
        {
            _distritoService = distritoService;
        }

        [HttpGet]
        [Route("distritos")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<DistritoResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarDistritos([FromQuery] DistritoRequestDto ubigeoRequestDto)
        {
            var distritos = await _distritoService.Listar(ubigeoRequestDto);
            var response = new StatusResponse<IEnumerable<DistritoResponseDto>>()
            {
                Success = distritos.Any() ? true : false,
                Data = distritos.Any() ? distritos : null,
                Message = distritos.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("distritos/siagie")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<DistritoResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarDistritosSIAGIE([FromQuery] DistritoRequestDto ubigeoRequestDto)
        {
            var distritossiagie = await _distritoService.ListarDistritosSIAGIE(ubigeoRequestDto);
            var response = new StatusResponse<IEnumerable<DistritoResponseDto>>()
            {
                Success = distritossiagie.Any() ? true : false,
                Data = distritossiagie.Any() ? distritossiagie : null,
                Message = distritossiagie.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }
    }
}