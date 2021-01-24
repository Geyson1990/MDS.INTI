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
    public class NivelUoW : BaseUnitOfWork, INivelQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public NivelUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_EXTERNO_CERTIFICADO_SEL_NIVELES_ALUMNO_Result>> Listar(USP_EXTERNO_CERTIFICADO_SEL_NIVELES_ALUMNO_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_PERSONA", filtro.ID_PERSONA),
                new Parameter("@ID_MODALIDAD", filtro.ID_MODALIDAD) ,
                new Parameter("@ID_SISTEMA", filtro.ID_SISTEMA)
            };
            var result = await this.ExecuteReaderAsync<USP_EXTERNO_CERTIFICADO_SEL_NIVELES_ALUMNO_Result>("externo_mi_certificado.USP_SEL_NIVELES_ALUMNO", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }
    }
}
