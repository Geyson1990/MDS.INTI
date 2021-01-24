using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class InstitucionEducativaPorDreUgelRequestDto
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo CodigoDre no válido.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Campo CodigoDre debe ser de 4 caracteres.")]
        public string CodigoDre { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo CodigoUgel no es válido.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Campo CodigoUgel debe ser de 6 caracteres.")]
        public string CodigoUgel { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo CodigoModular no es válido.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "El campo CodigoModular debe ser de 7 caracteres")]
        public string CodigoModular { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo Anexo no es válido.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El campo Anexo debe ser de 1 caracter")]
        public string Anexo { get; set; }

        [RegularExpression("^[a-zñA-Z0-9-ÑÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïñòóôõöùúûüýÿ'. ]*$", ErrorMessage = "El campo CenEdu no es válido.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El campo CenEdu es de mínimo 3 y máximo 50 caracteres")]
        public string CenEdu { get; set; }

        [NivelValidation(esObligatorio = true)]
        public string IdNivel { get; set; }

        [IntegerValidation(esObligatorio = true)]
        public int? PageNumber { get; set; }

        [IntegerValidation(esObligatorio = true)]
        public int? RowsPerPage { get; set; }

    }
}
