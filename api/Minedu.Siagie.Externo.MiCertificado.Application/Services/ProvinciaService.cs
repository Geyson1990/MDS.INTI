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
    public class ProvinciaService : IProvinciaService
    {
        private readonly IProvinciaQuery _uow;
        private readonly IMapper _iMapper;

        public ProvinciaService(IMapper iMapper, IProvinciaQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<ProvinciaResponseDto>> Listar(ProvinciaRequestDto filtro)
        {            
            var item = await _uow.Listar(_iMapper.Map<ProvinciaRequestDto, USP_SEL_PROVINCIA_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_PROVINCIA_Result>,IEnumerable<ProvinciaResponseDto>>(item);
        }

        public async Task<IEnumerable<ProvinciaResponseDto>> ListarProvinciasSIAGIE(ProvinciaRequestDto filtro)
        {
            var item = await _uow.ListarProvinciasSIAGIE(_iMapper.Map<ProvinciaRequestDto, USP_SEL_PROVINCIA_SIAGIE_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_PROVINCIA_SIAGIE_Result>, IEnumerable<ProvinciaResponseDto>>(item);
        }
    }
}
