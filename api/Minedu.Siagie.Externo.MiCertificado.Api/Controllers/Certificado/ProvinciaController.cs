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
    public class ProvinciaController : BaseController
    {
        private readonly IProvinciaService _provinciaService;

        public ProvinciaController(IProvinciaService provinciaService)
        {
            _provinciaService = provinciaService;
        }

        [HttpGet]
        [Route("provincias")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<ProvinciaResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarProvincias([FromQuery] ProvinciaRequestDto ubigeoRequestDto)
        {
            var provincias = await _provinciaService.Listar(ubigeoRequestDto);
            var response = new StatusResponse<IEnumerable<ProvinciaResponseDto>>()
            {
                Success = provincias.Any() ? true : false,
                Data = provincias.Any() ? provincias : null,
                Message = provincias.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("provincias/siagie")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<ProvinciaResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarProvinciasSIAGIE([FromQuery] ProvinciaRequestDto ubigeoRequestDto)
        {
            var provinciassiagie = await _provinciaService.ListarProvinciasSIAGIE(ubigeoRequestDto);
            var response = new StatusResponse<IEnumerable<ProvinciaResponseDto>>()
            {
                Success = provinciassiagie.Any() ? true : false,
                Data = provinciassiagie.Any() ? provinciassiagie : null,
                Message = provinciassiagie.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }

    }
}