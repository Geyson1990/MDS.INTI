using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface IProvinciaQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_PROVINCIA_Result>> Listar(USP_SEL_PROVINCIA_Request filtro);

        Task<IEnumerable<USP_SEL_PROVINCIA_SIAGIE_Result>> ListarProvinciasSIAGIE(USP_SEL_PROVINCIA_SIAGIE_Request filtro);
    }
}
