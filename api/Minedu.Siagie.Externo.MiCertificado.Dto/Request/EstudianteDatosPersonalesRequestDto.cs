using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteDatosPersonalesRequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo CodigoEstudiante")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo CodigoEstudiante no es válido.")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "El campo CodigoEstudiante debe ser de 14 caracteres.")]
        public string CodigoEstudiante { get; set; }
    }
}
