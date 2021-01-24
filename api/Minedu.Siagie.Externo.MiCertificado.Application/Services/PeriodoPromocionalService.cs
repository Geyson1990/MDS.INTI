using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Domain.Queries;
using Minedu.Siagie.Externo.MiCertificado.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Services
{
    public class PeriodoPromocionalService : IPeriodoPromocionalService
    {
        private readonly IPeriodoPromocionalQuery _uow;
        private readonly IMapper _iMapper;

        public PeriodoPromocionalService(IMapper iMapper, IPeriodoPromocionalQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<PeriodoPromocionalResponseDto>> ListarPeriodosPromocionales(PeriodoPromocionalDto filtro)
        {
            var item = await _uow.ListarPeriodosPromocionales(_iMapper.Map<PeriodoPromocionalDto, USP_SEL_PERIODOS_PROMOCIONALES_EBA_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_PERIODOS_PROMOCIONALES_EBA_Result>, IEnumerable<PeriodoPromocionalResponseDto>>(item);
        }
    }
}
