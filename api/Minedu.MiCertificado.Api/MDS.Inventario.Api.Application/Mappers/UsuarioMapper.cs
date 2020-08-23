using MDS.Inventario.Api.Application.Entities.Models;
using MDS.Inventario.Api.DataAccess.Contracts.Entities;
using MDS.Inventario.Api.DataAccess.Contracts.Entities.Certificado;

namespace MDS.Inventario.Api.Application.Mappers
{
    public static class UsuarioMapper
    {
        public static UsuarioEntity Map(UsuarioExtends dto)
        {
            return new UsuarioEntity()
            {
                ID_USUARIO = dto.IdUsuario,
                ID_PERSONAL = dto.Idpersonal,
                ID_TIPO_DOCUMENTO = dto.IdTipoDocumento,
                NUMERO_DOCUMENTO = dto.NumeroDocumento,
                CONTRASENIA = dto.Contrasenia,
                NOMBRE_COMPLETO = dto.NombreCompleto,
                TOKEN = dto.Token,
                USUARIO = dto.NombreUsuario,
                ID_ROL = dto.IdRol,
                DSC_ROL = dto.DscRol
            };
        }

        public static UsuarioExtends Map(UsuarioEntity entity)
        {
            return new UsuarioExtends()
            {
                IdUsuario = entity.ID_USUARIO,
                Idpersonal = entity.ID_PERSONAL,
                IdTipoDocumento = entity.ID_TIPO_DOCUMENTO,
                NumeroDocumento = entity.NUMERO_DOCUMENTO,
                Contrasenia = entity.CONTRASENIA,
                NombreCompleto = entity.NOMBRE_COMPLETO,
                Token = entity.TOKEN,
                NombreUsuario = entity.USUARIO,
                IdRol = entity.ID_ROL,
                DscRol = entity.DSC_ROL
            };
        }
    }
}
