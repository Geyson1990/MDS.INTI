using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteMatriculaConcluidaRequestDto
    {
        [NivelValidation(esObligatorio = false)]
        public string IdNivel { get; set; }
        
        [Required(ErrorMessage = "Ingrese el campo TipoDocumento.")]
        [RegularExpression("^[1-2]{1}", ErrorMessage = "El campo TipoDocumento no es válido.")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "Ingrese el campo NumeroDocumento.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo NumeroDocumento no es válido.")]
        [MaxLength(14, ErrorMessage = "El campo número de documento es de máximo 14 caracteres")]
        [MinLength(8, ErrorMessage = " El campo número de documento es de mínimo 8 caracteres")]
        public string NumeroDocumento { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo Anexo no es válido.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El campo Anexo debe ser de 1 caracter")]
        public string Anexo { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo CodigoModular no es válido.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "El campo CodigoModular debe ser de 7 caracteres")]
        public string CodigoModular { get; set; }

        //[RegularExpression("^[0-9]*$", ErrorMessage = "EL campo IdModalidad no es válido.")]
        //[StringLength(2, MinimumLength = 2, ErrorMessage = "El campo IdModalidad debe ser de 2 caracteres.")]
        //public string IdModalidad { get; set; }
    }
}
