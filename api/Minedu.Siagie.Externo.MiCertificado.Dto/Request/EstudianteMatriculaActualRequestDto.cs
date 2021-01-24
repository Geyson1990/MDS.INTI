using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteMatriculaActualRequestDto
    {
        [NivelValidation(esObligatorio = false)]
        public string IdNivel { get; set; }
        
        [Required(ErrorMessage = "Ingrese el campo TipoDocumento.")]
        [RegularExpression("^[1-2]{1}", ErrorMessage = "El campo TipoDocumento no es válido.")]
        public string TipoDocumento { get; set; }
        
        [Required(ErrorMessage = "Ingrese el campo NumeroDocumento.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo número de documento no es válido.")]
        public string NumeroDocumento { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo código modular no es válido.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "El campo código modular debe ser de 7 caracteres")]
        public string CodigoModular { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo anexo no es válido.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El campo anexo debe ser de 1 caracter")]
        public string Anexo { get; set; }
    }
}
