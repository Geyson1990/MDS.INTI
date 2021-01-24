using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface ITipoAreaQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_TIPO_AREA_Result>> Listar();
    }
}
