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
    public class ProvinciaUoW : BaseUnitOfWork, IProvinciaQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public ProvinciaUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_SEL_PROVINCIA_Result>> Listar(USP_SEL_PROVINCIA_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_DEPARTAMENTO", filtro.COD_DEPARTAMENTO)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_PROVINCIA_Result>("externo_mi_certificado.USP_SEL_PROVINCIA", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_PROVINCIA_SIAGIE_Result>> ListarProvinciasSIAGIE(USP_SEL_PROVINCIA_SIAGIE_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_DEPARTAMENTO", filtro.COD_DEPARTAMENTO)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_PROVINCIA_SIAGIE_Result>("externo_mi_certificado.USP_SEL_PROVINCIA_SIAGIE", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }
    }
}
