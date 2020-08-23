using MDS.Inventario.Api.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDS.Inventario.Api.Application.Entities.Models
{
    public class UsuarioExtends: Usuario
    {
        public int IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Contrasenia { get; set; }
        public string NombreCompleto { get; set; }
        public string Token { get; set; }
        public string DscRol { get; set; }
    }
}
