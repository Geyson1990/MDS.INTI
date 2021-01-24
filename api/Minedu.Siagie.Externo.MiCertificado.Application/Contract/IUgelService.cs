using System.Collections.Generic;
using System.Threading.Tasks;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Contract
{
    public interface IUgelService
    {
        Task<IEnumerable<UgelResponseDto>> Listar(UgelRequestDto filtro);
    }
}