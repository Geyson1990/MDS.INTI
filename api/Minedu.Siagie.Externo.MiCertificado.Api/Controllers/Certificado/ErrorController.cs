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
    [Route("api")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet("Error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Get()
        {
            var response = new StatusResponse
            {
                Success = false,
            };

            response.Validations.Add(new MessageStatusResponse("MiCertificado v1.0.0.0 Solicitud Incorrecta", "01"));
            return Ok(response);
        }
    }
}