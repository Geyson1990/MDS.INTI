using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Dto.Response
{
    public class TokenEbaDto
    {
        public string dni { get; set; }
        public string nombre { get; set; }
        public string apePaterno { get; set; }
        public string apeMaterno { get; set; }
        public string codModular { get; set; }
        public string anexo { get; set; }
        public bool esDirector { get; set; }
        public bool esTutor { get; set; }
        public bool esDocente { get; set; }
        public int anioId { get; set; }
        public string codigoModular { get; set; }
        public int dotacion { get; set; }
        public int rol { get; set; }
        public string nombreIE { get; set; }
        public string idNivel { get; set; }
        public string descNivel { get; set; }
        public string idRol { get; set; }
        public string idSistema { get; set; }
        public string idTipoSede { get; set; }
        public string descTipoSede { get; set; }
        public string loginUsuario { get; set; }
        public string ugel { get; set; }
        public string dre { get; set; }
        public string idGestion { get; set; }
        public string descGestion { get; set; }
        public string idModalidad { get; set; }
        public string idProgramaEBA { get; set; }
    }
}
