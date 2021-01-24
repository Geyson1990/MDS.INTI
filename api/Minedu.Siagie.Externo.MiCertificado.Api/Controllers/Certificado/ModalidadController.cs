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
    public class ModalidadController : BaseController
    {
        private readonly IModalidadService _modalidadService;

        public ModalidadController(IModalidadService modalidadService)
        {
            _modalidadService = modalidadService;
        }

        [HttpGet]
        [Route("modalidades")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<ModalidadResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarModalidadesAutorizadas()
        {
            var modalidad = await _modalidadService.Listar();
            var response = new StatusResponse<IEnumerable<ModalidadResponseDto>>()
            {
                Success = modalidad.Any() ? true: false,
                Data = modalidad.Any() ? modalidad : null,
                Message = modalidad.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }
    }
}