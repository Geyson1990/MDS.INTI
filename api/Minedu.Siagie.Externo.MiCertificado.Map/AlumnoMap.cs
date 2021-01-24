using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class AlumnoMap: Profile
    {
        public AlumnoMap()
        {
            CreateMap<EstudianteInfoPorCodModularRequestDto, USP_SEL_DATOS_ALUMNO_X_COD_MODULAR_Request>() 
              .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo));



            CreateMap<USP_SEL_DATOS_ALUMNO_X_COD_MODULAR_Result, EstudianteInfoPorCodModularResponseDto>()
                .ForMember(des => des.CodigoModular, opt => opt.MapFrom(src => src.COD_MOD))
                .ForMember(des => des.CenEdu, opt => opt.MapFrom(src => src.CEN_EDU))
                .ForMember(des => des.IdNivel, opt => opt.MapFrom(src => src.ID_NIVEL))
                .ForMember(des => des.DscNivel, opt => opt.MapFrom(src => src.DSC_NIVEL))
                .ForMember(des => des.IdModalidad, opt => opt.MapFrom(src => src.ID_MODALIDAD))
                .ForMember(des => des.AbrModalidad, opt => opt.MapFrom(src => src.ABR_MODALIDAD))
                .ForMember(des => des.DscModalidad, opt => opt.MapFrom(src => src.DSC_MODALIDAD))
                .ForMember(des => des.IdGrado, opt => opt.MapFrom(src => src.ID_GRADO))
                .ForMember(des => des.DscGrado, opt => opt.MapFrom(src => src.DSC_GRADO))
                .ForMember(des => des.Director, opt => opt.MapFrom(src => src.DIRECTOR)); 
    }
    }
}
