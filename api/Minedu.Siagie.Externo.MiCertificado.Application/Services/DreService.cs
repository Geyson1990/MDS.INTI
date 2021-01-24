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
    public class DreService : IDreService
    {
        private readonly IDreQuery _uow;
        private readonly IMapper _iMapper;

        public DreService(IMapper iMapper, IDreQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<DreResponseDto>> Listar()
        {            
            var item = await _uow.Listar();
            return _iMapper.Map<IEnumerable<USP_SEL_DRE_Result>,IEnumerable<DreResponseDto>>(item);
        }
    }
}
