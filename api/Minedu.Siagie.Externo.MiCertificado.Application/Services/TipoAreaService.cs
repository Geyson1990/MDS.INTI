using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Domain.Queries;
using Minedu.Siagie.Externo.MiCertificado.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Services
{
    public class TipoAreaService : ITipoAreaService
    {
        private readonly ITipoAreaQuery _uow;
        private readonly IMapper _iMapper;

        public TipoAreaService(IMapper iMapper, ITipoAreaQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<TipoAreaResponseDto>> Listar()
        {            
            var item = await _uow.Listar();
            return _iMapper.Map<IEnumerable<USP_SEL_TIPO_AREA_Result>,IEnumerable<TipoAreaResponseDto>>(item);
        }
    }
}
