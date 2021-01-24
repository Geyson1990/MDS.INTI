using Minedu.Siagie.Externo.MiCertificado.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Contract
{
    public interface IPeriodoPromocionalService
    {
        Task<IEnumerable<PeriodoPromocionalResponseDto>> ListarPeriodosPromocionales(PeriodoPromocionalDto filtro);
    }
}
