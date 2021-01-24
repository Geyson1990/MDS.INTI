using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteObservacionesRequestDto
    {
        [IntegerValidation(esObligatorio = true)]
        public int? IdPersona { get; set; }

        //[Required(ErrorMessage = "Ingrese el campo IdModalidad.")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "El campo IdModalidad no es válido.")]
        //[StringLength(2, MinimumLength = 2, ErrorMessage = "El campo IdModalidad debe ser de 2 caracteres.")]
        //public string IdModalidad { get; set; }

        [NivelValidation(esObligatorio =true)]
        public string IdNivel { get; set; }

        [Required(ErrorMessage = "Ingrese el campo IdSistema.")]
        [RegularExpression("^[1-2]*$", ErrorMessage = "El campo IdSistema no es válido.")]
        [MaxLength(1, ErrorMessage = "El campo IdSistema debe ser de 1 caracter.")]
        public string IdSistema { get; set; }
    }
}
