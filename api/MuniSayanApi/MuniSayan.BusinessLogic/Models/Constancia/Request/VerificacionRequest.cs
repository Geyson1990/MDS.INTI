using System.ComponentModel.DataAnnotations;

namespace MuniSayan.BusinessLogic.Models.Constancia
{
    public class VerificacionRequest
    {
        [Required]
        public string codigoVirtual { get; set; }

        [Required]
        public string tipoDocumento { get; set; }

        [Required]
        public string numeroDocumento { get; set; }

        [Required]
        public string captcha { get; set; }
    }
}
