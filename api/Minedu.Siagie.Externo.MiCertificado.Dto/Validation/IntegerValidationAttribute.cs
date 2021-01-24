using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class IntegerValidationAttribute : ValidationAttribute
    {
        public bool esObligatorio { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            try
            {
                if (value == null)
                {
                    if (esObligatorio)
                        return new ValidationResult("Ingrese el campo " + context.DisplayName.ToString() + ".");
                }

                int valor = (int)value;
                if (context.DisplayName.ToUpper().Contains("IDPERSONA"))
                {
                    if (valor < 1)
                        return new ValidationResult("El campo " + context.DisplayName.ToString() + " no es válido. Valor mínimo 1.");
                }
                else
                { 
                    if (valor < 0)
                        return new ValidationResult("El campo " + context.DisplayName.ToString() + " no es válido. Valor mínimo 0.");
                }
                
                if (valor > int.MaxValue)
                    return new ValidationResult("El campo " + context.DisplayName.ToString() + " no es válido. Valor máximo 2147483647.");
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                return new ValidationResult("El campo " + context.DisplayName.ToString() + " no es válido. Ingrese un valor entero válido.");
            }                                  
        }
    }
}
