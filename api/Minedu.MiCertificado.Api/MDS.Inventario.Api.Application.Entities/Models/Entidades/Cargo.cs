using MDS.Inventario.Api.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDS.Inventario.Api.Application.Entities.Models
{
    public class Cargo
    {
        public int IdCargo { get; set; }
        public string DescripcionCargo { get; set; }

        //Campos auditoría//
        public int EstadoActivo { get; set; }
        public bool EstadoRegistro { get; set; }
        public string Usuario { get; set; }
    }
}
