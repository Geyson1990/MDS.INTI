using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteModalidadNivelModularRequestDto
    {
        //[Required(ErrorMessage = "Ingrese el campo IdPersona")]
        //[Range(1, int.MaxValue, ErrorMessage = "Campo IdPersona no válido.")]
        [IntegerValidation(esObligatorio = true)]
        public int? IdPersona { get; set; }

        [Required(ErrorMessage = "Ingrese el campo IdModalidad")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo IdModalidad no es válido.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El campo IdModalidad debe ser de 2 caracteres.")]
        public string IdModalidad { get; set; }

        [NivelValidation(esObligatorio = true)]
        public string IdNivel { get; set; }

        [Required(ErrorMessage = "Ingrese el campo CodigoModular")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo CodigoModular no es válido.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "El campo CodigoModular debe ser de 7 caracteres")]
        public string CodigoModular { get; set; }

        [Required(ErrorMessage = "Ingrese el campo Anexo")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo Anexo no es válido.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El campo Anexo debe ser de 1 caracter")]
        public string Anexo { get; set; }
    }
}
