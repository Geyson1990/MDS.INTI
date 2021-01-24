namespace Minedu.Siagie.Externo.MiCertificado.Domain.Model
{
    public class TokenConfigurations
    {
        public string SecretKey { get; set; }
        public string Encryptkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int AccessTokenExpiration { get; set; }
        public long RenewTokenExpiration { get; set; }
        public string ClientToken { get; set; }
        public string ClientMiCertificadoToken { get; set; }
        public string Ticket { get; set; }
    }
}
