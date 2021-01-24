using System.Collections.Generic;
using System.Threading.Tasks;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Contract
{
    public interface IApoderadoService
    {
        Task<IEnumerable<ApoderadoResponseDto>> Listar(ApoderadoRequestDto filtro);
        Task<IEnumerable<ApoderadoEstudianteResponseDto>> ListarApoderadosEstudiantesConMatricula(ApoderadoEstudianteRequestDto filtro);
    }
}