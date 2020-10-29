using MuniSayan.BusinessLogic.Models.Constancia;
using MuniSayan.DataAccess.Contracts.Entities.Constancia;

namespace MuniSayan.Application.Mappers.Constancia
{
    public class NotaMapper
    {
        public static NotaEntity Map(NotaModel dto)
        {
            return new NotaEntity()
            {
                ID_CONSTANCIA_NOTA = dto.idConstanciaNota,
                ID_SOLICITUD = dto.idSolicitud,
                ID_ANIO = dto.idAnio,
                COD_MOD = dto.codMod,
                ANEXO = dto.anexo,
                ID_NIVEL = dto.idNivel,
                DSC_NIVEL = dto.dscNivel,
                ID_GRADO = dto.idGrado,
                DSC_GRADO = dto.dscGrado,
                ID_TIPO_AREA = dto.idTipoArea,
                DSC_TIPO_AREA = dto.dscTipoArea,
                ID_AREA = dto.idArea,
                DSC_AREA = dto.dscArea,
                ID_ASIGNATURA = dto.idAsignatura,
                DSC_ASIGNATURA = dto.dscAsignatura,
                ESCONDUCTA = dto.esconducta,
                NOTA_FINAL_AREA = dto.notaFinalArea
            };
        }

        public static NotaModel Map(NotaEntity entity)
        {
            return new NotaModel()
            {
                idConstanciaNota = entity.ID_CONSTANCIA_NOTA,
                idSolicitud = entity.ID_SOLICITUD,
                idAnio = entity.ID_ANIO,
                codMod = entity.COD_MOD,
                anexo = entity.ANEXO,
                idNivel = entity.ID_NIVEL,
                dscNivel = entity.DSC_NIVEL,
                idGrado = entity.ID_GRADO,
                dscGrado = entity.DSC_GRADO,
                idTipoArea = entity.ID_TIPO_AREA,
                dscTipoArea = entity.DSC_TIPO_AREA,
                idArea = entity.ID_AREA,
                dscArea = entity.DSC_AREA,
                idAsignatura = entity.ID_ASIGNATURA,
                dscAsignatura = entity.DSC_ASIGNATURA,
                esconducta = entity.ESCONDUCTA,
                notaFinalArea = entity.NOTA_FINAL_AREA
            };
        }
    }
}
