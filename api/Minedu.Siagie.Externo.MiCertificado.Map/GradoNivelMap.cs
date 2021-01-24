using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class GradoNivelMap: Profile
    {
        public GradoNivelMap()
        {
            CreateMap<GradoNivelRequestDto, USP_SEL_GRADOS_X_NIVEL_Request>() 
              .ForMember(des => des.ID_MODALIDAD, opt => opt.MapFrom(src => src.IdModalidad))
             .ForMember(des => des.ID_NIVEL, opt => opt.MapFrom(src => src.IdNivel));


            CreateMap<USP_SEL_GRADOS_X_NIVEL_Result, GradoNivelResponseDto>()
                .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                .ForMember(des => des.DscGrado, opt => opt.MapFrom(src => src.DSC_GRADO));               

    }
    }
}
