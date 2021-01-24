using System;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteMatriculaConcluidaResponseDto
    {
        public int IdMatricula { get; set; }
        public string CodigoModular { get; set; }
        public string Anexo { get; set; }
        public int IdPersonaEstudiante { get; set; }
        //public int IdPersona { get; set; }
        public string CodigoEstudiante { get; set; }
        public string IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Ubigeo { get; set; }
        public int IdAnio { get; set; }
        public string IdModalidad { get; set; }
        public string IdNivel { get; set; }
        public string IdGrado { get; set; }
        public string DscGrado { get; set; }
        public int ConVida { get; set; }
    }
}
