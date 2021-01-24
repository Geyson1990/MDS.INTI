using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface IInstitucionEducativaQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_IE_Result>> Listar(USP_SEL_IE_Request filtro);
        Task<IEnumerable<USP_SEL_IE_CODMOD_Result>> ListarDatosInstitucionesEducativas(USP_SEL_IE_CODMOD_Request filtro);
        Task<IEnumerable<SEL_DATOS_INSTITUCION_EDUCATIVA_Result>> ListarInstitucionEducativaPorCodigoModular(SEL_DATOS_INSTITUCION_EDUCATIVA_Request filtro);
        Task<IEnumerable<USP_SEL_DATOS_IE_X_NIVEL_Result>> ListarInstitucionEducativaNiveles(USP_SEL_DATOS_IE_X_NIVEL_Request filtro);
        Task<IEnumerable<USP_SEL_IIEE_PADRON_Result>> ListarInstitucionEducativaPorPadrones(USP_SEL_IIEE_PADRON_Request filtro);
    }
}
