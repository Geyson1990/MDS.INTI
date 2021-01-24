using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class UbigeoRequestDto
    {
        //[Required(ErrorMessage = "Ingrese el código del departamento")]
        public string codDepartamento { get; set; }
        //[Required(ErrorMessage = "Ingrese el código de la provincia")]    
        public string codProvincia { get; set; }
        //[Required(ErrorMessage = "Ingrese el código del ubigeo")]
        public string codUbigeo { get; set; } 
    }
}
