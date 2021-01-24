using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class ApoderadoMap: Profile
    {
        public ApoderadoMap()
        {
            CreateMap<ApoderadoRequestDto, USP_SEL_APODERADOS_X_DOCUMENTO_Request>()
              .ForMember(des => des.ID_TIPO_DOCUMENTO, opt => opt.MapFrom(src => src.TipoDocumento))
              .ForMember(des => des.NUMERO_DOCUMENTO, opt => opt.MapFrom(src => src.NumeroDocumento));
           
            CreateMap<USP_SEL_APODERADOS_X_DOCUMENTO_Result, ApoderadoResponseDto>()
                .ForMember(des => des.IdPersonaApoderado, opt => opt.MapFrom(src => src.ID_PERSONA_APODERADO))
                .ForMember(des => des.IdPersonaEstudiante, opt => opt.MapFrom(src => src.ID_PERSONA_ESTUDIANTE));

            CreateMap<ApoderadoEstudianteRequestDto, USP_SEL_APODERADO_CON_ESTUDIANTE_Request>()
              .ForMember(des => des.ID_PERSONA_APODERADO, opt => opt.MapFrom(src => src.IdPersonaApoderado))
              .ForMember(des => des.ID_PERSONA_ESTUDIANTE, opt => opt.MapFrom(src => src.IdPersonaEstudiante));

            CreateMap<USP_SEL_APODERADO_CON_ESTUDIANTE_Result, ApoderadoEstudianteResponseDto>()
                .ForMember(des => des.IdPersonaApoderado, opt => opt.MapFrom(src => src.ID_PERSONA_APODERADO))
                .ForMember(des => des.IdPersonaEstudiante, opt => opt.MapFrom(src => src.ID_PERSONA_ESTUDIANTE))
                .ForMember(des => des.IdMatricula, opt => opt.MapFrom(src => src.ID_MATRICULA));

        }
    }
}
