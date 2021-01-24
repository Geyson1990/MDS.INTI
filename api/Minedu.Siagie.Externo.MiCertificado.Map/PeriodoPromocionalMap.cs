using AutoMapper;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Domain.Queries;
using Minedu.Siagie.Externo.MiCertificado.Dto;

namespace Minedu.Siagie.Externo.MiCertificado.Map
{
    public class PeriodoPromocionalMap : Profile
    {
        public PeriodoPromocionalMap()
        {
            CreateMap<PeriodoPromocionalDto, USP_SEL_PERIODOS_PROMOCIONALES_EBA_Request>()
                .ForMember(des => des.COD_MOD, opt => opt.MapFrom(src => src.CodigoModular))
                .ForMember(des => des.ANEXO, opt => opt.MapFrom(src => src.Anexo))
                .ForMember(des => des.ID_ANIO, opt => opt.MapFrom(src => src.IdAnio))
                ;

            CreateMap<USP_SEL_PERIODOS_PROMOCIONALES_EBA_Result, PeriodoPromocionalResponseDto>()
                    .ForMember(des => des.IdFasePorPeriodoPromoIE, opt => opt.MapFrom(src => src.ID_FASE_POR_PERIODO_PROM_IE))
                    .ForMember(des => des.IdPeriodoPromIE, opt => opt.MapFrom(src => src.ID_PERIODO_PROM_POR_IE))
                    .ForMember(des => des.PeriodoPromIENombre, opt => opt.MapFrom(src => src.PERIODO_PROM_POR_IE_NOMBRE))
                    ;
        }
    }
}
