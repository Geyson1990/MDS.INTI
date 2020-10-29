using System;
using System.Collections.Generic;
using System.Text;

namespace MuniSayan.BusinessLogic.Models.Certificado
{
    public class CodigoSolicitudCertificadoRequest
    {
        public string codigoSolicitud { get; set; }
        public string codigoModular { get; set; }
        public string anexo { get; set; }
        public string estadoSolicitud { get; set; }
        public string estadoEstudiante { get; set; }
    }
}
