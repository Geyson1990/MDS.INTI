﻿namespace Minedu.Siagie.Externo.MiCertificado.Dto
{
    public class EstudianteNotasResponseDto
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
        public int EsConducta { get; set; }
        public string NotaFinalArea { get; set; }
        public int EsAreaSiagie { get; set; } = 1;
    }
}