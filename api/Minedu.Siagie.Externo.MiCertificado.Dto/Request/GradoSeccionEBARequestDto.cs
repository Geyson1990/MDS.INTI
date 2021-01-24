using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class GradoSeccionEBARequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo CodigoModular")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El ampo CodigoModular no es válido.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "El campo CodigoModular debe ser de 7 caracteres")]
        public string CodigoModular { get; set; }

        [Required(ErrorMessage = "Ingrese el campo Anexo.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo Anexo no es válido.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El campo Anexo debe ser de 1 caracter")]
        public string Anexo { get; set; }

        [AnioValidation(esObligatorio = true)]
        public int? IdAnio { get; set; }

        [NivelValidation(esObligatorio = true)]
        public string IdNivel { get; set; }

        [Required(ErrorMessage = "El IdFasePeriodoPromIE es obligatorio")]
        public int IdFasePeriodoPromIE { get; set; }

    }
}
