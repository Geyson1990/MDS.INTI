using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteAnioEstudiosRequestDto
    {
        [IntegerValidation(esObligatorio = true)]
        public int? IdPersona { get; set; }

        [AnioValidation (esObligatorio = true)]
        public int? IdAnio { get; set; }
    }
}
