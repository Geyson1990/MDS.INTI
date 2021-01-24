using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class InstitucionEducativaMap: Profile
    {
        public InstitucionEducativaMap()
        {
            CreateMap<InstitucionEducativaRequestDto, USP_SEL_IE_Request>() 
              .ForMember(des => des.COD_UGEL, opt => opt.MapFrom(src => src.codUgel))
              .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.idNivel)); 

            CreateMap<USP_SEL_IE_Result, InstitucionEducativaResponseDto>()
                .ForMember(des => des.cod_mod, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.cen_edu, opt => opt.MapFrom(src => src.CEN_EDU));

            CreateMap<InstitucionEducativaPorCodigoRequestDto, USP_SEL_IE_CODMOD_Request>() 
                .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))     
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.anexo));  

            CreateMap<USP_SEL_IE_CODMOD_Result, InstitucionEducativaPorCodigoResponseDto>()
                .ForMember(des => des.CenEdu, opt => opt.MapFrom(src => src.NOMBRE_IE))
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.CodigoDre, opt => opt.MapFrom(src => src.COD_DRE))
                .ForMember(des => des.NombreDre, opt => opt.MapFrom(src => src.NOMBRE_DRE))
                .ForMember(des => des.CodigoUgel, opt => opt.MapFrom(src => src.COD_UGEL))
                .ForMember(des => des.NombreUgel, opt => opt.MapFrom(src => src.NOMBRE_UGEL))
                .ForMember(des => des.Departamento, opt => opt.MapFrom(src => src.DEPARTAMENTO))
                .ForMember(des => des.Provincia, opt => opt.MapFrom(src => src.PROVINCIA))
                .ForMember(des => des.Distrito, opt => opt.MapFrom(src => src.DISTRITO))
                .ForMember(des => des.NombreDirector, opt => opt.MapFrom(src => src.NOMBRE_DIRECTOR))
                .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.ESTADO))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL));



            CreateMap<InstitucionEducativaPorDreUgelRequestDto, SEL_DATOS_INSTITUCION_EDUCATIVA_Request>()
                .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo))
                .ForMember(des => des.COD_DRE, opt => opt.MapFrom(src => src.CodigoDre))
                .ForMember(des => des.COD_UGEL, opt => opt.MapFrom(src => src.CodigoUgel))
                .ForMember(des => des.NOMBRE_IE, opt => opt.MapFrom(src => src.CenEdu))
                .ForMember(des => des.pageNumber, opt => opt.MapFrom(src => src.PageNumber))
                .ForMember(des => des.rowsPerPage, opt => opt.MapFrom(src => src.RowsPerPage))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel));

            CreateMap<SEL_DATOS_INSTITUCION_EDUCATIVA_Result, InstitucionEducativaPorDreUgelResponseDto>()
                    .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                    .ForMember(des => des.CenEdu, opt => opt.MapFrom(src => src.NOMBRE_IE))
                    .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                    .ForMember(des => des.CodigoDre, opt => opt.MapFrom(src => src.COD_DRE))
                    .ForMember(des => des.NombreDre, opt => opt.MapFrom(src => src.NOMBRE_DRE))
                    .ForMember(des => des.CodigoUgel, opt => opt.MapFrom(src => src.COD_UGEL))
                    .ForMember(des => des.NombreUgel, opt => opt.MapFrom(src => src.NOMBRE_UGEL))
                    .ForMember(des => des.Departamento, opt => opt.MapFrom(src => src.DEPARTAMENTO))
                    .ForMember(des => des.Provincia, opt => opt.MapFrom(src => src.PROVINCIA))
                    .ForMember(des => des.Distrito, opt => opt.MapFrom(src => src.DISTRITO))
                    .ForMember(des => des.TotalRegistros, opt => opt.MapFrom(src => src.TOTAL_REGISTROS))
                    .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL));

            CreateMap<EstudianteMatriculaNivelRequestDto, USP_SEL_DATOS_IE_X_NIVEL_Request>()
              .ForMember(des => des.ID_PERSONA, opt => opt.MapFrom(src => src.IdPersona))
              .ForMember(des => des.ID_MODALIDAD, opt => opt.MapFrom(src => src.IdModalidad))
              .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel));
        }
    }
}
