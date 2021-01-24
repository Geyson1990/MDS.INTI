using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class InstitucionEducativaRequestDto
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo código UGEL no es válido.")]
        [StringLength(6, MinimumLength = 4, ErrorMessage = "Campo código UGEL es de 4 caracteres.")]
        public string codUgel { get; set; }
        
        //[MaxLength(2, ErrorMessage = "El campo IdNivel es de máximo 2 caracteres")]
        //[RegularExpression("^[A-Z 0-9]*$", ErrorMessage = "Campo IdNivel no válido.")]
        [NivelValidation(esObligatorio = false)]
        public string idNivel { get; set; }
    }
}
