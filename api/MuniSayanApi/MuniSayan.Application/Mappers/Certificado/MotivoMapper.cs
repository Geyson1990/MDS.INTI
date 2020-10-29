using MuniSayan.Application.Contracts.Security;
using MuniSayan.BusinessLogic.Models.Certificado;
using MuniSayan.DataAccess.Contracts.Entities.Certificado;
using System;

namespace MuniSayan.Application.Mappers.Certificado
{
    public static class MotivoMapper
    {
        public static MotivoCertificadoEntity Map(MotivoCertificadoModel dto, IEncryptionServerSecurity encryptionServerSecurity)
        {
            return new MotivoCertificadoEntity()
            {
                ID_MOTIVO = encryptionServerSecurity.Decrypt<int>(dto.idMotivo, 0),
                DESCRIPCION = dto.descripcion,
                REQUIERE_DETALLE = dto.requiereDetalle
            };
        }

        public static MotivoCertificadoModel Map(MotivoCertificadoEntity entity, IEncryptionServerSecurity encryptionServerSecurity)
        {
            return new MotivoCertificadoModel()
            {
                idMotivo = encryptionServerSecurity.Encrypt(entity.ID_MOTIVO.ToString()),
                descripcion = entity.DESCRIPCION,
                requiereDetalle = entity.REQUIERE_DETALLE
            };
        }
    }
}
