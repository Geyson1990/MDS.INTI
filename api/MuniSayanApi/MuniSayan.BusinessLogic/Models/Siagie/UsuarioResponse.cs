﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MuniSayan.BusinessLogic.Models.Siagie
{
    public class UsuarioResponse
    {
        public string codModular { get; set; }
        public string anexo { get; set; }
        public string numeroDocumento { get; set; }
        public string idNivel { get; set; }
        public string dscNivel { get; set; }
        public string idRol { get; set; }

        public string nombreRol { get; set; }
        public string loginUsuario { get; set; }
        public string tipoSede { get; set; }
        public string nombreCompleto { get; set; }
        public string ugel { get; set; }
        public string nombreIE { get; set; }
        public string idModalidad { get; set; }
    }
}
