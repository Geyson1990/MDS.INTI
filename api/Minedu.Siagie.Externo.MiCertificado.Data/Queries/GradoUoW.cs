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
    public class GradoUoW : BaseUnitOfWork, IGradoQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public GradoUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_SEL_GRADOS_X_NIVEL_Result>> ListarNivel(USP_SEL_GRADOS_X_NIVEL_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_MODALIDAD", filtro.ID_MODALIDAD),
                new Parameter("@ID_NIVEL", filtro.ID_NIVEL)

            };
            var result = await this.ExecuteReaderAsync<USP_SEL_GRADOS_X_NIVEL_Result>("externo_mi_certificado.USP_SEL_GRADOS_X_NIVEL", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_GRADO_SECCION_Result>> ListarSeccion(USP_SEL_GRADO_SECCION_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_MOD" , filtro.CODIGO_MODULAR),
                new Parameter("@ANEXO" , filtro.ANEXO),
                new Parameter("@ID_ANIO" , filtro.ID_ANIO),
                new Parameter("@ID_NIVEL" , filtro.ID_NIVEL),
                new Parameter("@ID_GRADO" , filtro.ID_GRADO),
                new Parameter("@ID_SECCION" , filtro.ID_SECCION),
                new Parameter("@ID_FASE" , filtro.ID_FASE)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_GRADO_SECCION_Result>("externo_mi_certificado.USP_SEL_GRADO_SECCION", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }

        public async Task<IEnumerable<USP_SEL_GRADO_SECCION_Result>> ListarSeccionEBA(USP_SEL_GRADO_SECCION_EBA_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_MOD" , filtro.COD_MOD),
                new Parameter("@ANEXO" , filtro.ANEXO),
                new Parameter("@ID_ANIO" , filtro.ID_ANIO),
                new Parameter("@ID_FASE_POR_PERIODO_PROM_IE" , filtro.ID_FASE_POR_PERIODO_PROM_IE),
                new Parameter("@ID_NIVEL" , filtro.ID_NIVEL),
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_GRADO_SECCION_Result>("externo_mi_certificado.USP_SEL_GRADO_SECCION_EBA", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }
    }
}
