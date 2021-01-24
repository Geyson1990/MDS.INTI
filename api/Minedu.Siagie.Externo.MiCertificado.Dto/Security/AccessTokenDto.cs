using System;

namespace Minedu.Siagie.Identity.Dto.Security
{
    public class AccessTokenDto: TokenDto
    {
        public RenewTokenDto RenewToken { get; private set; }

        public AccessTokenDto(string token, long expiration, RenewTokenDto renewToken) : base(token, expiration)
        {
            if (renewToken == null)
                throw new ArgumentException("Ingresar un token de renovación válido.");

            RenewToken = renewToken;
        }
    }
}
