using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_EXTERNO_MATERIALES_SEL_APODERADOS_POR_ESTUDIANTE_Request
    {
        public string COD_MOD { get; set; }
        public string ANEXO { get; set; }
        public Int16 ID_ANIO { get; set; }
        public string ID_NIVEL { get; set; }
        public string CODIGO_ESTUDIANTE { get; set; }
        public string ID_GRADO { get; set; }
        public string ID_SECCION { get; set; }

    }
}
