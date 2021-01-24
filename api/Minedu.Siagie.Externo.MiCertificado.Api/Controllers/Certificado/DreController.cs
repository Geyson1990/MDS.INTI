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
    public class DreController : BaseController
    {
        private readonly IDreService _dreService;

        public DreController(IDreService dreService)
        {
            _dreService = dreService;
        }

        [HttpGet]
        [Route("dre")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<DreResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarDres()
        {
            var dres = await _dreService.Listar();
            var response = new StatusResponse<IEnumerable<DreResponseDto>>()
            {
                Success = dres.Any() ? true : false,
                Data = dres.Any() ? dres : null,
                Message = dres.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }
    }
}