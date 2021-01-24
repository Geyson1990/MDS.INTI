using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class ProvinciaMap: Profile
    {
        public ProvinciaMap()
        {
            CreateMap<ProvinciaRequestDto, USP_SEL_PROVINCIA_Request>() 
              .ForMember(des => des.COD_DEPARTAMENTO, opt => opt.MapFrom(src => src.CodigoDepartamento));            

            CreateMap<USP_SEL_PROVINCIA_Result, ProvinciaResponseDto>()
                .ForMember(des => des.CodigoProvincia, opt => opt.MapFrom(src => src.COD_PROVINCIA))
                .ForMember(des => des.Provincia, opt => opt.MapFrom(src => src.PROVINCIA));



            CreateMap<ProvinciaRequestDto, USP_SEL_PROVINCIA_SIAGIE_Request>()
              .ForMember(des => des.COD_DEPARTAMENTO, opt => opt.MapFrom(src => src.CodigoDepartamento));

            CreateMap<USP_SEL_PROVINCIA_SIAGIE_Result, ProvinciaResponseDto>()
                .ForMember(des => des.CodigoProvincia, opt => opt.MapFrom(src => src.COD_PROVINCIA))
                .ForMember(des => des.Provincia, opt => opt.MapFrom(src => src.PROVINCIA)); 
        }
    }
}
