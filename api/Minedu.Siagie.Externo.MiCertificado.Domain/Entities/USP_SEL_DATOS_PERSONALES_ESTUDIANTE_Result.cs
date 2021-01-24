using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_DATOS_PERSONALES_ESTUDIANTE_Result
    {
        public int ID_PERSONA { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string CODIGO_ESTUDIANTE { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
    }
}
