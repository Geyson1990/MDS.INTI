﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MuniSayan.Application.Contracts.Security
{
    public interface IEncryptionServerSecurity
    {
        string Encrypt(string input);
        T Decrypt<T>(string input, T porDefecto);
    }
}