using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minedu.Comun.Helper;
using MDS.Inventario.Api.Application.Contracts.Services;
using MDS.Inventario.Api.Utils;
using Models = MDS.Inventario.Api.Application.Entities.Models;

namespace MDS.Inventario.Api.Controllers
{
    //[EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MaestroController : ControllerBase
    {
        /*private readonly IMaestroService _maestroService;

        public MaestroController(IMaestroService maestroService)
        {
            _maestroService = maestroService;
        }

        // POST: api/auth/auth
        [HttpPost("cargo", Name = "GetLogin")]
        [Produces("application/json", Type = typeof(StatusResponse))]
        public async Task<IActionResult> GetLogin([FromBody] Models.Helpers.ParametroHelper encryptedRequest)
        {
            var response = await _authService.Login(encryptedRequest);

            return Ok(response);
        }*/
        
    }
}