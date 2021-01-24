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
    public class DistritoUoW : BaseUnitOfWork, IDistritoQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public DistritoUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_SEL_DISTRITO_Result>> Listar(USP_SEL_DISTRITO_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_PROVINCIA", filtro.COD_PROVINCIA)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_DISTRITO_Result>("externo_mi_certificado.USP_SEL_DISTRITO", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_DISTRITO_SIAGIE_Result>> ListarDistritosSIAGIE(USP_SEL_DISTRITO_SIAGIE_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_PROVINCIA", filtro.COD_PROVINCIA)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_DISTRITO_SIAGIE_Result>("externo_mi_certificado.USP_SEL_DISTRITO_SIAGIE", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }
    }
}
