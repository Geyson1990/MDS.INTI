namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteNotas2020ResponseDto
    {
        public int IdAnio { get; set; }
        public string CodigoModular { get; set; }
        public string Anexo { get; set; }
        public string IdNivel { get; set; }
        public string DscNivel { get; set; }
        public string IdGrado { get; set; }
        public string DscGrado { get; set; }
        public string IdTipoArea { get; set; }
        public string DscTipoArea { get; set; }
        public string IdArea { get; set; }
        public string DscArea { get; set; }
        public string IdAsignatura { get; set; }
        public string DscAsignatura { get; set; }
        public int Esconduta { get; set; }
        public string NotaFinalArea { get; set; }
        public int EsAreaSiagie { get; set; } = 1;
    }
}
