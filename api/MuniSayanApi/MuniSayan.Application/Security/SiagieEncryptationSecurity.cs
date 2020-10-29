using Minedu.Core.General.Communication.Security;
using System;

namespace MuniSayan.Application.Security
{
    public class SiagieEncryptationSecurity
    {
        private const string Key = "b5000df8893a5117b2b55c3c91f21ff30190b1e41563c957e4ede0a65c5421b5";

        public static string TryEncrypt(string input)
        {
            if (input == null)
            {
                return input;
            }
            return Encrypt.Execute(input, Key);
        }

        public static T TryDecrypt<T>(string input, T porDefecto)
        {
            try
            {
                if (input == null)
                {
                    return porDefecto;
                }
                return (T)Convert.ChangeType(Decrypt.Execute(input, Key), typeof(T));
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
