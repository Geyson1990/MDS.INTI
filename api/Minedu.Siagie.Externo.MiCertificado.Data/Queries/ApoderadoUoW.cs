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
    public class ApoderadoUoW : BaseUnitOfWork, IApoderadoQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public ApoderadoUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_SEL_APODERADOS_X_DOCUMENTO_Result>> Listar(USP_SEL_APODERADOS_X_DOCUMENTO_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_TIPO_DOCUMENTO", filtro.ID_TIPO_DOCUMENTO),
                new Parameter("@NUMERO_DOCUMENTO", filtro.NUMERO_DOCUMENTO)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_APODERADOS_X_DOCUMENTO_Result>("externo_mi_certificado.USP_SEL_APODERADOS_X_DOCUMENTO", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
                                                                                                                     
        }

        public async Task<IEnumerable<USP_SEL_APODERADO_CON_ESTUDIANTE_Result>> ListarApoderadosEstudiantesConMatricula(USP_SEL_APODERADO_CON_ESTUDIANTE_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_PERSONA_APODERADO", filtro.ID_PERSONA_APODERADO),
                new Parameter("@ID_PERSONA_ESTUDIANTE", filtro.ID_PERSONA_ESTUDIANTE)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_APODERADO_CON_ESTUDIANTE_Result>("externo_mi_certificado.USP_SEL_APODERADO_CON_ESTUDIANTE", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }
    }
}
