﻿using System;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Entities
{
    public class USP_SEL_AREAS_POR_GRADO_CONFIGURACION_Result
	{
		public string ID_DISENIO { get; set; }
		public Int16 ID_ANIO { get; set; }
		public string ID_NIVEL { get; set; }
		public string ID_GRADO { get; set; }

		public string ID_AREA { get; set; }
		public string DSC_AREA { get; set; }
		public bool ACT_NOMBRE_AREA { get; set; }
		public bool ADD_COMPETENCIAS { get; set; }
		public Int16 NUM_COMP_ADICIONALES { get; set; }

	}
}