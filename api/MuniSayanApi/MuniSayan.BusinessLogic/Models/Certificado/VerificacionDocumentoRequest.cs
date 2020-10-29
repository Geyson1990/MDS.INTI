using System.ComponentModel.DataAnnotations;

namespace MuniSayan.BusinessLogic.Models.Certificado
{
    public class VerificacionDocumentoRequest
    {
        [Required]
        public string codigoVirtual { get; set; }

        [Required]
        public string tipoDocumento { get; set; }

        [Required]
        public string numeroDocumento { get; set; }

        [Required]
        public string codigoModular { get; set; }

        [Required]
        public string anexo { get; set; }
    }
}
