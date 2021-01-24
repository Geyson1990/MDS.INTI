using Minedu.Core.NetConnect.SQL.IData;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Queries
{
    public interface IEstudianteQuery: IBaseUnitOfWork
    {
        Task<IEnumerable<USP_SEL_ESTUDIANTES_CON_MATRICULA_Result>> Listar(USP_SEL_ESTUDIANTES_CON_MATRICULA_Request filtro);
        Task<IEnumerable<USP_SEL_DATOS_ALUMNO_X_CODIGO_ESTUDIANTE_Result>> ListarInformacionEstudiante(USP_SEL_DATOS_ALUMNO_X_CODIGO_ESTUDIANTE_Request filtro);
        Task<IEnumerable<USP_SEL_ESTUDIANTES_X_SECCION_Result>> ListarEstudiantesPorAnioGradoSeccion(USP_SEL_ESTUDIANTES_X_SECCION_Request filtro);
        Task<IEnumerable<USP_SEL_ESTUDIANTES_X_SECCION_Result>> ListarEstudiantesPorAnioGradoSeccionEBA(USP_SEL_ESTUDIANTES_X_SECCION_EBA_Request filtro);
        Task<IEnumerable<USP_SEL_NOTAS_FALTANTES_ESTUDIANTE_Result>> ListarNotasPendientesPorEstudiantes(USP_SEL_NOTAS_FALTANTES_ESTUDIANTE_Request filtro);
        Task<IEnumerable<USP_SEL_DATOS_ALUMNO_X_NIVEL_Result>> ListarInformacionEstudiante(USP_SEL_DATOS_ALUMNO_X_NIVEL_Request filtro);
        Task<IEnumerable<USP_SEL_GRADOS_ALUMNO_X_NIVEL_Result>> ListarGradosEstudiante(USP_SEL_GRADOS_ALUMNO_X_NIVEL_Request filtro);
        Task<IEnumerable<USP_SEL_NOTAS_ALUMNO_NIVEL_Result>> ListarNotasEstudiante(USP_SEL_NOTAS_ALUMNO_NIVEL_Request filtro);
        Task<IEnumerable<USP_SEL_OBS_ALUMNO_NIVEL_Result>> ListarObservacionesEstudiante(USP_SEL_OBS_ALUMNO_NIVEL_Request filtro);
        Task<IEnumerable<USP_SEL_NOTAS_ALUMNO_NIVEL_NORMA2020_Result>> ListarNotas2020Estudiante(USP_SEL_NOTAS_ALUMNO_NIVEL_NORMA2020_Request filtro);
        Task<IEnumerable<USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL_Result>> ListarMatriculaActualEstudiante(USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL_Request filtro);
        Task<IEnumerable<USP_SEL_ESTUDIANTES_CON_MATRICULA_CONCLUIDA_Result>> ListarMatriculaConcluidaEstudiante(USP_SEL_ESTUDIANTES_CON_MATRICULA_CONCLUIDA_Request filtro);
        Task<IEnumerable<USP_SEL_VALIDAR_ANIO_Result>> ListarAnioEstudiosEstudiante(USP_SEL_VALIDAR_ANIO_Request filtro);
        Task<IEnumerable<USP_SEL_DATOS_ALUMNO_X_COD_MODULAR_Result>> ListarIePorCodigoModular(USP_SEL_DATOS_ALUMNO_X_COD_MODULAR_Request filtro);
        Task<IEnumerable<USP_SEL_DATOS_IE_X_NIVEL_Result>> ListarEstudianteDatosMatriculas(USP_SEL_DATOS_IE_X_NIVEL_Request filtro);
        Task<IEnumerable<USP_SEL_DATOS_IE_X_NIVEL_Result>> ListarEstudianteDatosMatriculasPorNivel(USP_SEL_DATOS_IE_X_NIVEL_Request filtro);
        Task<IEnumerable<USP_SEL_DATOS_PERSONALES_ESTUDIANTE_Result>> ListarEstudianteDatosPersonales(USP_SEL_DATOS_PERSONALES_ESTUDIANTE_Request filtro);
    }
}
