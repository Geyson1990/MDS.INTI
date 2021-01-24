using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class ModalidadMap: Profile
    {
        public ModalidadMap()
        {
            /*
           CreateMap<ApoderadoRequestDto, USP_EXTERNO_CERTIFICADO_SEL_APODERADOS_POR_ESTUDIANTE_Request>()
              .ForMember(des => des.ID_TIPO_DOCUMENTO, opt => opt.MapFrom(src => src.IdTipoDocumento))
              .ForMember(des => des.NUMERO_DOCUMENTO, opt => opt.MapFrom(src => src.NumeroDocumento))
              .ForMember(des => des.ID_SISTEMA, opt => opt.MapFrom(src => src.IdSistema));
           
              //.ForMember(des => des.ID_GRADO, opt => opt.MapFrom(src => src.GradoId))
              //.ForMember(des => des.ID_SECCION, opt => opt.MapFrom(src => src.SeccionId))
              ;
            */ 

        CreateMap<USP_SEL_MODALIDADES_IIEE_Result, ModalidadResponseDto>()
                .ForMember(des => des.IdModalidad, opt => opt.MapFrom(src => src.ID_MODALIDAD))
                .ForMember(des => des.DscModalidad, opt => opt.MapFrom(src => src.DSC_MODALIDAD));
        }
    }
}
