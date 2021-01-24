using System;

namespace Minedu.Siagie.Identity.Dto.Security
{
    public abstract class TokenDto
    {
        public string Token { get; protected set; }
        public long Renew { get; protected set; }

        public TokenDto(string token, long renew)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token inválido.");

            if (renew <= 0)
                throw new ArgumentException("Token Expirado.");

            Token = token;
            Renew = renew;
        }

        public bool Expired() => DateTime.UtcNow.Ticks > Renew;
    }
}
