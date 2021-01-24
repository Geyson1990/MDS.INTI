using Minedu.Core.NetConnect.SQL.Data;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Domain.Queries;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Data.Queries
{
    public class PeriodoPromocionalUoW : BaseUnitOfWork, IPeriodoPromocionalQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public PeriodoPromocionalUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_SEL_PERIODOS_PROMOCIONALES_EBA_Result>> ListarPeriodosPromocionales(USP_SEL_PERIODOS_PROMOCIONALES_EBA_Request filtro)
        {
            var parm = new Parameter[]
           {
                new Parameter("@COD_MOD", filtro.COD_MOD),
                new Parameter("@ANEXO", filtro.ANEXO),
                new Parameter("@ID_ANIO", filtro.ID_ANIO),
           };
            var result = await this.ExecuteReaderAsync<USP_SEL_PERIODOS_PROMOCIONALES_EBA_Result>("externo_mi_certificado.USP_SEL_PERIODOS_PROMOCIONALES_EBA", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }
    }
}
