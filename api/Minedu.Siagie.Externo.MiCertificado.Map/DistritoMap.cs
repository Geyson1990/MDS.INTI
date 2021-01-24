using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class DistritoMap: Profile
    {
        public DistritoMap()
        {
            CreateMap<DistritoRequestDto, USP_SEL_DISTRITO_Request>() 
              .ForMember(des => des.COD_PROVINCIA, opt => opt.MapFrom(src => src.CodigoProvincia));            

            CreateMap<USP_SEL_DISTRITO_Result, DistritoResponseDto>()
                .ForMember(des => des.CodigoUbigeo, opt => opt.MapFrom(src => src.COD_UBIGEO))
                .ForMember(des => des.Distrito, opt => opt.MapFrom(src => src.DISTRITO));



            CreateMap<DistritoRequestDto, USP_SEL_DISTRITO_SIAGIE_Request>()
             .ForMember(des => des.COD_PROVINCIA, opt => opt.MapFrom(src => src.CodigoProvincia));

            CreateMap<USP_SEL_DISTRITO_SIAGIE_Result, DistritoResponseDto>()
                .ForMember(des => des.CodigoUbigeo, opt => opt.MapFrom(src => src.COD_UBIGEO))
                .ForMember(des => des.Distrito, opt => opt.MapFrom(src => src.DISTRITO));

        }
    }
}
