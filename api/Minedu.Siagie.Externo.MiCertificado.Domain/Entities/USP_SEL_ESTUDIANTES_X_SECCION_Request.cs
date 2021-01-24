using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_ESTUDIANTES_X_SECCION_Request
    {
        public string COD_MOD { get; set; }
        public string ANEXO { get; set; }
        public int ID_ANIO { get; set; } 
        public string ID_NIVEL { get; set; }
        public string ID_GRADO { get; set; }
        public string ID_SECCION { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public string NOMBRES_ESTUDIANTE { get; set; } 

    }
}
