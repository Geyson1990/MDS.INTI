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
    public class DistritoService : IDistritoService
    {
        private readonly IDistritoQuery _uow;
        private readonly IMapper _iMapper;

        public DistritoService(IMapper iMapper, IDistritoQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<DistritoResponseDto>> Listar(DistritoRequestDto filtro)
        {            
            var item = await _uow.Listar(_iMapper.Map<DistritoRequestDto, USP_SEL_DISTRITO_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_DISTRITO_Result>,IEnumerable<DistritoResponseDto>>(item);
        }
        public async Task<IEnumerable<DistritoResponseDto>> ListarDistritosSIAGIE(DistritoRequestDto filtro)
        {
            var item = await _uow.ListarDistritosSIAGIE(_iMapper.Map<DistritoRequestDto, USP_SEL_DISTRITO_SIAGIE_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_DISTRITO_SIAGIE_Result>, IEnumerable<DistritoResponseDto>>(item);
        }

        
    }
}
