using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface IAnioInstitucionQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_ANIOS_X_IE_Result>> Listar(USP_SEL_ANIOS_X_IE_Request filtro);
    }
}
