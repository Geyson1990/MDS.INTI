using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface IGradoQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_GRADOS_X_NIVEL_Result>> ListarNivel(USP_SEL_GRADOS_X_NIVEL_Request filtro);
        Task<IEnumerable<USP_SEL_GRADO_SECCION_Result>> ListarSeccion(USP_SEL_GRADO_SECCION_Request filtro);
        Task<IEnumerable<USP_SEL_GRADO_SECCION_Result>> ListarSeccionEBA(USP_SEL_GRADO_SECCION_EBA_Request filtro);
    }
}
