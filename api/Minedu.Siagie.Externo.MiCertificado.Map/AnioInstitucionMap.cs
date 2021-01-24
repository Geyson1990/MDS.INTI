using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class AnioInstitucionMap: Profile
    {
        public AnioInstitucionMap()
        {
            CreateMap<AnioInstitucionRequestDto, USP_SEL_ANIOS_X_IE_Request>() 
                .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo));


            CreateMap<USP_SEL_ANIOS_X_IE_Result, AnioInstitucionResponseDto>()
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.Anexo, opt => opt.MapFrom(src => src.ANEXO))
                .ForMember(des => des.IdAnio, opt => opt.MapFrom(src => src.ID_ANIO)); 
    }
    }
}
