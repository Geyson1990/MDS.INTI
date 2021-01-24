using AutoMapper;
using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Domain.Queries;
using Minedu.Siagie.Externo.MiCertificado.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Application.Services
{
    public class InstitucionEducativaService : IInstitucionEducativaService
    {
        private readonly IInstitucionEducativaQuery _uow;
        private readonly IMapper _iMapper;

        public InstitucionEducativaService(IMapper iMapper, IInstitucionEducativaQuery unitOfWork)
        {
            _iMapper = iMapper;
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<InstitucionEducativaResponseDto>> Listar(InstitucionEducativaRequestDto filtro)
        {            
            var item = await _uow.Listar(_iMapper.Map<InstitucionEducativaRequestDto, USP_SEL_IE_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_IE_Result>,IEnumerable<InstitucionEducativaResponseDto>>(item);
        }

        public async Task<IEnumerable<InstitucionEducativaPorCodigoResponseDto>> ListarDatosInstitucionesEducativas(InstitucionEducativaPorCodigoRequestDto filtro)
        {
            var item = await _uow.ListarDatosInstitucionesEducativas(_iMapper.Map<InstitucionEducativaPorCodigoRequestDto, USP_SEL_IE_CODMOD_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_IE_CODMOD_Result>, IEnumerable<InstitucionEducativaPorCodigoResponseDto>>(item);
        }

        public async Task<IEnumerable<InstitucionEducativaPorDreUgelResponseDto>> ListarInstitucionEducativaPorCodigoModular(InstitucionEducativaPorDreUgelRequestDto filtro)
        {
            var item = await _uow.ListarInstitucionEducativaPorCodigoModular(_iMapper.Map<InstitucionEducativaPorDreUgelRequestDto, SEL_DATOS_INSTITUCION_EDUCATIVA_Request>(filtro));
            return _iMapper.Map<IEnumerable<SEL_DATOS_INSTITUCION_EDUCATIVA_Result>, IEnumerable<InstitucionEducativaPorDreUgelResponseDto>>(item);
        }

        public async Task<IEnumerable<EstudianteColegioNivelResponseDto>> ListarInstitucionEducativaNiveles(EstudianteModalidadNivelRequestDto filtro)
        {
            filtro.IdSistema = filtro.IdSistema == "2" ? "" : filtro.IdSistema;
            var item = await _uow.ListarInstitucionEducativaNiveles(_iMapper.Map<EstudianteModalidadNivelRequestDto, USP_SEL_DATOS_IE_X_NIVEL_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_DATOS_IE_X_NIVEL_Result>, IEnumerable<EstudianteColegioNivelResponseDto>>(item);
        }

        public async Task<IEnumerable<ColegioPadronResponseDto>> ListarInstitucionEducativaPorPadrones(ColegioRequestDto filtro)
        {
            var item = await _uow.ListarInstitucionEducativaPorPadrones(_iMapper.Map<ColegioRequestDto, USP_SEL_IIEE_PADRON_Request>(filtro));
            return _iMapper.Map<IEnumerable<USP_SEL_IIEE_PADRON_Result>, IEnumerable<ColegioPadronResponseDto>>(item);
        }

        public async Task<StatusResponse<IEnumerable<object>>> ConsultarDatosIECunaJardin(string codigoModular, string anexo)
        {
            var response = new StatusResponse<IEnumerable<object>>();
            if (!string.IsNullOrEmpty(codigoModular) && !string.IsNullOrEmpty(anexo))
            {
                var objetoIE = new InstitucionEducativaPorCodigoRequestDto { CodigoModular = codigoModular, anexo = anexo };
                //Preguntar si es Nivel A1
                var datosIE = await _uow.ListarDatosInstitucionesEducativas(_iMapper.Map<InstitucionEducativaPorCodigoRequestDto, USP_SEL_IE_CODMOD_Request>(objetoIE));
                //Preguntar si hay Niveles A1
                var numeroIEs = datosIE.Where(x => x.ID_NIVEL == "A1" || x.ID_NIVEL == "E0");
                if (numeroIEs.Count() > 0)
                {
                    var IdNivel = datosIE.FirstOrDefault().ID_NIVEL;
                    response.Data = null;
                    response.Success = false;
                    response.Message = "El nivel "+IdNivel+" no es contemplado para el proyecto MiCertificado.";
                    return response;
                }
            }            
            response.Success = true;
            return response;
        }


    }
}
