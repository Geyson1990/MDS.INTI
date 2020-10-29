using MuniSayan.Application.Contracts.Security;
using MuniSayan.BusinessLogic.Models.Constancia;
using MuniSayan.DataAccess.Contracts.Entities.Constancia;

namespace MuniSayan.Application.Mappers.Constancia
{
    public static class MenuMapper
    {
        public static MenuEntity Map(MenuModel dto, IEncryptionServerSecurity encryptionServerSecurity)
        {
            return new MenuEntity()
            {
                ID_MENU = encryptionServerSecurity.Decrypt<int>(dto.idMenu, 0),
                URL = dto.ruta,
                NOMBRE_ICONO = dto.nombreIcono,
                DESCRIPCION_CORTA = dto.descripcionCorta,
                DESCRIPCION = dto.descripcion
            };
        }

        public static MenuModel Map(MenuEntity entity, IEncryptionServerSecurity encryptionServerSecurity)
        {
            return new MenuModel()
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
