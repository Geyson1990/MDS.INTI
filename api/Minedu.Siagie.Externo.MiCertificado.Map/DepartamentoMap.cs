using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class DepartamentoMap: Profile
    {
        public DepartamentoMap()
        {
           CreateMap<USP_SEL_DEPARTAMENTO_Result, DepartamentoResponseDto>()
                    .ForMember(des => des.CodigoDepartamento, opt => opt.MapFrom(src => src.COD_DEPARTAMENTO))
                    .ForMember(des => des.Departamento, opt => opt.MapFrom(src => src.DEPARTAMENTO));

            CreateMap<USP_SEL_DEPARTAMENTO_SIAGIE_Result, DepartamentoResponseDto>()
                  .ForMember(des => des.CodigoDepartamento, opt => opt.MapFrom(src => src.COD_DEPARTAMENTO))
                  .ForMember(des => des.Departamento, opt => opt.MapFrom(src => src.DEPARTAMENTO));
        }
    }
}
