﻿using System.Collections.Generic;

namespace MuniSayan.BusinessLogic.Models.Constancia
{
    public class EstudianteConstancia
    {
        public SolicitudModel solicitud { get; set; }

        public EstudianteModel estudiante { get; set; }

        public List<GradoModel> grados { get; set; }

        public List<NotaModel> notas { get; set; }

        public List<ObservacionModel> observaciones { get; set; }
    }
}
