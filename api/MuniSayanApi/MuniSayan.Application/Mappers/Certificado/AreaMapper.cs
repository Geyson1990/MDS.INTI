using MuniSayan.Application.Contracts.Security;
using MuniSayan.BusinessLogic.Models.Certificado;
using MuniSayan.DataAccess.Contracts.Entities.Certificado;
using System;

namespace MuniSayan.Application.Mappers.Certificado
{
    public static class AreaMapper
    {
        public static AreaCertificadoEntity Map(AreaModel dto)
        {
            return new AreaCertificadoEntity()
            {
                ID_AREA = dto.idArea,
                DSC_AREA = dto.descripcionArea,
                ID_NIVEL = dto.nivel,
                ID_TIPO_AREA = dto.codigoTipoArea
            };
        }
 
        public static AreaModel Map(AreaCertificadoEntity entity)
        {
            return new AreaModel()
            {
                idArea = entity.ID_AREA,
                descripcionArea = entity.DSC_AREA.ToString(),
                nivel = entity.ID_NIVEL,
                codigoTipoArea = entity.ID_TIPO_AREA

            };
        }
    }
}
