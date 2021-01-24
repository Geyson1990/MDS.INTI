using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface IUgelQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_UGEL_Result>> Listar(USP_SEL_UGEL_Request filtro);
    }
}
