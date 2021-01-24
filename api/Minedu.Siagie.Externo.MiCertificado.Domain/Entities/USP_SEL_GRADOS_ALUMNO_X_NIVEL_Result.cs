using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_GRADOS_ALUMNO_X_NIVEL_Result
    {
        public string ID_GRADO { get; set; }
        public string DSC_GRADO { get; set; }
        public string CORR_ESTADISTICA { get; set; }
        public int ID_ANIO { get; set; }
        public string COD_MOD { get; set; }
        public string ANEXO { get; set; }
        public string SITUACION_FINAL { get; set; }
        public int ESTADO { get; set; }
        public string ID_NIVEL { get; set; }
        public string DSC_NIVEL { get; set; }
        public int EDAD_MINIMA { get; set; }
        public int EDAD_MAXIMA { get; set; }
    }
}
