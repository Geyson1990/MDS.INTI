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
    public class ModalidadService : IModalidadService
    {
        private readonly IModalidadQuery _uow;
        private readonly IMapper _iMapper;

        public ModalidadService(IMapper iMapper, IModalidadQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<ModalidadResponseDto>> Listar()
        {            
            var item = await _uow.Listar();
            return _iMapper.Map<IEnumerable<USP_SEL_MODALIDADES_IIEE_Result>,IEnumerable<ModalidadResponseDto>>(item);
        }
    }
}
