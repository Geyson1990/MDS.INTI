using MuniSayan.BusinessLogic.Models;
using System.Threading.Tasks;

namespace MuniSayan.Application.Contracts.Services
{
    public interface IReniecService
    {
        Task<ReniecPersona> ReniecConsultarPersona(string nroDocumento);
    }
}
