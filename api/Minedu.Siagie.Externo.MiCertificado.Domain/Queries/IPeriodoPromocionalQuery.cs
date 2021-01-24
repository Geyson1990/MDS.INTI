using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface IPeriodoPromocionalQuery : IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_PERIODOS_PROMOCIONALES_EBA_Result>> ListarPeriodosPromocionales(USP_SEL_PERIODOS_PROMOCIONALES_EBA_Request filtro);
    }
}
