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
    public class EstudianteUoW : BaseUnitOfWork, IEstudianteQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public EstudianteUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<IEnumerable<USP_SEL_ESTUDIANTES_CON_MATRICULA_Result>> Listar(USP_SEL_ESTUDIANTES_CON_MATRICULA_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_TIPO_DOCUMENTO", filtro.ID_TIPO_DOCUMENTO),
                new Parameter("@NUMERO_DOCUMENTO", filtro.NUMERO_DOCUMENTO),
                new Parameter("@ID_SISTEMA", filtro.ID_SISTEMA)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_ESTUDIANTES_CON_MATRICULA_Result>("externo_mi_certificado.USP_SEL_ESTUDIANTES_CON_MATRICULA", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }
        public async Task<IEnumerable<USP_SEL_DATOS_ALUMNO_X_CODIGO_ESTUDIANTE_Result>> ListarInformacionEstudiante(USP_SEL_DATOS_ALUMNO_X_CODIGO_ESTUDIANTE_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@CODIGO_ESTUDIANTE" , filtro.CODIGO_ESTUDIANTE),
                new Parameter("@ID_NIVEL" , filtro.ID_NIVEL),
                new Parameter("@ID_TIPO_DOCUMENTO" , filtro.ID_TIPO_DOCUMENTO)

            };
            var result = await this.ExecuteReaderAsync<USP_SEL_DATOS_ALUMNO_X_CODIGO_ESTUDIANTE_Result>("externo_mi_certificado.USP_SEL_DATOS_ALUMNO_X_CODIGO_ESTUDIANTE", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_ESTUDIANTES_X_SECCION_Result>> ListarEstudiantesPorAnioGradoSeccion(USP_SEL_ESTUDIANTES_X_SECCION_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_MOD" , filtro.COD_MOD),
                new Parameter("@ANEXO" , filtro.ANEXO),
                new Parameter("@ID_ANIO" , filtro.ID_ANIO),
                new Parameter("@ID_NIVEL" , filtro.ID_NIVEL),
                new Parameter("@ID_GRADO" , filtro.ID_GRADO),
                new Parameter("@ID_SECCION" , filtro.ID_SECCION),
                new Parameter("@NRO_DOCUMENTO", filtro.NRO_DOCUMENTO),
                new Parameter("@NOMBRES_ESTUDIANTE",filtro.NOMBRES_ESTUDIANTE)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_ESTUDIANTES_X_SECCION_Result>("externo_mi_certificado.USP_SEL_ESTUDIANTES_X_SECCION", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }

        public async Task<IEnumerable<USP_SEL_ESTUDIANTES_X_SECCION_Result>> ListarEstudiantesPorAnioGradoSeccionEBA(USP_SEL_ESTUDIANTES_X_SECCION_EBA_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_MOD" , filtro.COD_MOD),
                new Parameter("@ANEXO" , filtro.ANEXO),
                new Parameter("@ID_NIVEL" , filtro.ID_NIVEL),
                new Parameter("@ID_ANIO" , filtro.ID_ANIO),
                new Parameter("@ID_SECC_FASE_PER_PROM" , filtro.ID_SECC_FASE_PER_PROM),
                new Parameter("@NRO_DOCUMENTO", filtro.NRO_DOCUMENTO),
                new Parameter("@NOMBRES_ESTUDIANTE",filtro.NOMBRES_ESTUDIANTE)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_ESTUDIANTES_X_SECCION_Result>("externo_mi_certificado.USP_SEL_ESTUDIANTES_X_SECCION_EBA", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }


        public async Task<IEnumerable<USP_SEL_NOTAS_FALTANTES_ESTUDIANTE_Result>> ListarNotasPendientesPorEstudiantes(USP_SEL_NOTAS_FALTANTES_ESTUDIANTE_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_PERSONA" , filtro.ID_PERSONA),
                new Parameter("@ID_NIVEL" ,  filtro.ID_NIVEL),
                new Parameter("@ID_SISTEMA", filtro.ID_SISTEMA)

            };
            var result = await this.ExecuteReaderAsync<USP_SEL_NOTAS_FALTANTES_ESTUDIANTE_Result>("externo_mi_certificado.USP_SEL_NOTAS_FALTANTES_ESTUDIANTE", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_DATOS_ALUMNO_X_NIVEL_Result>> ListarInformacionEstudiante(USP_SEL_DATOS_ALUMNO_X_NIVEL_Request filtro)
        {
            var parm = new Parameter[]
            {

                new Parameter("@ID_PERSONA", filtro.ID_PERSONA),
                new Parameter("@ID_MODALIDAD", filtro.ID_MODALIDAD),
                new Parameter("@ID_NIVEL", filtro.ID_NIVEL),
                new Parameter("@ID_SISTEMA", filtro.ID_SISTEMA)

            };
            var result = await this.ExecuteReaderAsync<USP_SEL_DATOS_ALUMNO_X_NIVEL_Result>("externo_mi_certificado.USP_SEL_DATOS_ALUMNO_X_NIVEL", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_GRADOS_ALUMNO_X_NIVEL_Result>> ListarGradosEstudiante(USP_SEL_GRADOS_ALUMNO_X_NIVEL_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_PERSONA", filtro.ID_PERSONA),
                new Parameter("@ID_MODALIDAD", filtro.ID_MODALIDAD),
                new Parameter("@ID_NIVEL", filtro.ID_NIVEL),
                new Parameter("@ID_SISTEMA", filtro.ID_SISTEMA)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_GRADOS_ALUMNO_X_NIVEL_Result>("externo_mi_certificado.USP_SEL_GRADOS_ALUMNO_X_NIVEL", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_NOTAS_ALUMNO_NIVEL_Result>> ListarNotasEstudiante(USP_SEL_NOTAS_ALUMNO_NIVEL_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_PERSONA", filtro.ID_PERSONA),
                new Parameter("@ID_NIVEL", filtro.ID_NIVEL),
                new Parameter("@ID_SISTEMA", filtro.ID_SISTEMA)
            };

            IEnumerable<USP_SEL_NOTAS_ALUMNO_NIVEL_Result> result = null;

            if (filtro.ID_NIVEL == "D1" || filtro.ID_NIVEL == "D2")
                result = await this.ExecuteReaderAsync<USP_SEL_NOTAS_ALUMNO_NIVEL_Result>("externo_mi_certificado.USP_SEL_NOTAS_ALUMNO_NIVEL_EBA", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            else
                result = await this.ExecuteReaderAsync<USP_SEL_NOTAS_ALUMNO_NIVEL_Result>("externo_mi_certificado.USP_SEL_NOTAS_ALUMNO_NIVEL", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }

        public async Task<IEnumerable<USP_SEL_OBS_ALUMNO_NIVEL_Result>> ListarObservacionesEstudiante(USP_SEL_OBS_ALUMNO_NIVEL_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_PERSONA", filtro.ID_PERSONA),
                new Parameter("@ID_NIVEL", filtro.ID_NIVEL),
                new Parameter("@ID_SISTEMA", filtro.ID_SISTEMA)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_OBS_ALUMNO_NIVEL_Result>("externo_mi_certificado.USP_SEL_OBS_ALUMNO_NIVEL", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }

        public async Task<IEnumerable<USP_SEL_NOTAS_ALUMNO_NIVEL_NORMA2020_Result>> ListarNotas2020Estudiante(USP_SEL_NOTAS_ALUMNO_NIVEL_NORMA2020_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_PERSONA", filtro.ID_PERSONA),
                new Parameter("@ID_NIVEL", filtro.ID_NIVEL)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_NOTAS_ALUMNO_NIVEL_NORMA2020_Result>("externo_mi_certificado.USP_SEL_NOTAS_ALUMNO_NIVEL_NORMA2020", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }

        public async Task<IEnumerable<USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL_Result>> ListarMatriculaActualEstudiante(USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_TIPO_DOCUMENTO", filtro.ID_TIPO_DOCUMENTO),
                new Parameter("@NUMERO_DOCUMENTO", filtro.NUMERO_DOCUMENTO),
                new Parameter("@COD_MOD", filtro.COD_MOD),
                new Parameter("@ANEXO", filtro.ANEXO),
                new Parameter("@ID_NIVEL", filtro.ID_NIVEL)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL_Result>("externo_mi_certificado.USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }

        public async Task<IEnumerable<USP_SEL_ESTUDIANTES_CON_MATRICULA_CONCLUIDA_Result>> ListarMatriculaConcluidaEstudiante(USP_SEL_ESTUDIANTES_CON_MATRICULA_CONCLUIDA_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_TIPO_DOCUMENTO", filtro.ID_TIPO_DOCUMENTO),
                new Parameter("@NUMERO_DOCUMENTO", filtro.NUMERO_DOCUMENTO),
                new Parameter("@COD_MOD", filtro.COD_MOD),
                new Parameter("@ANEXO",  filtro.ANEXO),
                new Parameter("@ID_NIVEL", filtro.ID_NIVEL)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_ESTUDIANTES_CON_MATRICULA_CONCLUIDA_Result>("externo_mi_certificado.USP_SEL_ESTUDIANTES_CON_MATRICULA_CONCLUIDA", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }

        public async Task<IEnumerable<USP_SEL_VALIDAR_ANIO_Result>> ListarAnioEstudiosEstudiante(USP_SEL_VALIDAR_ANIO_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_PERSONA" , filtro.ID_PERSONA),
                new Parameter("@ID_ANIO" , filtro.ID_ANIO),
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_VALIDAR_ANIO_Result>("externo_mi_certificado.USP_SEL_VALIDAR_ANIO", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }

        public async Task<IEnumerable<USP_SEL_DATOS_ALUMNO_X_COD_MODULAR_Result>> ListarIePorCodigoModular(USP_SEL_DATOS_ALUMNO_X_COD_MODULAR_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@COD_MOD", filtro.COD_MOD),
                new Parameter("@ANEXO", filtro.ANEXO)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_DATOS_ALUMNO_X_COD_MODULAR_Result>("externo_mi_certificado.USP_SEL_DATOS_ALUMNO_X_COD_MODULAR", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_DATOS_IE_X_NIVEL_Result>> ListarEstudianteDatosMatriculas(USP_SEL_DATOS_IE_X_NIVEL_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_PERSONA" , filtro.ID_PERSONA),
                new Parameter("@ID_MODALIDAD", filtro.ID_MODALIDAD),
                new Parameter("@ID_NIVEL",  filtro.ID_NIVEL),
                new Parameter("@COD_MOD",  filtro.COD_MOD),
                new Parameter("@ANEXO",  filtro.ANEXO),
                new Parameter("@ID_SISTEMA", "1")


            };
            var result = await this.ExecuteReaderAsync<USP_SEL_DATOS_IE_X_NIVEL_Result>("externo_mi_certificado.USP_SEL_DATOS_IE_X_NIVEL", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_DATOS_IE_X_NIVEL_Result>> ListarEstudianteDatosMatriculasPorNivel(USP_SEL_DATOS_IE_X_NIVEL_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@ID_PERSONA" , filtro.ID_PERSONA),
                new Parameter("@ID_MODALIDAD", filtro.ID_MODALIDAD),
                new Parameter("@ID_NIVEL",  filtro.ID_NIVEL),
                new Parameter("@COD_MOD",  ""),
                new Parameter("@ANEXO",  ""),
                new Parameter("@ID_SISTEMA", "")
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_DATOS_IE_X_NIVEL_Result>("externo_mi_certificado.USP_SEL_DATOS_IE_X_NIVEL", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;

        }

        public async Task<IEnumerable<USP_SEL_DATOS_PERSONALES_ESTUDIANTE_Result>> ListarEstudianteDatosPersonales(USP_SEL_DATOS_PERSONALES_ESTUDIANTE_Request filtro)
        {
            var parm = new Parameter[]
            {
                new Parameter("@CODIGO_ESTUDIANTE" , filtro.CODIGO_ESTUDIANTE)
            };
            var result = await this.ExecuteReaderAsync<USP_SEL_DATOS_PERSONALES_ESTUDIANTE_Result>("externo_mi_certificado.USP_SEL_DATOS_PERSONALES_ESTUDIANTE", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
            return result;
        }

    }
}
