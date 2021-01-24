using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class ProvinciaRequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo CodigoDepartamento")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo CodigoDepartamento no es válido.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El campo CodigoDepartamento debe ser de 2 caracteres.")]
        public string CodigoDepartamento { get; set; }
    }
}
