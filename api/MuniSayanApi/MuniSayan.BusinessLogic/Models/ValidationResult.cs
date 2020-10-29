﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MuniSayan.BusinessLogic.Models
{
    public class ValidationResult
    {
        public bool result { get; set; }

        public ReniecPersona persona { get; set; }

        public bool esPadreMadre { get; set; }

        public string error { get; set; }
    }
}
