using System;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteDatosPersonalesResponseDto
    {
        public int IdPersona { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string CodigoEstudiante { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
