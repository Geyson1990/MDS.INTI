using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class AnioInstitucionRequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo CodigoModular")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo CodigoModular no es válido.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "El campo CodigoModular debe ser de 7 caracteres")]
        public string CodigoModular { get; set; }

        [Required(ErrorMessage = "Ingrese el campo anexo")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo Anexo no es válido.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El campo anexo debe ser de 1 caracter")]
        public string Anexo { get; set; }

    }
}
