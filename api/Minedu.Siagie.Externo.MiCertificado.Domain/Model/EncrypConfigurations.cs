using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Model
{
    public class EncrypConfigurations
    {
        public SecurityKey SecurityKey { get; }
        public EncryptingCredentials EncryptingCredentials { get; }
        public EncrypConfigurations(string key)
        {
            var encrypKey = Encoding.ASCII.GetBytes(key);
            SecurityKey = new SymmetricSecurityKey(encrypKey);
            EncryptingCredentials = new EncryptingCredentials(SecurityKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        }
    }
}
