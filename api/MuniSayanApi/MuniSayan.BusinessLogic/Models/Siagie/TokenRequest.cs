﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MuniSayan.BusinessLogic.Models.Siagie
{
    public class TokenRequest
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Ticket { get; set; }
        public string ServerName { get; set; }
        public string RolId { get; set; }
    }
}
