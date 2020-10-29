﻿using MuniSayan.Application.Contracts.Security;
using MuniSayan.BusinessLogic.Models.Certificado;
using MuniSayan.DataAccess.Contracts.Entities.Certificado;

namespace MuniSayan.Application.Mappers.Certificado
{
    public static class MenuMapper
    {
        public static MenuCertificadoEntity Map(MenuCertificadoModel dto, IEncryptionServerSecurity encryptionServerSecurity)
        {
            return new MenuCertificadoEntity()
            {
                ID_MENU = encryptionServerSecurity.Decrypt<int>(dto.idMenu, 0),
                URL = dto.ruta,
                NOMBRE_ICONO = dto.nombreIcono,
                DESCRIPCION_CORTA = dto.descripcionCorta,
                DESCRIPCION = dto.descripcion
            };
        }

        public static MenuCertificadoModel Map(MenuCertificadoEntity entity, IEncryptionServerSecurity encryptionServerSecurity)
        {
            return new MenuCertificadoModel()
            {
                idMenu = encryptionServerSecurity.Encrypt(entity.ID_MENU.ToString()),
                ruta = entity.URL,
                nombreIcono = entity.NOMBRE_ICONO,
                descripcionCorta = entity.DESCRIPCION_CORTA,
                descripcion = entity.DESCRIPCION
            };
        }
    }
}
