using System.Collections;
using System.Collections.Generic;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Utils
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public Dictionary<string, ArrayList> Errors { get; set; } = new Dictionary<string, ArrayList>();
    }
}
