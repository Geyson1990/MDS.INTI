using Minedu.Comun.Helper;
using MDS.Inventario.Api.Application.Contracts.Security;
using MDS.Inventario.Api.Application.Contracts.Services;
using Mappers = MDS.Inventario.Api.Application.Mappers;
using MDS.Inventario.Api.DataAccess.Contracts.UnitOfWork;
using Models = MDS.Inventario.Api.Application.Entities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using MDS.Inventario.Api.Application.Utils;
using Newtonsoft.Json;
using MDS.Inventario.Api.Application.Security;

namespace MDS.Inventario.Api.Application.Services
{

    public class CargoService : ICargoService
    {
        private readonly IUnitOfWork _unitOfWork;//Base datos
        private readonly IEncryptionServerSecurity _encryptionServerSecurity;//Mètodos de encriptaciòn
        private readonly IConfiguration _configuration;//Appsettings configuraciòn
        public readonly IHttpContextAccessor _httpContextAccessor;

        public CargoService(IUnitOfWork unitOfWork,
            IEncryptionServerSecurity encryptionServerSecurity,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _unitOfWork = unitOfWork;
            _encryptionServerSecurity = encryptionServerSecurity;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<StatusResponse> Insertar(Models.Helpers.ParametroHelper objetoEncriptado)
        {
            var response = new StatusResponse();
            var request = JsonConvert.DeserializeObject<Models.CargoExtends>(ReactEncryptationSecurity.Decrypt<string>(objetoEncriptado.parametros, ""));
            
            try
            {
                var consulta = await _unitOfWork.Insertar(Mappers.CargoMapper.Map(request));
                if(consulta > 0)
                {
                    response.Data = consulta;
                    response.Success = true;
                    response.Messages.Add("Operaciòn exitosa.");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Messages.Add("Ocurrió un error al momneto de insertar el registro.");
            }
            return response;
        }
    }
}
