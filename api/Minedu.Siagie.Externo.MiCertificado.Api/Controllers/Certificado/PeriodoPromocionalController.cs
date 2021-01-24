using Microsoft.AspNetCore.Mvc;
using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Controllers
{
    [Route("api/certificado")]
    public class PeriodoPromocionalController : BaseController
    {
        private readonly IPeriodoPromocionalService _periodoPromocionalService;

        public PeriodoPromocionalController(IPeriodoPromocionalService periodoPromocionalService)
        {
            _periodoPromocionalService = periodoPromocionalService;
        }

        //[HttpGet]
        //[Route("periodospromocionales")]
        //[ProducesResponseType(typeof(StatusResponse<IEnumerable<PeriodoPromocionalResponseDto>>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> ListarPeriodosPromocionales([FromQuery] PeriodoPromocionalDto periodoPromocionalDto)
        //{
        //    var respuesta = await _periodoPromocionalService.ListarPeriodosPromocionales(periodoPromocionalDto);
        //    var response = new StatusResponse<IEnumerable<PeriodoPromocionalResponseDto>>()
        //    {
        //        Success = respuesta.Any() ? true : false,
        //        Data = respuesta.Any() ? respuesta : null,
        //        Message = respuesta.Any() ? "Consulta exitosa" : "No se encontró períodos promocionales."
        //    };

        //    return Ok(response);
        //}
    }
}