using System.Collections.Generic;
using System.Threading.Tasks;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Contract
{
    public interface IAnioInstitucionService
    {
        Task<IEnumerable<AnioInstitucionResponseDto>> Listar(AnioInstitucionRequestDto filtro);
    }
}