using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface IDepartamentoQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_DEPARTAMENTO_Result>> Listar();

        Task<IEnumerable<USP_SEL_DEPARTAMENTO_SIAGIE_Result>> ListarDepartamentosSIAGIE();

    }
}
