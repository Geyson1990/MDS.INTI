using System;

namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteAnioGradoSeccionResponseDto
    {
        public string CodigoModular { get; set; }
        public string Anexo { get; set; }
        public int IdAnio { get; set; }
        public string IdNivel { get; set; }
        public string IdGrado { get; set; }
        public string IdSeccion { get; set; }
        public int IdPersona { get; set; }
        //public string TrasladoExterno { get; set; }

        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string NombreCompleto { get; set; }
        //public DateTime FechaNacimiento { get; set; }
        public string CodigoEstudiante { get; set; }

       // public int IdMatricula { get; set; }
        //public int EstadoMatricula { get; set; }
        //public DateTime FechaMatricula { get; set; }
        public string NumeroDocumento { get; set; }
        public int ValidadoReniec { get; set; }
        //public string IdModulo { get; set; }
        //public string DescNivel { get; set; }
        public string IdModalidad { get; set; }
        //public string DescModalidad { get; set; }
        //public string AbreModalidad { get; set; }
        //public string DescGrado { get; set; }
        //public bool Generar { get; set; }

    }
}
