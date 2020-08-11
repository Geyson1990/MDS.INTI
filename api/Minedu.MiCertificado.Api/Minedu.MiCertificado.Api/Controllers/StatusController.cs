﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Minedu.MiCertificado.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [EnableCors]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Mi-Certificado API v1.0 is Online";
        }
    }
}