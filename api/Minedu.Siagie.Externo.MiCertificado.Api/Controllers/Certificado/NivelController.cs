using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Api.Utils;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Dto;
using Minedu.Siagie.Externo.MiCertificado.Enumerado;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Controllers
{
    [Route("api/certificado")]
    public class NivelController : BaseController
    {
        private readonly INivelService _nivelService;

        public NivelController(INivelService nivelService)
        {
            _nivelService = nivelService;
        }
        
        [HttpGet]
        [Route("niveles")]
        [ProducesResponseType(typeof(StatusResponse<IEnumerable<NivelResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListarEstudiantesNiveles([FromQuery] ModalidadRequestDto estudiantemodalidadRequestDto)
        {
            var estudiantenivel = await _nivelService.Listar(estudiantemodalidadRequestDto);
            var inicialCuna = estudiantenivel.Where(x => x.IdNivel == Constantes.INICIAL_CUNA || x.IdNivel == Constantes.EBE_PRITE).Select(c => new NivelRequestDto { IdNivel = c.IdNivel });
            var respuesta = ExceptResponse.ProcesarRespuesta(estudiantenivel, inicialCuna, estudiantemodalidadRequestDto.IdSistema);

            return Ok(respuesta);
        }
    }
}