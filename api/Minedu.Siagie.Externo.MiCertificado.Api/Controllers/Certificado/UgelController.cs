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
    public class UgelController : BaseController
    {
        private readonly IUgelService _ugelService;

        public UgelController(IUgelService ugelService)
        {
            _ugelService = ugelService;
        }

        [HttpGet]
        [Route("ugel")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<UgelResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarUgeles([FromQuery] UgelRequestDto ugelRequestDto)
        {
            var ugeles = await _ugelService.Listar(ugelRequestDto);
            var response = new StatusResponse<IEnumerable<UgelResponseDto>>()
            {
                Success = ugeles.Any() ? true : false,
                Data = ugeles.Any() ? ugeles : null,
                Message = ugeles.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }
    }
}