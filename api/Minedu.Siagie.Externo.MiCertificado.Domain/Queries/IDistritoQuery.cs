using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface IDistritoQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_DISTRITO_Result>> Listar(USP_SEL_DISTRITO_Request filtro);
        Task<IEnumerable<USP_SEL_DISTRITO_SIAGIE_Result>> ListarDistritosSIAGIE(USP_SEL_DISTRITO_SIAGIE_Request filtro);
    }
}
