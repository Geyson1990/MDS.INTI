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
    public class UgelService : IUgelService
    {
        private readonly IUgelQuery _uow;
        private readonly IMapper _iMapper;

        public UgelService(IMapper iMapper, IUgelQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<UgelResponseDto>> Listar(UgelRequestDto filtro)
        {            
            var item = await _uow.Listar(_iMapper.Map<UgelRequestDto, USP_SEL_UGEL_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_UGEL_Result>,IEnumerable<UgelResponseDto>>(item);
        }
    }
}
