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
    public class InstitucionEducativaUoW : BaseUnitOfWork, IInstitucionEducativaQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public InstitucionEducativaUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_SEL_IE_Result>> Listar(USP_SEL_IE_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_UGEL", filtro.COD_UGEL),
                new Parameter("@ID_NIVEL", filtro.ID_NIVEL) 
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_IE_Result>("externo_mi_certificado.USP_SEL_IE", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_IE_CODMOD_Result>> ListarDatosInstitucionesEducativas(USP_SEL_IE_CODMOD_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_MOD", filtro.COD_MOD),
                new Parameter("@ANEXO", filtro.ANEXO)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_IE_CODMOD_Result>("externo_mi_certificado.USP_SEL_IE_CODMOD", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<SEL_DATOS_INSTITUCION_EDUCATIVA_Result>> ListarInstitucionEducativaPorCodigoModular(SEL_DATOS_INSTITUCION_EDUCATIVA_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_MOD" , filtro.COD_MOD),
                new Parameter("@ANEXO" , filtro.ANEXO),
                new Parameter("@COD_DRE" , filtro.COD_DRE),
                new Parameter("@COD_UGEL" , filtro.COD_UGEL),
                new Parameter("@NOMBRE_IE" , filtro.NOMBRE_IE),
                new Parameter("@pageNumber" , filtro.pageNumber),
                new Parameter("@rowsPerPage" , filtro.rowsPerPage),
                new Parameter("@ID_NIVEL", filtro.ID_NIVEL)
            };
            var result = await this.ExecuteReaderAsync<SEL_DATOS_INSTITUCION_EDUCATIVA_Result>("externo_mi_certificado.USP_SEL_DATOS_INSTITUCION_EDUCATIVA", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_DATOS_IE_X_NIVEL_Result>> ListarInstitucionEducativaNiveles(USP_SEL_DATOS_IE_X_NIVEL_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_PERSONA", filtro.ID_PERSONA),
                new Parameter("@ID_MODALIDAD", filtro.ID_MODALIDAD),
                new Parameter("@ID_NIVEL", filtro.ID_NIVEL),
                new Parameter("@ID_SISTEMA", filtro.ID_SISTEMA)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_DATOS_IE_X_NIVEL_Result>("externo_mi_certificado.USP_SEL_DATOS_IE_X_NIVEL", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_IIEE_PADRON_Result>> ListarInstitucionEducativaPorPadrones(USP_SEL_IIEE_PADRON_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@DEPARTAMENTO" , filtro.DEPARTAMENTO),
                new Parameter("@PROVINCIA" , filtro.PROVINCIA),
                new Parameter("@UBIGEO" , filtro.UBIGEO),
                new Parameter("@CEN_EDU" , filtro.CEN_EDU),
                new Parameter("@COD_MOD" , filtro.COD_MOD),
                new Parameter("@ANEXO" , filtro.ANEXO),
                new Parameter("@COD_UGEL" , filtro.COD_UGEL),
                new Parameter("@ESTADO" , filtro.ESTADO),
                new Parameter("@PAGE_SIZE" , filtro.PAGE_SIZE),
                new Parameter("@PAGE" , filtro.PAGE)

            };
            var result = await this.ExecuteReaderAsync<USP_SEL_IIEE_PADRON_Result>("externo_mi_certificado.USP_SEL_IIEE_PADRON", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        
    }
}
