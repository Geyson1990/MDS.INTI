using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class ColegioMap: Profile
    {
        public ColegioMap()
        {
            CreateMap<EstudianteModalidadNivelRequestDto, USP_SEL_DATOS_IE_X_NIVEL_Request>() 
              .ForMember(des => des.ID_PERSONA, opt => opt.MapFrom(src => src.IdPersona))
              .ForMember(des => des.ID_MODALIDAD, opt => opt.MapFrom(src => src.IdModalidad))
              .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
              .ForMember(des => des.ID_SISTEMA, opt => opt.MapFrom(src => src.IdSistema));


            CreateMap<EstudianteModalidadNivelModularRequestDto, USP_SEL_DATOS_IE_X_NIVEL_Request>()
              .ForMember(des => des.ID_PERSONA, opt => opt.MapFrom(src => src.IdPersona))
              .ForMember(des => des.ID_MODALIDAD, opt => opt.MapFrom(src => src.IdModalidad))
              .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
              .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))
              .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo));



            CreateMap<USP_SEL_DATOS_IE_X_NIVEL_Result, EstudianteColegioNivelResponseDto>()
                .ForMember(des => des.IdMatricula, opt => opt.MapFrom(src => src.ID_MATRICULA))        
                .ForMember(des => des.IdAnio, opt => opt.MapFrom(src => src.ID_ANIO))
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.CenEdu, opt => opt.MapFrom(src => src.CEN_EDU))
                .ForMember(des => des.DscModalidad, opt => opt.MapFrom(src => src.DSC_MODALIDAD))
                .ForMember(des => des.DscNivel, opt => opt.MapFrom(src => src.DSC_NIVEL))
                .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                .ForMember(des => des.DscGrado, opt => opt.MapFrom(src => src.DSC_GRADO))
                .ForMember(des => des.EstadoActa, opt => opt.MapFrom(src => src.ESTADO_FORMATO_SECCION))
                .ForMember(des => des.DscEstadoActa, opt => opt.MapFrom(src => src.DSC_ESTADO_FORMATO))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL));
 


            CreateMap<ColegioRequestDto, USP_SEL_IIEE_PADRON_Request>()
                .ForMember(des => des.DEPARTAMENTO, opt => opt.MapFrom(src => src.Departamento))
                .ForMember(des => des.PROVINCIA, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(des => des.UBIGEO, opt => opt.MapFrom(src => src.Ubigeo))
                .ForMember(des => des.CEN_EDU, opt => opt.MapFrom(src => src.CenEdu))
                .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo))
                .ForMember(des => des.COD_UGEL, opt => opt.MapFrom(src => src.CodigoUgel))
                .ForMember(des => des.ESTADO, opt => opt.MapFrom(src => src.Estado))
                .ForMember(des => des.PAGE_SIZE, opt => opt.MapFrom(src => src.PageSize)) 
                .ForMember(des => des.PAGE, opt => opt.MapFrom(src => src.Page));
 

        CreateMap<USP_SEL_IIEE_PADRON_Result, ColegioPadronResponseDto>()
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.CenEdu, opt => opt.MapFrom(src => src.CEN_EDU))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.DscNivel, opt => opt.MapFrom(src => src.DSC_NIVEL))
                .ForMember(des => des.IdModalidad, opt => opt.MapFrom(src => src.ID_MODALIDAD))
                .ForMember(des => des.DscModalidad, opt => opt.MapFrom(src => src.DSC_MODALIDAD))
                .ForMember(des => des.AbrModalidad, opt => opt.MapFrom(src => src.ABR_MODALIDAD))
                .ForMember(des => des.Departamento, opt => opt.MapFrom(src => src.DEPARTAMENTO))
                .ForMember(des => des.Provincia, opt => opt.MapFrom(src => src.PROVINCIA))
                .ForMember(des => des.Distrito, opt => opt.MapFrom(src => src.DISTRITO))
                .ForMember(des => des.DirCen, opt => opt.MapFrom(src => src.DIR_CEN))
                .ForMember(des => des.Ugel, opt => opt.MapFrom(src => src.UGEL))
                .ForMember(des => des.Dre, opt => opt.MapFrom(src => src.DRE))
                .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.ESTADO))
                .ForMember(des => des.DscEstado, opt => opt.MapFrom(src => src.DSC_ESTADO));
 
        }
    }
}
