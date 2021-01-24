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
    public class TipoAreaController : BaseController
    {
        private readonly ITipoAreaService _tipoareaService;

        public TipoAreaController(ITipoAreaService tipoareaService)
        {
            _tipoareaService = tipoareaService;
        }

        [HttpGet]
        [Route("tipoarea")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<TipoAreaResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarTiposAreas()
        {
            var tipoarea = await _tipoareaService.Listar();
            var response = new StatusResponse<IEnumerable<TipoAreaResponseDto>>()
            {
                Success = tipoarea.Any() ? true : false,
                Data = tipoarea.Any() ? tipoarea : null,
                Message = tipoarea.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }
    }
}