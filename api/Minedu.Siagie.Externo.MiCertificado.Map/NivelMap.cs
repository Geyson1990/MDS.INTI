using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class NivelMap: Profile
    {
        public NivelMap()
        {
            CreateMap<ModalidadRequestDto, USP_EXTERNO_CERTIFICADO_SEL_NIVELES_ALUMNO_Request>() 
              .ForMember(des => des.ID_PERSONA, opt => opt.MapFrom(src => src.IdPersona))
              .ForMember(des => des.ID_MODALIDAD, opt => opt.MapFrom(src => src.IdModalidad))
              .ForMember(des => des.ID_SISTEMA, opt => opt.MapFrom(src => src.IdSistema));


            CreateMap<USP_EXTERNO_CERTIFICADO_SEL_NIVELES_ALUMNO_Result, NivelResponseDto>()
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.DscNivel, opt => opt.MapFrom(src => src.DSC_NIVEL));               

    }
    }
}
