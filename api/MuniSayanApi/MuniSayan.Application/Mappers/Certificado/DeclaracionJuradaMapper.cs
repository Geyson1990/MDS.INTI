using MuniSayan.Application.Contracts.Security;
using MuniSayan.BusinessLogic.Models.Certificado;
using MuniSayan.DataAccess.Contracts.Entities.Certificado;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuniSayan.Application.Mappers.Certificado
{
    public static class DeclaracionJuradaMapper
    {
        public static DeclaracionJuradaCertificadoEntity Map(DeclaracionJuradaCertificadoModel dto, IEncryptionServerSecurity encryptionServerSecurity)
        {
            return new DeclaracionJuradaCertificadoEntity()
            {
                ID_DECLARACION = encryptionServerSecurity.Decrypt<int>(dto.idDeclaracion, 0),
                DESCRIPCION = dto.descripcion
            };
        }

        public static DeclaracionJuradaCertificadoModel Map(DeclaracionJuradaCertificadoEntity entity, IEncryptionServerSecurity encryptionServerSecurity)
        {
            return new DeclaracionJuradaCertificadoModel()
            {
                idDeclaracion = encryptionServerSecurity.Encrypt(entity.ID_DECLARACION.ToString()),
                descripcion = entity.DESCRIPCION
            };
        }
    }
}
