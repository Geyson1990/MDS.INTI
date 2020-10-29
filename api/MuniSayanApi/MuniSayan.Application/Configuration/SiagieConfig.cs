﻿using Microsoft.Extensions.Configuration;
using MuniSayan.Application.Contracts.Configuration;

namespace MuniSayan.Application.Configuration
{
    public class SiagieConfig : ISiagieConfig
    {
        private readonly IConfiguration _configuration;

        public SiagieConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int MaxTrys => int.Parse(_configuration.GetSection("Polly:MaxTrys").Value);
        public int SecondsToWait => int.Parse(_configuration.GetSection("Polly:TimeDelay").Value);
        public int CacheExpireInMinutes => int.Parse(_configuration.GetSection("Cache:CacheExpireInMinutes").Value);
        public string ServiceUrl => _configuration.GetSection("SiagieService:BaseUrl").Value;
    }
}