using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_EXTERNO_MATERIALES_SEL_APODERADOS_POR_ESTUDIANTE_Result
    {
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string TIPO_PARENTESCO { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }
        public string NUMERO_CELULAR { get; set; }
        public string NUMERO_TELEFONO { get; set; }
        public bool ES_VALIDADO_RENIEC { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
    }
}
