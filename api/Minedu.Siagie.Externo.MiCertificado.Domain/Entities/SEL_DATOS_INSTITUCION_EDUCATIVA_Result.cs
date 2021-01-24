using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class SEL_DATOS_INSTITUCION_EDUCATIVA_Result
    {
        public string NOMBRE_IE { get; set; }
        public string COD_MOD { get; set; }
        public string ANEXO { get; set; }
        public string COD_DRE { get; set; }
        public string NOMBRE_DRE { get; set; }
        public string COD_UGEL { get; set; }
        public string NOMBRE_UGEL { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string PROVINCIA { get; set; }
        public string DISTRITO { get; set; }
        public int TOTAL_REGISTROS { get; set; }
        public string ID_NIVEL { get; set; }
    }
}
