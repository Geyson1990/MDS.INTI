using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_DATOS_ALUMNO_X_NIVEL_Request
    {
        public int ID_PERSONA { get; set; }
        public string ID_MODALIDAD { get; set; }
        public string ID_NIVEL { get; set; }
        public string ID_SISTEMA { get; set; }
    }
}
