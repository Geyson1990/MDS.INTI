using Microsoft.AspNetCore.Http;
using MuniSayan.Application.Contracts.Security;

namespace MuniSayan.Application.Utils
{
    public class JwTReader
    {
        public static T Leerkey<T>(
           IEncryptionServerSecurity encryptionServerSecurity,
           IHttpContextAccessor httpContextAccessor,
           string key,
           T porDefecto)
        {
            return encryptionServerSecurity.Decrypt<T>(
                ReadRequest.getKeyValue<string>(httpContextAccessor, key, ""),
                porDefecto);
        }
    }
}
