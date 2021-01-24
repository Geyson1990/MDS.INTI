using Minedu.Siagie.Identity.Dto.Security;

namespace Minedu.Siagie.Externo.MiCertificado.Dto.Response
{
    public class TokenResponseDto : BaseResponseDto
    {
        public AccessTokenDto Token { get; set; }
        public TokenResponseDto(bool success, string message, AccessTokenDto token) : base(success, message)
        {
            Token = token;
        }
    }
}
