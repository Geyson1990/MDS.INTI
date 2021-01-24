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
    public class GradoService : IGradoService
    {
        private readonly IGradoQuery _uow;
        private readonly IMapper _iMapper;

        public GradoService(IMapper iMapper, IGradoQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<GradoNivelResponseDto>> ListarNivel(GradoNivelRequestDto filtro)
        {            
            var item = await _uow.ListarNivel(_iMapper.Map<GradoNivelRequestDto, USP_SEL_GRADOS_X_NIVEL_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_GRADOS_X_NIVEL_Result>,IEnumerable<GradoNivelResponseDto>>(item);
        }

        public async Task<IEnumerable<GradoSeccionResponseDto>> ListarSeccion(GradoSeccionRequestDto filtro)
        {
            var item = await _uow.ListarSeccion(_iMapper.Map<GradoSeccionRequestDto, USP_SEL_GRADO_SECCION_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_GRADO_SECCION_Result>, IEnumerable<GradoSeccionResponseDto>>(item);
        }

        public async Task<IEnumerable<GradoSeccionResponseDto>> ListarSeccionEBA(GradoSeccionEBARequestDto filtro)
        {
            var item = await _uow.ListarSeccionEBA(_iMapper.Map<GradoSeccionEBARequestDto, USP_SEL_GRADO_SECCION_EBA_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_GRADO_SECCION_Result>, IEnumerable<GradoSeccionResponseDto>>(item);
        }


    }
}
