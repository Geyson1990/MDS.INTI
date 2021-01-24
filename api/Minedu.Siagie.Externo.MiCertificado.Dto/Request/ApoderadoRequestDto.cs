using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class ApoderadoRequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo TipoDocumento.")]
        [RegularExpression("^[2]{1}", ErrorMessage = "El campo TipoDocumento no es válido.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El campo TipoDocumento debe ser de 1 caracter.")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "Ingrese el campo NumeroDocumento.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo NumeroDocumento no es válido.")]
        public string NumeroDocumento { get; set; }

    }
}
