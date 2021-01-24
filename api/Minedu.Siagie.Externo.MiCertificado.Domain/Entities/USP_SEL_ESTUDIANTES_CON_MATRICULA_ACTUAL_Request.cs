using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL_Request
    {
        public string ID_TIPO_DOCUMENTO { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }
        public string COD_MOD { get; set; }
        public string ANEXO { get; set; }
        public string ID_NIVEL { get; set; }
    }
}
