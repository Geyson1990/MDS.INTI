using Minedu.Comun.IData;
using MuniSayan.DataAccess.Contracts.Entities.Constancia;
using MuniSayan.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuniSayan.DataAccess.Contracts.UnitOfWork
{
    public partial interface IUnitOfWork : IBaseUnitOfWork
    {
        Task<IEnumerable<MenuEntity>> ObtenerMenu(MenuEntity entity);
        Task<IEnumerable<DeclaracionJuradaEntity>> ObtenerDeclaracionJurada();
        Task<IEnumerable<MotivoEntity>> ObtenerMotivo(MotivoEntity entity);
        Task<bool> EnviarCorreo(CorreoEntity entity);
    }
}
