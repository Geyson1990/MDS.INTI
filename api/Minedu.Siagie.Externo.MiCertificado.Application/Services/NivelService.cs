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
    public class NivelService : INivelService
    {
        private readonly INivelQuery _uow;
        private readonly IMapper _iMapper;

        public NivelService(IMapper iMapper, INivelQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<NivelResponseDto>> Listar(ModalidadRequestDto filtro)
        {
            filtro.IdSistema = filtro.IdSistema == "2" ? "" : filtro.IdSistema;
            var item = await _uow.Listar(_iMapper.Map<ModalidadRequestDto, USP_EXTERNO_CERTIFICADO_SEL_NIVELES_ALUMNO_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_EXTERNO_CERTIFICADO_SEL_NIVELES_ALUMNO_Result>,IEnumerable<NivelResponseDto>>(item);
        }
    }
}
