using System;
using System.Collections.Generic;
using System.Text;

namespace MuniSayan.BusinessLogic.Models.Certificado
{
    public class CertificadoRequest
    {
        public SolicitanteRequest2 solicitante { get; set; }

        public EstudianteRequest2 estudiante { get; set; }

        public InformacionRequest2 solicitud { get; set; }
    }
}