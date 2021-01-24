using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class UgelRequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo CodigoDre")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo CodigoDre no es válido.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Campo CodigoDre debe ser de 4 caracteres.")]
        public string CodigoDre { get; set; }
    }
}
