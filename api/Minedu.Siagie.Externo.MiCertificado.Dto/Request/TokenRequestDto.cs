using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto.Request
{
    public class TokenRequestDto
    {
        [Required(ErrorMessage = "Ingrese el campo Client")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La longitud del campo Client está comprendido entre 1 y 100 caracteres")]
        [RegularExpression("([A-Z0-9]+)", ErrorMessage = "El campo Client solo acepta letras mayúsculas o números")]
        public string Client { get; set; }
    }
}