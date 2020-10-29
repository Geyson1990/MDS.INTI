using MuniSayan.Application.Contracts.Security;
using MuniSayan.BusinessLogic.Models.Certificado;
using MuniSayan.DataAccess.Contracts.Entities.Certificado;
using System;

namespace MuniSayan.Application.Mappers.Certificado
{
    public static class AnioMapper
    {
        public static SolicitudExtend Map(AnioPorSolicitudResponse dto)
        {
            return new SolicitudExtend()
            {
                ANIO_CULMINACION = dto.IdAnio
            };
        }
 
        public static AnioPorSolicitudResponse Map(SolicitudExtend entity)
        {
            return new AnioPorSolicitudResponse()
            {
                IdAnio = entity.ANIO_CULMINACION

            };
        }
    }
}
