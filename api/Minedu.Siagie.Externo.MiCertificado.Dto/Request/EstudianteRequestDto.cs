using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteRequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo TipoDocumento")]
        [RegularExpression("^[1-2]{1}", ErrorMessage = "El campo TipoDocumento no es válido.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El campo TipoDocumento debe ser de 1 caracter")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "Ingrese el campo NumeroDocumento.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo NumeroDocumento no es válido.")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "Ingrese campo IdSistema.")]
        [RegularExpression("^[1-2]*$", ErrorMessage = "El campo IdSistema no es válido.")]
        [MaxLength(1, ErrorMessage = "El campo IdSistema debe ser de 1 caracter.")]
        public string IdSistema { get; set; }  
    }
}
