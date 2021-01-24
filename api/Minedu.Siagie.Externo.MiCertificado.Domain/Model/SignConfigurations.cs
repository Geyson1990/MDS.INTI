using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Model
{
    public class SignConfigurations
    {
        public SecurityKey SecurityKey { get; }
        public SigningCredentials SignCredentials { get; }
        public SignConfigurations(string key)
        {
            var secretKey = Encoding.ASCII.GetBytes(key);

            SecurityKey = new SymmetricSecurityKey(secretKey);
            SignCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
        }



    }
}
