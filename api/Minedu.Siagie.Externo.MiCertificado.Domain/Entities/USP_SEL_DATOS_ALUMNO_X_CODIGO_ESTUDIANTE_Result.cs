using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_DATOS_ALUMNO_X_CODIGO_ESTUDIANTE_Result
    {
        public int ID_PERSONA { get; set; }

        public string CODIGO_ESTUDIANTE { get; set; }
        public string ID_TIPO_DOCUMENTO { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }

        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string FECHA_NACIMIENTO { get; set; }
        public string UBIGEO_NACIMIENTO_INEI { get; set; }

        public int ID_ANIO { get; set; }
        public string ID_MODALIDAD { get; set; }
        public string COD_MOD { get; set; }

        public string ID_NIVEL { get; set; }
        public string ID_GRADO { get; set; }
        public string DSC_GRADO { get; set; }
        public int ESTADO { get; set; }
    }
}
