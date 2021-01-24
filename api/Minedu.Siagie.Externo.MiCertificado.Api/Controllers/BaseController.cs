using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minedu.Siagie.Externo.MiCertificado.Api.Utils;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {

    }
}