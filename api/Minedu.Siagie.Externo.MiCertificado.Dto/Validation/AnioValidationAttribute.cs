using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class AnioValidationAttribute : ValidationAttribute
    {
        public bool esObligatorio { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value == null)
            {
                if (esObligatorio)
                    return new ValidationResult("Ingrese el campo "+context.DisplayName.ToString()+".");

                value = 0;
            }
                
            
            int valor  = (int)value;
            if (valor < 1900 || valor > DateTime.Now.Year)
            {
                if (valor != 0 && esObligatorio == false)
                    return new ValidationResult("El campo " + context.DisplayName.ToString() + " no es válido.");

                if (valor == 0 && esObligatorio == true)
                    return new ValidationResult("El campo " + context.DisplayName.ToString() + " no es válido.");

                if (valor < 1900 || valor > DateTime.Now.Year)
                    return new ValidationResult("El campo " + context.DisplayName.ToString() + " no es válido.");
            }

            if (valor > int.MaxValue)
                return new ValidationResult("El campo " + context.DisplayName.ToString() + " no es válido.");

            return ValidationResult.Success;                        
        }
    }
}
