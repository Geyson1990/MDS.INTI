using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteNotas2020RequestDto
    {
        [IntegerValidation(esObligatorio = true)]
        public int? IdPersona { get; set; }

        [NivelValidation(esObligatorio = true)]
        public string IdNivel { get; set; }
    }
}
