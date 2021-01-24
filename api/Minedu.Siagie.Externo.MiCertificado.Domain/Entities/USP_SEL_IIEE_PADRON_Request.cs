using System;
using System.Collections.Generic;
using System.Text;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_IIEE_PADRON_Request
    {
        public string DEPARTAMENTO { get; set; }
        public string PROVINCIA { get; set; }
        public string UBIGEO { get; set; }
        public string CEN_EDU { get; set; }
        public string COD_MOD { get; set; }
        public string ANEXO { get; set; }
        public string COD_UGEL { get; set; }
        public string ESTADO { get; set; }
        public int PAGE_SIZE { get; set; }
        public int PAGE { get; set; } 

    }
}
