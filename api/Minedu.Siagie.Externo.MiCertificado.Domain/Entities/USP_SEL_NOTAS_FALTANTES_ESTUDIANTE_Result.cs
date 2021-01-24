using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_NOTAS_FALTANTES_ESTUDIANTE_Result
    {
        public int ID_ANIO { get; set; }
        public string COD_MOD { get; set; }
        public string ANEXO { get; set; }
        public int ID_PERSONA { get; set; }
        public string ID_NIVEL { get; set; }
        public string DSC_NIVEL { get; set; }
        public string ID_GRADO { get; set; }
        public string DSC_GRADO { get; set; }
        public string ID_TIPO_AREA { get; set; }
        public string DSC_TIPO_AREA { get; set; }
        public int ESCONDUCTA { get; set; }
        public string NOTA_FINAL_AREA { get; set; }
        public string ID_AREA { get; set; }
        public string DSC_AREA { get; set; }
        public string REGISTRO_NOTA { get; set; }
    }
}
