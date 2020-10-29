using Minedu.Comun.IData;
using MuniSayan.DataAccess.Contracts.Entities.Certificado;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuniSayan.DataAccess.Contracts.UnitOfWork
{
    public partial interface IUnitOfWork : IBaseUnitOfWork
    {
        //Task<IEnumerable<IEEntity>> GetIE(IEEntity entity);
        //Task<IEnumerable<IEEntity>> ObtenerRolActivo(IEEntity entity);
        Task<IEnumerable<MenuNivelRolEntity>> ObtenerMenuNivelPorRol(string ID_ROL);
    }
}
