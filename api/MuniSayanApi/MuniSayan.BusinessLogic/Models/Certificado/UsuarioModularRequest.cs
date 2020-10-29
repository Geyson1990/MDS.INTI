using MuniSayan.DataAccess.Contracts.Entities.Certificado;
using System.Collections.Generic;

namespace MuniSayan.BusinessLogic.Models.Certificado
{
    public class UsuarioModularRequest
    {
        public string usuarioLogin { get; set; }
        public string codigoModular { get; set; }
        public string anexo { get; set; }
        public string idRol { get; set; }
    }
}
