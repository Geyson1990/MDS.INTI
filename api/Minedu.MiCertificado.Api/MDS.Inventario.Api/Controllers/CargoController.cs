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
    public class CargoController : ControllerBase
    {
        private readonly ICargoService _cargoService;

        public CargoController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }


        //OK-S
        // POST: api/Cargo/insertar
        [HttpPost("insertar", Name = "PostInsertar")]
        [Produces("application/json", Type = typeof(StatusResponse))]
        public async Task<IActionResult> PostInsertar([FromBody] Models.Helpers.ParametroHelper encryptedRequest)
        {
            var response = await _cargoService.Insertar(encryptedRequest);

            return Ok(response);
        }

        
    }
}