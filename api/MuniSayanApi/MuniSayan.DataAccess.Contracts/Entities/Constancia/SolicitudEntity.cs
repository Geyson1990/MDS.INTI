﻿using System;

namespace MuniSayan.DataAccess.Contracts.Entities.Constancia
{
    public class SolicitudEntity
    {
        public int ID_SOLICITUD { get; set; }

        public int ID_ESTUDIANTE { get; set; }
        public int ID_SOLICITANTE { get; set; }

        public int ID_MOTIVO { get; set; }
        public string MOTIVO_OTROS { get; set; }

        public string ID_MODALIDAD { get; set; }
        public string ABR_MODALIDAD { get; set; }
        public string DSC_MODALIDAD { get; set; }
        public string ID_NIVEL { get; set; }
        public string DSC_NIVEL { get; set; }
        public string ID_GRADO { get; set; }
        public string DSC_GRADO { get; set; }
        public string ESTADO_SOLICITUD { get; set; }

        public string CODIGO_VIRTUAL { get; set; }

        public bool ACTIVO { get; set; }
        public string ESTADO { get; set; }
        public DateTime FECHA_CREACION { get; set; }
        public DateTime FECHA_ACTUALIZACION { get; set; }
    }
}
