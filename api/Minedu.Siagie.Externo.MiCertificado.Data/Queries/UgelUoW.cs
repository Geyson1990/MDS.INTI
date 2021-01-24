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
    public class UgelUoW : BaseUnitOfWork, IUgelQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public UgelUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_SEL_UGEL_Result>> Listar(USP_SEL_UGEL_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_DRE", filtro.COD_DRE)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_UGEL_Result>("externo_mi_certificado.USP_SEL_UGEL", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }
    }
}
