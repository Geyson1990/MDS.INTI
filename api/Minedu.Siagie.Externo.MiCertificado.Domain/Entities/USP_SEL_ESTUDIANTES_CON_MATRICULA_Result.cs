using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_ESTUDIANTES_CON_MATRICULA_Result
    {
        public int ID_PERSONA_ESTUDIANTE { get; set; }
        public int ID_MATRICULA { get; set; }
        public string COD_MOD { get; set; }
        public string ANEXO { get; set; }  
        public string ID_NIVEL { get; set; }
        public string DSC_NIVEL { get; set; }
    }
}
