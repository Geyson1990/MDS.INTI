using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Dto.Request;
using Minedu.Siagie.Externo.MiCertificado.Dto.Response;
using System.Threading.Tasks;

namespace Minedu.Siagie.Identity.Application.Contract
{
    public interface IJsonWebTokenService
    {
        Task<StatusResponse<string>> Create(TokenRequestDto dto);
        Task<TokenResponseDto> Renew(string key, string userEmail);
        void Revoke(string key);
        Task</*TokenResponseDto*/string> Read(TokenDecrypDto dto);
    }
}
