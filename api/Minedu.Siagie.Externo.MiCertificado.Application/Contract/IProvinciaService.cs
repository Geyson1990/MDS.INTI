using System.Collections.Generic;
using System.Threading.Tasks;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Contract
{
    public interface IProvinciaService
    {
        Task<IEnumerable<ProvinciaResponseDto>> Listar(ProvinciaRequestDto filtro);
        Task<IEnumerable<ProvinciaResponseDto>> ListarProvinciasSIAGIE(ProvinciaRequestDto filtro);
    }
}