using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_ESTUDIANTES_CON_MATRICULA_ACTUAL_Result
    {
        public string COD_MOD { get; set; }
        public string ANEXO { get; set; }
        public int ID_ANIO { get; set; }
        public int ID_PERSONA { get; set; }
        public int ESTADO { get; set; }
        public byte VALIDADO_RENIEC { get; set; }
        public string ID_NIVEL { get; set; }
    }
}
