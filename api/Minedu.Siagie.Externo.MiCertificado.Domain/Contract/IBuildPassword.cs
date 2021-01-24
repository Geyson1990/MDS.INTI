namespace Minedu.Siagie.Externo.MiCertificado.Domain.Contract
{
    public interface IBuildPassword
    {
        string Hash(string password);
        bool Matches(string providedPassword, string passwordHash);
    }
}
