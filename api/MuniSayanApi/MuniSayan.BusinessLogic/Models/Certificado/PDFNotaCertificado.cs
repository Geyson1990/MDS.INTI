using System.Collections.Generic;

namespace MuniSayan.BusinessLogic.Models.Certificado
{
    public class PDFNotaCertificado
    {
        public string IdArea { get; set; }
        public string DscArea { get; set; }
        public string IdTipoArea { get; set; }
        public string DscTipoArea { get; set; }
        public List<PDFGradoNotaCertificado> GradoNotas { get; set; }
        public string Estado { get; set; }
        public int EsAreaSiagie { get; set; }
    }
}
