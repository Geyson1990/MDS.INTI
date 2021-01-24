using AutoMapper;
using Minedu.Core.General.Communication;
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
    public class ApoderadoService : IApoderadoService
    {
        private readonly IApoderadoQuery _uow;
        private readonly IMapper _iMapper;

        public ApoderadoService(IMapper iMapper, IApoderadoQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<ApoderadoResponseDto>> Listar(ApoderadoRequestDto filtro)
        {
            var item = await _uow.Listar(_iMapper.Map<ApoderadoRequestDto, USP_SEL_APODERADOS_X_DOCUMENTO_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_APODERADOS_X_DOCUMENTO_Result>,IEnumerable<ApoderadoResponseDto>>(item);
        }

        public async Task<IEnumerable<ApoderadoEstudianteResponseDto>> ListarApoderadosEstudiantesConMatricula(ApoderadoEstudianteRequestDto filtro)
        {
            var item = await _uow.ListarApoderadosEstudiantesConMatricula(_iMapper.Map<ApoderadoEstudianteRequestDto, USP_SEL_APODERADO_CON_ESTUDIANTE_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_APODERADO_CON_ESTUDIANTE_Result>, IEnumerable<ApoderadoEstudianteResponseDto>>(item);
        }
    }
}
