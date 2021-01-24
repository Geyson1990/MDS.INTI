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
    public class ApoderadoController : BaseController
    {
        private readonly IApoderadoService _apoderadoEstudianteService;

        public ApoderadoController(IApoderadoService apoderadoEstudianteService)
        {
            _apoderadoEstudianteService = apoderadoEstudianteService;
        }

        [HttpGet]
        [Route("apoderado")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<ApoderadoResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarApoderadosEstudiantes([FromQuery] ApoderadoRequestDto apoderadosEstudiantesRequestDto)
        {
            var response = new StatusResponse<IEnumerable<ApoderadoResponseDto>>();
            ValidarDocumento(response, apoderadosEstudiantesRequestDto.TipoDocumento, apoderadosEstudiantesRequestDto.NumeroDocumento);
            if (response.Validations.Count > 0)
            {
                response.Success = false;
                return BadRequest(response);
            }
            var apoderadosEstudiantes = await _apoderadoEstudianteService.Listar(apoderadosEstudiantesRequestDto);
            response.Success = apoderadosEstudiantes.Any() ? true : false;
            response.Data = apoderadosEstudiantes.Any() ? apoderadosEstudiantes : null;
            response.Message = apoderadosEstudiantes.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados.";

            return Ok(response);
        }

        [HttpGet]
        [Route("apoderado/estudiante")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<ApoderadoEstudianteResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarApoderadosEstudiantesConMatricula([FromQuery] ApoderadoEstudianteRequestDto apoderadosEstudiantesRequestDto)
        {
            var apoderadosEstudiantes = await _apoderadoEstudianteService.ListarApoderadosEstudiantesConMatricula(apoderadosEstudiantesRequestDto);
            var response = new StatusResponse<IEnumerable<ApoderadoEstudianteResponseDto>>()
            {
                Success = apoderadosEstudiantes.Any() ? true : false,
                Data = apoderadosEstudiantes.Any() ? apoderadosEstudiantes : null,
                Message = apoderadosEstudiantes.Any() ? "Consulta exitosa" : "No se encontró información con los filtros ingresados."
            };

            return Ok(response);
        }

        private void ValidarDocumento(StatusResponse response, string tipoDocumento, string numeroDocumento)
        {
            switch (tipoDocumento.Trim())
            {
                case "2": // DNI
                    {
                        if (numeroDocumento.Trim().Length != 8)
                        {
                            response.Validations.Add(new MessageStatusResponse("El número de documento debe tener 8 caracteres", "DNI"));
                        }
                        break;
                    }
                default:
                    response.Validations.Add(new MessageStatusResponse("Campo tipo de documento no válido; debe ser 2.", "Tipo de documento"));
                    break;
            }
        }
    }
}
