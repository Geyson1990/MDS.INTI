using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface IDreQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_DRE_Result>> Listar();
    }
}
