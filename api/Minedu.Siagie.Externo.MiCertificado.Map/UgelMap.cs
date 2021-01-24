using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class UgelMap: Profile
    {
        public UgelMap()
        {
            CreateMap<UgelRequestDto, USP_SEL_UGEL_Request>() 
              .ForMember(des => des.COD_DRE, opt => opt.MapFrom(src => src.CodigoDre));
            

            CreateMap<USP_SEL_UGEL_Result, UgelResponseDto>()
                .ForMember(des => des.CodigoUgel, opt => opt.MapFrom(src => src.CODIGEL))
                .ForMember(des => des.Ugel, opt => opt.MapFrom(src => src.NOMIGEL));               

    }
    }
}
