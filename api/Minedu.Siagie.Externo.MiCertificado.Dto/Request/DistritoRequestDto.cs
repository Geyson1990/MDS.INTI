using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class DistritoRequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo CodigoProvincia")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo código de provincia no es válido.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "El campo código de provincia debe ser de 4 caracteres.")]        
        public string CodigoProvincia { get; set; }
    }
}
