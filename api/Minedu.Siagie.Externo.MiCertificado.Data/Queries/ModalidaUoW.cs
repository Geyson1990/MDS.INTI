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
    public class ModalidadUoW : BaseUnitOfWork, IModalidadQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public ModalidadUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_SEL_MODALIDADES_IIEE_Result>> Listar()
        {        
            var parm = new Parameter[] { };
            var result = await this.ExecuteReaderAsync<USP_SEL_MODALIDADES_IIEE_Result>("externo_mi_certificado.USP_SEL_MODALIDADES_IIEE", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }
    }
}
