using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface INivelQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_EXTERNO_CERTIFICADO_SEL_NIVELES_ALUMNO_Result>> Listar(USP_EXTERNO_CERTIFICADO_SEL_NIVELES_ALUMNO_Request filtro);
    }
}
