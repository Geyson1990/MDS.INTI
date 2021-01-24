using System.Collections.Generic;
using System.Threading.Tasks;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Contract
{
    public interface IEstudianteService
    {
        Task<IEnumerable<EstudianteResponseDto>> Listar(EstudianteRequestDto filtro);
        Task<IEnumerable<EstudianteMatriculaPorCodigoResponseDto>> ListarInformacionEstudiante(EstudianteMatriculaPorCodigoRequestDto filtro);
        Task<IEnumerable<EstudianteAnioGradoSeccionResponseDto>> ListarEstudiantesPorAnioGradoSeccion(EstudianteAnioGradoSeccionRequestDto filtro);
        Task<IEnumerable<EstudianteAnioGradoSeccionResponseDto>> ListarEstudiantesPorAnioGradoSeccionEBA(EstudianteAnioGradoSeccionEBARequestDto filtro);
        Task<IEnumerable<EstudianteNotaPendienteResponseDto>> ListarNotasPendientesPorEstudiantes(EstudianteNotaPendienteRequestDto filtro);
        Task<IEnumerable<EstudianteInformacionResponseDto>> ListarInformacionEstudiante(EstudianteInformacionRequestDto filtro);
        Task<IEnumerable<EstudianteGradosResponseDto>> ListarGradosEstudiante(EstudianteGradosRequestDto filtro);
        Task<IEnumerable<EstudianteNotasResponseDto>> ListarNotasEstudiante(EstudianteNotasRequestDto filtro);
        Task<IEnumerable<EstudianteObservacionesResponseDto>> ListarObservacionesEstudiante(EstudianteObservacionesRequestDto filtro);
        Task<IEnumerable<EstudianteNotas2020ResponseDto>> ListarNotas2020Estudiante(EstudianteNotas2020RequestDto filtro);
        Task<IEnumerable<EstudianteMatriculaActualResponseDto>> ListarMatriculaActualEstudiante(EstudianteMatriculaActualRequestDto filtro);
        Task<IEnumerable<EstudianteMatriculaConcluidaResponseDto>> ListarMatriculaConcluidaEstudiante(EstudianteMatriculaConcluidaRequestDto filtro);
        Task<IEnumerable<EstudianteAnioEstudiosResponseDto>> ListarAnioEstudiosEstudiante(EstudianteAnioEstudiosRequestDto filtro);
        Task<IEnumerable<EstudianteInfoPorCodModularResponseDto>> ListarIePorCodigoModular(EstudianteInfoPorCodModularRequestDto filtro);
        Task<IEnumerable<EstudianteColegioNivelResponseDto>> ListarEstudianteDatosMatriculas(EstudianteModalidadNivelModularRequestDto filtro);
        Task<IEnumerable<EstudianteColegioNivelResponseDto>> ListarEstudianteDatosMatriculasPorNivel(EstudianteMatriculaNivelRequestDto filtro);
        Task<IEnumerable<EstudianteDatosPersonalesResponseDto>> ListarEstudianteDatosPersonales(EstudianteDatosPersonalesRequestDto filtro);
    }
}