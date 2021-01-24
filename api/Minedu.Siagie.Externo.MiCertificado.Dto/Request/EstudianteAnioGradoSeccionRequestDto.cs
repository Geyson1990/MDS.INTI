using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteAnioGradoSeccionRequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo CodigoModular.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo código modular no es válido.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "El campo código modular debe ser de 7 caracteres")]
        public string CodigoModular { get; set; }

        [Required(ErrorMessage = "Ingrese el campo anexo")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo anexo no es válido.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El campo anexo debe ser de 1 caracter")]
        public string Anexo { get; set; }

        //[AnioValidation(esObligatorio = false)]
        public int? IdAnio { get; set; }

        [NivelValidation(esObligatorio =true)]
        public string IdNivel { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo IdGrado no es válido.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El campo IdGrado debe ser de 2 caracteres")]
        public string IdGrado { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo IdSeccion no es válido.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El campo IdSeccion debe ser de 2 caracteres")]
        public string IdSeccion { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo número de documento no es válido.")]
        [MaxLength(14, ErrorMessage = "El campo número de documento es de máximo 14 caracteres")]
        [MinLength(8, ErrorMessage = "El campo número de documento es de mínimo 8 caracteres")]
        public string NumeroDocumento { get; set; }
        
        [RegularExpression("^[a-zñA-Z0-9-ÑÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïñòóôõöùúûüýÿ'. ]*$", ErrorMessage = "El campo NombresEstudiante no es válido.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El campo NombresEstudiante es de mínimo 3 y máximo 50 caracteres")]
        public string NombresEstudiante { get; set; }



    }
}
