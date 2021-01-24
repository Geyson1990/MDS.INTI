using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface IApoderadoQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_APODERADOS_X_DOCUMENTO_Result>> Listar(USP_SEL_APODERADOS_X_DOCUMENTO_Request filtro);
        Task<IEnumerable<USP_SEL_APODERADO_CON_ESTUDIANTE_Result>> ListarApoderadosEstudiantesConMatricula(USP_SEL_APODERADO_CON_ESTUDIANTE_Request filtro);
    }
}
