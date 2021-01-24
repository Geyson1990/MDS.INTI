namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteMatriculaPorCodigoResponseDto
    {
        public string IdPersona { get; set; }

        public string CodigoEstudiante { get; set; }
        public string IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }

        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Nombres { get; set; }
        public string FechaNacimiento { get; set; }
        public string UbigeoDomicilio { get; set; }
        public int UltimoAnio { get; set; }
        public string IdModalidad { get; set; }
        public string CodigoModular { get; set; }
        public string IdNivel { get; set; }
        public string IdGrado { get; set; }
        public string DscGrado { get; set; }
        //public int Estado { get; set; }
    }
}
