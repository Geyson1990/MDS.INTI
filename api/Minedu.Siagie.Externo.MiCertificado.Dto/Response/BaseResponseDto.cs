using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Dto.Response
{
    public abstract class BaseResponseDto
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        public BaseResponseDto(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
