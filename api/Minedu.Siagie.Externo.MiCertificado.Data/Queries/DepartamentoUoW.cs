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
    public class DepartamentoUoW : BaseUnitOfWork, IDepartamentoQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public DepartamentoUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_SEL_DEPARTAMENTO_Result>> Listar()
        {        
            var parm = new Parameter[] { };
            var result = await this.ExecuteReaderAsync<USP_SEL_DEPARTAMENTO_Result>("externo_mi_certificado.USP_SEL_DEPARTAMENTO", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_DEPARTAMENTO_SIAGIE_Result>> ListarDepartamentosSIAGIE()
        {
            var parm = new Parameter[] { };
            var result = await this.ExecuteReaderAsync<USP_SEL_DEPARTAMENTO_SIAGIE_Result>("externo_mi_certificado.USP_SEL_DEPARTAMENTO_SIAGIE", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }


    }
}
