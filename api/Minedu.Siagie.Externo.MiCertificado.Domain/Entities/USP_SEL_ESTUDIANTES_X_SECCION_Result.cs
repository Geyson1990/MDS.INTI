using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_ESTUDIANTES_X_SECCION_Result
    {
        public string COD_MOD { get; set; }
        public string ANEXO { get; set; }
        public int ID_ANIO { get; set; }
        public string ID_NIVEL { get; set; }
        public string ID_GRADO { get; set; }
        public string ID_SECCION { get; set; }
        public int ID_PERSONA { get; set; }
        //public string TRASLADO_EXTERNO { get; set; }

        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        //public DateTime FECHA_NACIMIENTO { get; set; }
        public string CODIGO_ESTUDIANTE { get; set; }

        //public int ID_MATRICULA { get; set; }
        //public int ESTADO_MATRICULA { get; set; }
        //public DateTime FECHA_MATRICULA { get; set; }
        public string DNI { get; set; }
        public int VALIDADO_RENIEC { get; set; }
        //public string ID_MODULO { get; set; }
        //public string DSC_NIVEL { get; set; }
        public string ID_MODALIDAD { get; set; }
        //public string DSC_MODALIDAD { get; set; }
        //public string ABR_MODALIDAD { get; set; }
        //public string DSC_GRADO { get; set; }
    }
}
