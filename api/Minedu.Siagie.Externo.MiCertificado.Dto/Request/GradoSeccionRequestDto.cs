using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class GradoSeccionRequestDto
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

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo IdGrado no es válido.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El campo IdGrado debe ser de 2 caracteres.")]
        public string IdGrado { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "EL campo IdSeccion no es válido.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El campo IdSeccion debe ser de 2 caracteres.")]
        public string IdSeccion { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo IdFase no es válido.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El campo IdFase debe ser de 2 caracteres.")]
        public string IdFase { get; set; }

    }
}
