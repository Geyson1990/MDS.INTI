using Models = MDS.Inventario.Api.Application.Entities.Models;
using Minedu.Comun.Helper;
using System.Threading.Tasks;

namespace MDS.Inventario.Api.Application.Contracts.Services
{
    public interface ICargoService
    {
        Task<StatusResponse> Insertar(Models.CargoExtends objetoEncriptado);
    }
}
