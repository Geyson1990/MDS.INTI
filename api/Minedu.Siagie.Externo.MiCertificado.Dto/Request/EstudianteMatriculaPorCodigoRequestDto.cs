using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteMatriculaPorCodigoRequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo NumeroDocumento")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo NúmeroDocumento no es válido.")]
        public string NumeroDocumento { get; set; }

        [NivelValidation(esObligatorio =true)]
        public string IdNivel { get; set; }

        [Required(ErrorMessage = "Ingrese el campo TipoDocumento.")]
        [RegularExpression("^[1-2]{1}", ErrorMessage = "El campo TipoDocumento no es válido.")]
        [MaxLength(1, ErrorMessage = "El campo TipoDocumento debe ser de 1 caracter")]
        public string TipoDocumento { get; set; }

    }
}
