using System.Collections.Generic;
using System.Threading.Tasks;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Contract
{
    public interface IDistritoService
    {
        Task<IEnumerable<DistritoResponseDto>> Listar(DistritoRequestDto filtro);
        Task<IEnumerable<DistritoResponseDto>> ListarDistritosSIAGIE(DistritoRequestDto filtro);
        
    }
}