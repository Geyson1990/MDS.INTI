using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class ApoderadoEstudianteRequestDto
    {
        //[Required(ErrorMessage = "Ingrese el campo IdPersonaApoderado.")]
        //[Range(1, int.MaxValue, ErrorMessage = "Campo IdPersonaApoderado no válido.")]
        [IntegerValidation(esObligatorio =true)]
        public int? IdPersonaApoderado { get; set; }

        //[Required(ErrorMessage = "Ingrese el campo IdPersonaEstudiante.")]
        //[Range(1, int.MaxValue, ErrorMessage = "Campo IdPersonaEstudiante no válido.")]
        [IntegerValidation(esObligatorio = true)]
        public int? IdPersonaEstudiante { get; set; }

    }
}
