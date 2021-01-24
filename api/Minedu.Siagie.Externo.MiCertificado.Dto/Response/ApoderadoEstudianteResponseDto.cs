using System;
using System.ComponentModel.DataAnnotations;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class ApoderadoEstudianteResponseDto
    {
        public int IdPersonaApoderado { get; set; }
        public int IdPersonaEstudiante { get; set; }
        public int IdMatricula { get; set; }
    }
}
