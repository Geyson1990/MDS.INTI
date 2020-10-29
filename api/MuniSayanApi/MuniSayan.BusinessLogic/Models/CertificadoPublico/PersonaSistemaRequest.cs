using System;
using System.Collections.Generic;
using System.Text;

namespace MuniSayan.BusinessLogic.Models.Certificado
{
    public class PersonaSistemaRequest
    {
        public string tipoDocumento { get; set; }
        public string nroDocumento { get; set; }

        public string idSistema { get; set; }
    }
}
