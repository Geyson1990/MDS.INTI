using System;
using System.Collections.Generic;
using System.Text;

namespace MuniSayan.BusinessLogic.Models.Certificado
{
    public class PersonaModalidadNivelRequest
    {
        public string tipoDocumento { get; set; }

        public string nroDocumento { get; set; }

        public string idModalidad { get; set; }

        public string idNivel { get; set; }

    }
}
