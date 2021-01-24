using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class GradoSeccionMap : Profile
    {
        public GradoSeccionMap()
        {
            CreateMap<GradoSeccionRequestDto, USP_SEL_GRADO_SECCION_Request>()
                .ForMember(des => des.CODIGO_MODULAR, opt => opt.MapFrom(src => src.CodigoModular))
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo))
                .ForMember(des => des.ID_ANIO, opt => opt.MapFrom(src => src.IdAnio))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                .ForMember(des => des.ID_GRADO, opt => opt.MapFrom(src => src.IdGrado))
                .ForMember(des => des.ID_SECCION, opt => opt.MapFrom(src => src.IdSeccion))
                .ForMember(des => des.ID_FASE, opt => opt.MapFrom(src => src.IdFase));

            CreateMap<USP_SEL_GRADO_SECCION_Result, GradoSeccionResponseDto>()
                    .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                    .ForMember(des => des.IdSeccion, opt => opt.MapFrom(src => src.ID_SECCION))
                    .ForMember(des => des.DscGrado, opt => opt.MapFrom(src => src.DSC_GRADO))
                    .ForMember(des => des.DscSeccion, opt => opt.MapFrom(src => src.DSC_SECCION));

            CreateMap<GradoSeccionEBARequestDto, USP_SEL_GRADO_SECCION_EBA_Request>()
                .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo))
                .ForMember(des => des.ID_ANIO, opt => opt.MapFrom(src => src.IdAnio))
                .ForMember(des => des.ID_FASE_POR_PERIODO_PROM_IE, opt => opt.MapFrom(src => src.IdFasePeriodoPromIE))
                .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel))
                ;

        }
    }
}
