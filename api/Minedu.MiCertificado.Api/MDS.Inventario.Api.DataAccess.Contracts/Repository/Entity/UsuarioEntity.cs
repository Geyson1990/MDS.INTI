using System;
using System.Collections.Generic;
using System.Text;

namespace MDS.Inventario.Api.DataAccess.Contracts.Entities.Certificado
{
    public class UsuarioEntity : usuario
    {
        public int ID_TIPO_DOCUMENTO { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string DSC_ROL { get; set; }
        public string TOKEN { get; set; }
    }
}
