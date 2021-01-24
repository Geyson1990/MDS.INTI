using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_DATOS_ALUMNO_X_NIVEL_Result
    {
        public int ID_PERSONA { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }
        public string ID_TIPO_DOCUMENTO { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public int ID_MATRICULA { get; set; }
        public int ID_ANIO { get; set; }
        public string COD_MOD { get; set; }
        public string ANEXO { get; set; }
        public string CEN_EDU { get; set; }
        public string ID_GRADO { get; set; }
        public string DSC_GRADO { get; set; }
        public string ID_NIVEL { get; set; }
        public string DSC_NIVEL { get; set; }
        public string ID_MODALIDAD { get; set; }
        public string ABR_MODALIDAD { get; set; }
        public string DSC_MODALIDAD { get; set; }
        public string DIRECTOR { get; set; }
    }
}
