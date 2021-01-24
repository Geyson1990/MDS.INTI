using Minedu.Core.NetConnect.SQL.Data;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Minedu.Siagie.Externo.MiCertificado.Data.Queries
{
    public class AnioInstitucionUoW : BaseUnitOfWork, IAnioInstitucionQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public AnioInstitucionUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_SEL_ANIOS_X_IE_Result>> Listar(USP_SEL_ANIOS_X_IE_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_MOD", filtro.COD_MOD),
                new Parameter("@ANEXO", filtro.ANEXO)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_ANIOS_X_IE_Result>("externo_mi_certificado.USP_SEL_ANIOS_X_IE", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }
    }
}
