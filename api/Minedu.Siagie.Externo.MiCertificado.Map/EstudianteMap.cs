using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class EstudianteMap : Profile
    {
        public EstudianteMap()
        {
            CreateMap<EstudianteRequestDto, USP_SEL_ESTUDIANTES_CON_MATRICULA_Request>()
                .ForMember(des => des.ID_TIPO_DOCUMENTO, opt => opt.MapFrom(src => src.TipoDocumento))
                .ForMember(des => des.NUMERO_DOCUMENTO, opt => opt.MapFrom(src => src.NumeroDocumento))
                .ForMember(des => des.ID_SISTEMA, opt => opt.MapFrom(src => src.IdSistema));

            CreateMap<USP_SEL_ESTUDIANTES_CON_MATRICULA_Result, EstudianteResponseDto>()
                .ForMember(des => des.IdPersonaEstudiante, opt => opt.MapFrom(src => src.ID_PERSONA_ESTUDIANTE))
                .ForMember(des => des.IdMatricula, opt => opt.MapFrom(src => src.ID_MATRICULA))
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.DscNivel, opt => opt.MapFrom(src => src.DSC_NIVEL));


            CreateMap<EstudianteMatriculaPorCodigoRequestDto, USP_SEL_DATOS_ALUMNO_X_CODIGO_ESTUDIANTE_Request>()
                .ForMember(des => des.CODIGO_ESTUDIANTE, opt => opt.MapFrom(src => src.NumeroDocumento))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                .ForMember(des => des.ID_TIPO_DOCUMENTO, opt => opt.MapFrom(src => src.TipoDocumento));

            CreateMap<USP_SEL_DATOS_ALUMNO_X_CODIGO_ESTUDIANTE_Result, EstudianteMatriculaPorCodigoResponseDto>()
                .ForMember(des => des.IdPersona, opt => opt.MapFrom(src => src.ID_PERSONA))
                .ForMember(des => des.CodigoEstudiante, opt => opt.MapFrom(src => src.CODIGO_ESTUDIANTE))
                .ForMember(des => des.IdTipoDocumento, opt => opt.MapFrom(src => src.ID_TIPO_DOCUMENTO))
                .ForMember(des => des.NumeroDocumento, opt => opt.MapFrom(src => src.NUMERO_DOCUMENTO))
                .ForMember(des => des.ApellidoPaterno, opt => opt.MapFrom(src => src.APELLIDO_PATERNO))
                .ForMember(des => des.ApellidoMaterno, opt => opt.MapFrom(src => src.APELLIDO_MATERNO))
                .ForMember(des => des.Nombres, opt => opt.MapFrom(src => src.NOMBRES))
                .ForMember(des => des.FechaNacimiento, opt => opt.MapFrom(src => src.FECHA_NACIMIENTO))
                .ForMember(des => des.UbigeoDomicilio, opt => opt.MapFrom(src => src.UBIGEO_NACIMIENTO_INEI))
                .ForMember(des => des.UltimoAnio, opt => opt.MapFrom(src => src.ID_ANIO))
                .ForMember(des => des.IdModalidad, opt => opt.MapFrom(src => src.ID_MODALIDAD))
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                .ForMember(des => des.DscGrado, opt => opt.MapFrom(src => src.DSC_GRADO));
            //.ForMember(des => des.Estado, opt => opt.MapFrom(src => src.ESTADO));


            CreateMap<EstudianteAnioGradoSeccionRequestDto, USP_SEL_ESTUDIANTES_X_SECCION_Request>()
                .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo))
                .ForMember(des => des.ID_ANIO, opt => opt.MapFrom(src => src.IdAnio))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                .ForMember(des => des.ID_GRADO, opt => opt.MapFrom(src => src.IdGrado))
                .ForMember(des => des.ID_SECCION, opt => opt.MapFrom(src => src.IdSeccion))
                .ForMember(des => des.NRO_DOCUMENTO, opt => opt.MapFrom(src => src.NumeroDocumento))
                .ForMember(des => des.NOMBRES_ESTUDIANTE, opt => opt.MapFrom(src => src.NombresEstudiante));

            CreateMap<USP_SEL_ESTUDIANTES_X_SECCION_Result, EstudianteAnioGradoSeccionResponseDto>()
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.IdAnio, opt => opt.MapFrom(src => src.ID_ANIO))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                .ForMember(des => des.IdSeccion, opt => opt.MapFrom(src => src.ID_SECCION))
                .ForMember(des => des.IdPersona, opt => opt.MapFrom(src => src.ID_PERSONA))
                //.ForMember(des => des.TrasladoExterno, opt => opt.MapFrom(src => src.TRASLADO_EXTERNO))
                .ForMember(des => des.ApellidoPaterno, opt => opt.MapFrom(src => src.APELLIDO_PATERNO))
                .ForMember(des => des.ApellidoMaterno, opt => opt.MapFrom(src => src.APELLIDO_MATERNO))
                .ForMember(des => des.Nombres, opt => opt.MapFrom(src => src.NOMBRES))
                .ForMember(des => des.NombreCompleto, opt => opt.MapFrom(src => src.NOMBRE_COMPLETO))
                //.ForMember(des => des.FechaNacimiento, opt => opt.MapFrom(src => src.FECHA_NACIMIENTO))
                .ForMember(des => des.CodigoEstudiante, opt => opt.MapFrom(src => src.CODIGO_ESTUDIANTE))
                //.ForMember(des => des.IdMatricula, opt => opt.MapFrom(src => src.ID_MATRICULA))
                //.ForMember(des => des.EstadoMatricula, opt => opt.MapFrom(src => src.ESTADO_MATRICULA))
                //.ForMember(des => des.FechaMatricula, opt => opt.MapFrom(src => src.FECHA_MATRICULA))
                .ForMember(des => des.NumeroDocumento, opt => opt.MapFrom(src => src.DNI))
                .ForMember(des => des.ValidadoReniec, opt => opt.MapFrom(src => src.VALIDADO_RENIEC))
                //.ForMember(des => des.IdModulo, opt => opt.MapFrom(src => src.ID_MODULO))
                //.ForMember(des => des.DescNivel, opt => opt.MapFrom(src => src.DSC_NIVEL))
                .ForMember(des => des.IdModalidad, opt => opt.MapFrom(src => src.ID_MODALIDAD));
            //.ForMember(des => des.DescModalidad, opt => opt.MapFrom(src => src.DSC_MODALIDAD))
            //.ForMember(des => des.AbreModalidad, opt => opt.MapFrom(src => src.ABR_MODALIDAD))
            //.ForMember(des => des.DescGrado, opt => opt.MapFrom(src => src.DSC_GRADO));


            CreateMap<EstudianteNotaPendienteRequestDto, USP_SEL_NOTAS_FALTANTES_ESTUDIANTE_Request>()
                .ForMember(des => des.ID_PERSONA, opt => opt.MapFrom(src => src.IdPersona))
                .ForMember(des => des.ID_SISTEMA, opt => opt.MapFrom(src => src.IdSistema))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel));

            CreateMap<USP_SEL_NOTAS_FALTANTES_ESTUDIANTE_Result, EstudianteNotaPendienteResponseDto>()
                .ForMember(des => des.IdAnio, opt => opt.MapFrom(src => src.ID_ANIO))
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.IdPersona, opt => opt.MapFrom(src => src.ID_PERSONA))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.DscNivel, opt => opt.MapFrom(src => src.DSC_NIVEL))
                .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                .ForMember(des => des.DscGrado, opt => opt.MapFrom(src => src.DSC_GRADO))
                .ForMember(des => des.IdTipoArea, opt => opt.MapFrom(src => src.ID_TIPO_AREA))
                .ForMember(des => des.DscTipoArea, opt => opt.MapFrom(src => src.DSC_TIPO_AREA))
                .ForMember(des => des.EsConducta, opt => opt.MapFrom(src => src.ESCONDUCTA))
                .ForMember(des => des.NotaFinalArea, opt => opt.MapFrom(src => src.NOTA_FINAL_AREA))
                .ForMember(des => des.IdArea, opt => opt.MapFrom(src => src.ID_AREA))
                .ForMember(des => des.DscArea, opt => opt.MapFrom(src => src.DSC_AREA))
                .ForMember(des => des.RegistroNota, opt => opt.MapFrom(src => src.REGISTRO_NOTA));


            CreateMap<EstudianteInformacionRequestDto, USP_SEL_DATOS_ALUMNO_X_NIVEL_Request>()
                .ForMember(des => des.ID_PERSONA, opt => opt.MapFrom(src => src.IdPersona))
                .ForMember(des => des.ID_MODALIDAD, opt => opt.MapFrom(src => src.IdModalidad))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                .ForMember(des => des.ID_SISTEMA, opt => opt.MapFrom(src => src.IdSistema));

            CreateMap<USP_SEL_DATOS_ALUMNO_X_NIVEL_Result, EstudianteInformacionResponseDto>()
                .ForMember(des => des.IdPersona, opt => opt.MapFrom(src => src.ID_PERSONA))
                .ForMember(des => des.IdTipoDocumento, opt => opt.MapFrom(src => src.ID_TIPO_DOCUMENTO))
                .ForMember(des => des.NumeroDocumento, opt => opt.MapFrom(src => src.NUMERO_DOCUMENTO))
                .ForMember(des => des.ApellidoPaterno, opt => opt.MapFrom(src => src.APELLIDO_PATERNO))
                .ForMember(des => des.ApellidoMaterno, opt => opt.MapFrom(src => src.APELLIDO_MATERNO))
                .ForMember(des => des.Nombres, opt => opt.MapFrom(src => src.NOMBRES))
                .ForMember(des => des.IdMatricula, opt => opt.MapFrom(src => src.ID_MATRICULA))
                .ForMember(des => des.IdAnio, opt => opt.MapFrom(src => src.ID_ANIO))
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.CenEdu, opt => opt.MapFrom(src => src.CEN_EDU))
                .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                .ForMember(des => des.DscGrado, opt => opt.MapFrom(src => src.DSC_GRADO))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.DscNivel, opt => opt.MapFrom(src => src.DSC_NIVEL))
                .ForMember(des => des.IdModalidad, opt => opt.MapFrom(src => src.ID_MODALIDAD))
                .ForMember(des => des.AbrModalidad, opt => opt.MapFrom(src => src.ABR_MODALIDAD))
                .ForMember(des => des.DscModalidad, opt => opt.MapFrom(src => src.DSC_MODALIDAD))
                .ForMember(des => des.Director, opt => opt.MapFrom(src => src.DIRECTOR));


            CreateMap<EstudianteGradosRequestDto, USP_SEL_GRADOS_ALUMNO_X_NIVEL_Request>()
                .ForMember(des => des.ID_MODALIDAD, opt => opt.MapFrom(src => src.IdModalidad))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                .ForMember(des => des.ID_PERSONA, opt => opt.MapFrom(src => src.IdPersona))
                .ForMember(des => des.ID_SISTEMA, opt => opt.MapFrom(src => src.IdSistema));

            CreateMap<USP_SEL_GRADOS_ALUMNO_X_NIVEL_Result, EstudianteGradosResponseDto>()
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.CorrEstadistica, opt => opt.MapFrom(src => src.CORR_ESTADISTICA))
                .ForMember(des => des.DscGrado, opt => opt.MapFrom(src => src.DSC_GRADO))
                .ForMember(des => des.DscNivel, opt => opt.MapFrom(src => src.DSC_NIVEL))
                .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.ESTADO))
                .ForMember(des => des.IdAnio, opt => opt.MapFrom(src => src.ID_ANIO))
                .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.EdadMinima, opt => opt.MapFrom(src => src.EDAD_MINIMA))
                .ForMember(des => des.EdadMaxima, opt => opt.MapFrom(src => src.EDAD_MAXIMA))
                .ForMember(des => des.SituacionFinal, opt => opt.MapFrom(src => src.SITUACION_FINAL));


            CreateMap<EstudianteNotasRequestDto, USP_SEL_NOTAS_ALUMNO_NIVEL_Request>()
                //.ForMember(des => des.ID_MODALIDAD, opt => opt.MapFrom(src => src.IdModalidad))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                .ForMember(des => des.ID_PERSONA, opt => opt.MapFrom(src => src.IdPersona))
                .ForMember(des => des.ID_SISTEMA, opt => opt.MapFrom(src => src.IdSistema));

            CreateMap<USP_SEL_NOTAS_ALUMNO_NIVEL_Result, EstudianteNotasResponseDto>()
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.DscArea, opt => opt.MapFrom(src => src.DSC_AREA))
                .ForMember(des => des.DscGrado, opt => opt.MapFrom(src => src.DSC_GRADO))
                .ForMember(des => des.DscNivel, opt => opt.MapFrom(src => src.DSC_NIVEL))
                .ForMember(des => des.DscTipoArea, opt => opt.MapFrom(src => src.DSC_TIPO_AREA))
                .ForMember(des => des.EsConducta, opt => opt.MapFrom(src => src.ESCONDUCTA))
                .ForMember(des => des.IdAnio, opt => opt.MapFrom(src => src.ID_ANIO))
                .ForMember(des => des.IdArea, opt => opt.MapFrom(src => src.ID_AREA))
                .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.IdTipoArea, opt => opt.MapFrom(src => src.ID_TIPO_AREA))
                .ForMember(des => des.NotaFinalArea, opt => opt.MapFrom(src => src.NOTA_FINAL_AREA));


            CreateMap<EstudianteObservacionesRequestDto, USP_SEL_OBS_ALUMNO_NIVEL_Request>()
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                .ForMember(des => des.ID_PERSONA, opt => opt.MapFrom(src => src.IdPersona))
                .ForMember(des => des.ID_SISTEMA, opt => opt.MapFrom(src => src.IdSistema));

            CreateMap<USP_SEL_OBS_ALUMNO_NIVEL_Result, EstudianteObservacionesResponseDto>()
                .ForMember(des => des.Dsc, opt => opt.MapFrom(src => src.DSC))
                .ForMember(des => des.IdAnio, opt => opt.MapFrom(src => src.ID_ANIO))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.IdTipo, opt => opt.MapFrom(src => src.ID_TIPO))
                .ForMember(des => des.Motivo, opt => opt.MapFrom(src => src.MOTIVO))
                .ForMember(des => des.Resolucion, opt => opt.MapFrom(src => src.RESOLUCION))
                .ForMember(des => des.TipoObs, opt => opt.MapFrom(src => src.TIPO_OBS))
                .ForMember(des => des.TipoSolicitud, opt => opt.MapFrom(src => src.TIPO_SOLICITUD));


            CreateMap<EstudianteNotas2020RequestDto, USP_SEL_NOTAS_ALUMNO_NIVEL_NORMA2020_Request>()
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                .ForMember(des => des.ID_PERSONA, opt => opt.MapFrom(src => src.IdPersona));

            CreateMap<USP_SEL_NOTAS_ALUMNO_NIVEL_NORMA2020_Result, EstudianteNotas2020ResponseDto>()
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.DscArea, opt => opt.MapFrom(src => src.DSC_AREA))
                .ForMember(des => des.DscAsignatura, opt => opt.MapFrom(src => src.DSC_ASIGNATURA))
                .ForMember(des => des.DscGrado, opt => opt.MapFrom(src => src.DSC_GRADO))
                .ForMember(des => des.DscNivel, opt => opt.MapFrom(src => src.DSC_NIVEL))
                .ForMember(des => des.DscTipoArea, opt => opt.MapFrom(src => src.DSC_TIPO_AREA))
                .ForMember(des => des.Esconduta, opt => opt.MapFrom(src => src.ESCONDUCTA))
                .ForMember(des => des.IdAnio, opt => opt.MapFrom(src => src.ID_ANIO))
                .ForMember(des => des.IdArea, opt => opt.MapFrom(src => src.ID_AREA))
                .ForMember(des => des.IdAsignatura, opt => opt.MapFrom(src => src.ID_ASIGNATURA))
                .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.IdTipoArea, opt => opt.MapFrom(src => src.ID_TIPO_AREA))
                .ForMember(des => des.NotaFinalArea, opt => opt.MapFrom(src => src.NOTA_FINAL_AREA));


            CreateMap<EstudianteMatriculaActualRequestDto, USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL_Request>()
                .ForMember(des => des.ID_TIPO_DOCUMENTO, opt => opt.MapFrom(src => src.TipoDocumento))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo))
                .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))
                .ForMember(des => des.NUMERO_DOCUMENTO, opt => opt.MapFrom(src => src.NumeroDocumento));

            CreateMap<USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL_Result, EstudianteMatriculaActualResponseDto>()
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.ESTADO))
                .ForMember(des => des.IdAnio, opt => opt.MapFrom(src => src.ID_ANIO))
                .ForMember(des => des.IdPersona, opt => opt.MapFrom(src => src.ID_PERSONA))
                .ForMember(des => des.validadoReniec, opt => opt.MapFrom(src => src.VALIDADO_RENIEC))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL));


            CreateMap<EstudianteMatriculaConcluidaRequestDto, USP_SEL_ESTUDIANTES_CON_MATRICULA_CONCLUIDA_Request>()
                .ForMember(des => des.ID_TIPO_DOCUMENTO, opt => opt.MapFrom(src => src.TipoDocumento))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo))
                .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))
                .ForMember(des => des.NUMERO_DOCUMENTO, opt => opt.MapFrom(src => src.NumeroDocumento));
                //.ForMember(des => des.ID_MODALIDAD, opt => opt.MapFrom(src => src.IdModalidad));

            CreateMap<USP_SEL_ESTUDIANTES_CON_MATRICULA_CONCLUIDA_Result, EstudianteMatriculaConcluidaResponseDto>()
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.ApellidoMaterno, opt => opt.MapFrom(src => src.APELLIDO_MATERNO))
                .ForMember(des => des.ApellidoPaterno, opt => opt.MapFrom(src => src.APELLIDO_PATERNO))
                .ForMember(des => des.Nombres, opt => opt.MapFrom(src => src.NOMBRES))
                .ForMember(des => des.CodigoEstudiante, opt => opt.MapFrom(src => src.CODIGO_ESTUDIANTE))
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.DscGrado, opt => opt.MapFrom(src => src.DSC_GRADO))
                .ForMember(des => des.FechaNacimiento, opt => opt.MapFrom(src => src.FECHA_NACIMIENTO))
                .ForMember(des => des.IdAnio, opt => opt.MapFrom(src => src.ID_ANIO))
                .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                .ForMember(des => des.IdMatricula, opt => opt.MapFrom(src => src.ID_MATRICULA))
                .ForMember(des => des.IdModalidad, opt => opt.MapFrom(src => src.ID_MODALIDAD))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                //.ForMember(des => des.IdPersona, opt => opt.MapFrom(src => src.ID_PERSONA))
                .ForMember(des => des.IdPersonaEstudiante, opt => opt.MapFrom(src => src.ID_PERSONA_ESTUDIANTE))
                .ForMember(des => des.IdTipoDocumento, opt => opt.MapFrom(src => src.ID_TIPO_DOCUMENTO))
                .ForMember(des => des.NumeroDocumento, opt => opt.MapFrom(src => src.NUMERO_DOCUMENTO))
                .ForMember(des => des.Ubigeo, opt => opt.MapFrom(src => src.UBIGEO_NACIMIENTO_INEI))
                .ForMember(des => des.ConVida, opt => opt.MapFrom(src => src.CON_VIDA));

            CreateMap<EstudianteAnioEstudiosRequestDto, USP_SEL_VALIDAR_ANIO_Request>()
                .ForMember(des => des.ID_ANIO, opt => opt.MapFrom(src => src.IdAnio))
                .ForMember(des => des.ID_PERSONA, opt => opt.MapFrom(src => src.IdPersona));

            CreateMap<USP_SEL_VALIDAR_ANIO_Result, EstudianteAnioEstudiosResponseDto>()
                .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.ESTADO));

            //Datos Personales
            CreateMap<EstudianteDatosPersonalesRequestDto, USP_SEL_DATOS_PERSONALES_ESTUDIANTE_Request>()
                .ForMember(des => des.CODIGO_ESTUDIANTE, opt => opt.MapFrom(src => src.CodigoEstudiante));

            CreateMap<USP_SEL_DATOS_PERSONALES_ESTUDIANTE_Result, EstudianteDatosPersonalesResponseDto>()
                .ForMember(des => des.ApellidoPaterno, opt => opt.MapFrom(src => src.APELLIDO_PATERNO))
                .ForMember(des => des.ApellidoMaterno, opt => opt.MapFrom(src => src.APELLIDO_MATERNO))
                .ForMember(des => des.Nombres, opt => opt.MapFrom(src => src.NOMBRES))
                .ForMember(des => des.IdPersona, opt => opt.MapFrom(src => src.ID_PERSONA))
                .ForMember(des => des.FechaNacimiento, opt => opt.MapFrom(src => src.FECHA_NACIMIENTO))
                .ForMember(des => des.CodigoEstudiante, opt => opt.MapFrom(src => src.CODIGO_ESTUDIANTE));


            CreateMap<EstudianteAnioGradoSeccionEBARequestDto, USP_SEL_ESTUDIANTES_X_SECCION_EBA_Request>()
                .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                .ForMember(des => des.ID_ANIO, opt => opt.MapFrom(src => src.IdAnio))
                .ForMember(des => des.ID_SECC_FASE_PER_PROM, opt => opt.MapFrom(src => src.IdSecFasePerProm))
                .ForMember(des => des.NRO_DOCUMENTO, opt => opt.MapFrom(src => src.NumeroDocumento))
                .ForMember(des => des.NOMBRES_ESTUDIANTE, opt => opt.MapFrom(src => src.NombresEstudiante))
                ;

        }
    }
}
