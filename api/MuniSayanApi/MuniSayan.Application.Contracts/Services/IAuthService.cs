using Minedu.Comun.Helper;
using MuniSayan.BusinessLogic.Models.Certificado;
using System.Threading.Tasks;

namespace MuniSayan.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<StatusResponse> ObtenerUsuario(AuthModel request);
        Task<StatusResponse> ObtenerRolCentralizado(RolCentralizadoRequest encryptedRequest);
        Task<StatusResponse> ObtenerRolesDescentralizados(RolDescentralizadoRequest encryptedRequest);
        Task<StatusResponse> ObtenerRolPorModular(UsuarioModularRequest encryptedRequest);
        Task<StatusResponse> ObtenerMenuNivelPorRol(RolRequest encryptedRequest);
        Task<StatusResponse> Login(AuthUserModel request);
    }
}
