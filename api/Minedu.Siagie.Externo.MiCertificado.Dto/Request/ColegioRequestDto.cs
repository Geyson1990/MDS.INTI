using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class ColegioRequestDto
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo código de Departamento no es válido.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El campo código de Departamento debe ser de 2 caracteres")]
        public string Departamento { get; set; }
        
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo código de Provincia no es válido.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "El campo código de Provincia debe ser de 4 caracteres")]
        public string Provincia { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo código de Ubigeo no es válido.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "El campo código de Ubigeo debe ser de 6 caracteres")]        
        public string Ubigeo { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo código UGEL no es válido.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Campo CodigoUgel debe ser de 6 caracteres.")]        
        public string CodigoUgel { get; set; }

        [RegularExpression("^[a-zñA-Z0-9-ÑÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïñòóôõöùúûüýÿ'. ]*$", ErrorMessage = "El campo CenEdu no es válido.")]        
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El campo CenEdu es de mínimo 3 y máximo 50 caracteres")]        
        public string CenEdu { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo Código modular no es válido.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "El campo código modular debe ser de 7 caracteres")]        
        public string CodigoModular { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo Anexo no es válido.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El campo anexo debe ser de 1 caracter")]        
        public string Anexo { get; set; }

        [RegularExpression("^[12]*$", ErrorMessage = "El campo Estado no es válido.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El campo Estado debe ser de 1 caracter")]        
        public string Estado { get; set; } 

        [IntegerValidation(esObligatorio = true)]
        public int? PageSize { get; set; } = 10;

        [IntegerValidation(esObligatorio = true)]
        public int? Page { get; set; } = 0;

    }
}
