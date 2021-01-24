using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class GradoNivelRequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo IdModalidad")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo IdModalidad no es válido.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El campo IdModalidad debe ser de 2 caracteres.")]
        public string IdModalidad { get; set; }

        [NivelValidation(esObligatorio = true)]
        public string IdNivel { get; set; } 
    }
}
