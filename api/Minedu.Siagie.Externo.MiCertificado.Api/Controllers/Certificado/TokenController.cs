using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Dto.Request;
using Minedu.Siagie.Identity.Application.Contract;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJsonWebTokenService _iJsonWebTokenService;

        public TokenController(IJsonWebTokenService iJsonWebTokenService)
        {
            _iJsonWebTokenService = iJsonWebTokenService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(StatusResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] TokenRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _iJsonWebTokenService.Create(dto);
            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
