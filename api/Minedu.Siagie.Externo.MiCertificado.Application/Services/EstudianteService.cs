using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Domain.Queries;
using Minedu.Siagie.Externo.MiCertificado.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteQuery _uow;
        private readonly IMapper _iMapper;

        public EstudianteService(IMapper iMapper, IEstudianteQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<EstudianteResponseDto>> Listar(EstudianteRequestDto filtro)
        {
            filtro.IdSistema = filtro.IdSistema == "2" ? "" : filtro.IdSistema;
            var item = await _uow.Listar(_iMapper.Map<EstudianteRequestDto, USP_SEL_ESTUDIANTES_CON_MATRICULA_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_ESTUDIANTES_CON_MATRICULA_Result>,IEnumerable<EstudianteResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteMatriculaPorCodigoResponseDto>> ListarInformacionEstudiante(EstudianteMatriculaPorCodigoRequestDto filtro)
        {
            var item = await _uow.ListarInformacionEstudiante(_iMapper.Map<EstudianteMatriculaPorCodigoRequestDto, USP_SEL_DATOS_ALUMNO_X_CODIGO_ESTUDIANTE_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_DATOS_ALUMNO_X_CODIGO_ESTUDIANTE_Result>, IEnumerable<EstudianteMatriculaPorCodigoResponseDto>>(item);
        }
        
        public async Task<IEnumerable<EstudianteAnioGradoSeccionResponseDto>> ListarEstudiantesPorAnioGradoSeccion(EstudianteAnioGradoSeccionRequestDto filtro)
        {
            var item = await _uow.ListarEstudiantesPorAnioGradoSeccion(_iMapper.Map<EstudianteAnioGradoSeccionRequestDto, USP_SEL_ESTUDIANTES_X_SECCION_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_ESTUDIANTES_X_SECCION_Result>, IEnumerable<EstudianteAnioGradoSeccionResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteAnioGradoSeccionResponseDto>> ListarEstudiantesPorAnioGradoSeccionEBA(EstudianteAnioGradoSeccionEBARequestDto filtro)
        {
            var item = await _uow.ListarEstudiantesPorAnioGradoSeccionEBA(_iMapper.Map<EstudianteAnioGradoSeccionEBARequestDto, USP_SEL_ESTUDIANTES_X_SECCION_EBA_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_ESTUDIANTES_X_SECCION_Result>, IEnumerable<EstudianteAnioGradoSeccionResponseDto>>(item);
        }


        public async Task<IEnumerable<EstudianteNotaPendienteResponseDto>> ListarNotasPendientesPorEstudiantes(EstudianteNotaPendienteRequestDto filtro)
        {
            filtro.IdSistema = filtro.IdSistema == "2" ? "" : filtro.IdSistema;
            var item = await _uow.ListarNotasPendientesPorEstudiantes(_iMapper.Map<EstudianteNotaPendienteRequestDto, USP_SEL_NOTAS_FALTANTES_ESTUDIANTE_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_NOTAS_FALTANTES_ESTUDIANTE_Result>, IEnumerable<EstudianteNotaPendienteResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteInformacionResponseDto>> ListarInformacionEstudiante(EstudianteInformacionRequestDto filtro)
        {
            filtro.IdSistema = filtro.IdSistema == "2" ? "" : filtro.IdSistema;
            var item = await _uow.ListarInformacionEstudiante(_iMapper.Map<EstudianteInformacionRequestDto, USP_SEL_DATOS_ALUMNO_X_NIVEL_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_DATOS_ALUMNO_X_NIVEL_Result>, IEnumerable<EstudianteInformacionResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteGradosResponseDto>> ListarGradosEstudiante(EstudianteGradosRequestDto filtro)
        {
            filtro.IdSistema = filtro.IdSistema == "2" ? "" : filtro.IdSistema;
            var item = await _uow.ListarGradosEstudiante(_iMapper.Map<EstudianteGradosRequestDto, USP_SEL_GRADOS_ALUMNO_X_NIVEL_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_GRADOS_ALUMNO_X_NIVEL_Result>, IEnumerable<EstudianteGradosResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteNotasResponseDto>> ListarNotasEstudiante(EstudianteNotasRequestDto filtro)
        {
            filtro.IdSistema = filtro.IdSistema == "2" ? "" : filtro.IdSistema;
            var item = await _uow.ListarNotasEstudiante(_iMapper.Map<EstudianteNotasRequestDto, USP_SEL_NOTAS_ALUMNO_NIVEL_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_NOTAS_ALUMNO_NIVEL_Result>, IEnumerable<EstudianteNotasResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteObservacionesResponseDto>> ListarObservacionesEstudiante(EstudianteObservacionesRequestDto filtro)
        {
            filtro.IdSistema = filtro.IdSistema == "2" ? "" : filtro.IdSistema;
            var item = await _uow.ListarObservacionesEstudiante(_iMapper.Map<EstudianteObservacionesRequestDto, USP_SEL_OBS_ALUMNO_NIVEL_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_OBS_ALUMNO_NIVEL_Result>, IEnumerable<EstudianteObservacionesResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteNotas2020ResponseDto>> ListarNotas2020Estudiante(EstudianteNotas2020RequestDto filtro)
        {
            var item = await _uow.ListarNotas2020Estudiante(_iMapper.Map<EstudianteNotas2020RequestDto, USP_SEL_NOTAS_ALUMNO_NIVEL_NORMA2020_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_NOTAS_ALUMNO_NIVEL_NORMA2020_Result>, IEnumerable<EstudianteNotas2020ResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteMatriculaActualResponseDto>> ListarMatriculaActualEstudiante(EstudianteMatriculaActualRequestDto filtro)
        {
            var item = await _uow.ListarMatriculaActualEstudiante(_iMapper.Map<EstudianteMatriculaActualRequestDto, USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL_Result>, IEnumerable<EstudianteMatriculaActualResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteMatriculaConcluidaResponseDto>> ListarMatriculaConcluidaEstudiante(EstudianteMatriculaConcluidaRequestDto filtro)
        {
            var item = await _uow.ListarMatriculaConcluidaEstudiante(_iMapper.Map<EstudianteMatriculaConcluidaRequestDto, USP_SEL_ESTUDIANTES_CON_MATRICULA_CONCLUIDA_Request>(filtro));       
            return _iMapper.Map<IEnumerable<USP_SEL_ESTUDIANTES_CON_MATRICULA_CONCLUIDA_Result>, IEnumerable<EstudianteMatriculaConcluidaResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteAnioEstudiosResponseDto>> ListarAnioEstudiosEstudiante(EstudianteAnioEstudiosRequestDto filtro)
        {
            var item = await _uow.ListarAnioEstudiosEstudiante(_iMapper.Map<EstudianteAnioEstudiosRequestDto, USP_SEL_VALIDAR_ANIO_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_VALIDAR_ANIO_Result>, IEnumerable<EstudianteAnioEstudiosResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteInfoPorCodModularResponseDto>> ListarIePorCodigoModular(EstudianteInfoPorCodModularRequestDto filtro)
        {
            var item = await _uow.ListarIePorCodigoModular(_iMapper.Map<EstudianteInfoPorCodModularRequestDto, USP_SEL_DATOS_ALUMNO_X_COD_MODULAR_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_DATOS_ALUMNO_X_COD_MODULAR_Result>, IEnumerable<EstudianteInfoPorCodModularResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteColegioNivelResponseDto>> ListarEstudianteDatosMatriculas(EstudianteModalidadNivelModularRequestDto filtro)
        {
            var item = await _uow.ListarEstudianteDatosMatriculas(_iMapper.Map<EstudianteModalidadNivelModularRequestDto, USP_SEL_DATOS_IE_X_NIVEL_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_DATOS_IE_X_NIVEL_Result>, IEnumerable<EstudianteColegioNivelResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteColegioNivelResponseDto>> ListarEstudianteDatosMatriculasPorNivel(EstudianteMatriculaNivelRequestDto filtro)
        {
            var item = await _uow.ListarEstudianteDatosMatriculasPorNivel(_iMapper.Map<EstudianteMatriculaNivelRequestDto, USP_SEL_DATOS_IE_X_NIVEL_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_DATOS_IE_X_NIVEL_Result>, IEnumerable<EstudianteColegioNivelResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteDatosPersonalesResponseDto>> ListarEstudianteDatosPersonales(EstudianteDatosPersonalesRequestDto filtro)
        {
            var item = await _uow.ListarEstudianteDatosPersonales(_iMapper.Map<EstudianteDatosPersonalesRequestDto, USP_SEL_DATOS_PERSONALES_ESTUDIANTE_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_DATOS_PERSONALES_ESTUDIANTE_Result>, IEnumerable<EstudianteDatosPersonalesResponseDto>>(item);
        }
    }
}
