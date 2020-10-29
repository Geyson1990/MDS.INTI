using MuniSayan.BusinessLogic.Models;
using MuniSayan.DataAccess.Contracts.Entities;

namespace MuniSayan.Application.Mappers
{
    public static class CorreoMapper
    {
        public static CorreoEntity Map(CorreoModel dto)
        {
            return new CorreoEntity()
            {
                PARA = dto.para,
                CC = dto.cc,
                CCO = dto.cco,
                ASUNTO = dto.asunto,
                MENSAJE = dto.mensaje
            };
        }

        public static CorreoModel Map(CorreoEntity entity)
        {
            return new CorreoModel()
            {
                para = entity.PARA,
                cc = entity.CC,
                cco = entity.CCO,
                asunto = entity.ASUNTO,
                mensaje = entity.MENSAJE
            };
        }
    }
}
