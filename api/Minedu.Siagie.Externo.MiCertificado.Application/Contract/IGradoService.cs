using System.Collections.Generic;
using System.Threading.Tasks;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Contract
{
    public interface IGradoService
    {
        Task<IEnumerable<GradoNivelResponseDto>> ListarNivel(GradoNivelRequestDto filtro);
        Task<IEnumerable<GradoSeccionResponseDto>> ListarSeccion(GradoSeccionRequestDto filtro);
        Task<IEnumerable<GradoSeccionResponseDto>> ListarSeccionEBA(GradoSeccionEBARequestDto filtro);
    }
}