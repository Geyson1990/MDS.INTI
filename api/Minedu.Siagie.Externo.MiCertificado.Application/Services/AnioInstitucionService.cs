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
    public class AnioInstitucionService : IAnioInstitucionService
    {
        private readonly IAnioInstitucionQuery _uow;
        private readonly IMapper _iMapper;

        public AnioInstitucionService(IMapper iMapper, IAnioInstitucionQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<AnioInstitucionResponseDto>> Listar(AnioInstitucionRequestDto filtro)
        {            
            var item = await _uow.Listar(_iMapper.Map<AnioInstitucionRequestDto, USP_SEL_ANIOS_X_IE_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_ANIOS_X_IE_Result>,IEnumerable<AnioInstitucionResponseDto>>(item);
        }
    }
}
