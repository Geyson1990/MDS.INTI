using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class NivelValidationAttribute : ValidationAttribute
    {
        public bool esObligatorio { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value == null)
            {
                if (esObligatorio)
                    return new ValidationResult("Ingrese el campo " + context.DisplayName.ToString() + ".");
                else
                    return ValidationResult.Success;
            }
            if (value.ToString().ToUpper().Equals("A1"))
            {
                return new ValidationResult("El nivel A1 no es contemplado para el proyecto MiCertificado.");
            }

            if (value.ToString().ToUpper().Equals("E0"))
            {
                return new ValidationResult("El nivel E0 no es contemplado para el proyecto MiCertificado.");
            }

            var arrayNiveles = new string[] { "A0", "A2", "A3", "A5", "B0", "C0", "D0", "D1","D2","E1","E2","F0","G0"};
            var valor = value.ToString().ToUpper();
            if (Array.IndexOf(arrayNiveles,valor)==-1)
                return new ValidationResult("El campo " + context.DisplayName.ToString() + " no es válido.");

            if (value.ToString().Length != 2)
            {
                return new ValidationResult("El campo " + context.DisplayName.ToString() + " debe de ser de 2 caracteres.");
            }

            return ValidationResult.Success;                        
        }
    }
}
