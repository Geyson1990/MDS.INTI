using MuniSayan.BusinessLogic.Models;
using MuniSayan.BusinessLogic.Models.Siagie;
using System.Threading.Tasks;

namespace MuniSayan.Application.Contracts.Services
{
    public interface ISiagieService
    {
        Task<T> GetServiceResponseById<T>(string controller, int id);
        Task<T> PostServiceToken<T>(TokenRequest request);
        Task<T> GetServiceByQueryAndToken<T, Y>(string token, string method, Y filter);
    }
}
