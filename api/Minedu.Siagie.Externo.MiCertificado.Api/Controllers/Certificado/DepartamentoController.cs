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
    public class DepartamentoController : BaseController
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet]
        [Route("departamentos")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<DepartamentoResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarDepartamentos()
        {
            var departamentos = await _departamentoService.Listar();
            var response = new StatusResponse<IEnumerable<DepartamentoResponseDto>>()
            {
                Success = departamentos.Any() ? true : false,
                Data = departamentos.Any() ? departamentos : null,
                Message = departamentos.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("departamentos/siagie")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<DepartamentoResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarDepartamentosSIAGIE()
        {
            var departamentossiagie = await _departamentoService.ListarDepartamentosSIAGIE();
            var response = new StatusResponse<IEnumerable<DepartamentoResponseDto>>()
            {
                Success = departamentossiagie.Any() ? true : false,
                Data = departamentossiagie.Any() ? departamentossiagie : null,
                Message = departamentossiagie.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }

        



    }
}