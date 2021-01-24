using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Domain.Commands;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Domain.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.CommandHandlers
{
    public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, StatusResponse<string>>
    {
        private readonly TokenConfigurations _tokenConfigs;
        private readonly SignConfigurations _signConfigurations;
        private readonly EncrypConfigurations _encrypConfigurations;
        public CreateTokenCommandHandler(IOptions<TokenConfigurations> tokenConfigs)
        {
            _tokenConfigs = tokenConfigs.Value;
            _signConfigurations = new SignConfigurations(_tokenConfigs.SecretKey);
            _encrypConfigurations = new EncrypConfigurations(_tokenConfigs.Encryptkey);
            //var signingConfigurations = new SignConfigurations(tokenOptions.Secret);
        }

        public async Task<StatusResponse<string>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new StatusResponse<string>();

            var info = new TokenInfo
            {
                Id = request.Client
            };

            if (!Valid(request.Client))
            {
                response.Validations.Add(new MessageStatusResponse("Campo Client no válido", "00"));                
                //response.Message = "Token no generado";
                //response.Code = "-1";
                response.Success = false;
                return response;
            }

            var accessToken = BuildAccessToken(info);
            response.Data = accessToken;
            response.Code = "1001";
            response.Success = true;
            return response;
        }
        private bool Valid(string id)
        {
            var isValid = false;
            var client = _tokenConfigs.ClientMiCertificadoToken;
            if (id.Equals(client.ToString()))
                return true;
            return isValid;

        }
        private string BuildAccessToken(TokenInfo info)
        {

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigs.Issuer,
                Audience = _tokenConfigs.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_tokenConfigs.NotBeforeMinutes),
                Expires = DateTime.Now.AddSeconds(_tokenConfigs.AccessTokenExpiration),
                SigningCredentials = _signConfigurations.SignCredentials,
                //EncryptingCredentials = _encrypConfigurations.EncryptingCredentials,
                Subject = new ClaimsIdentity(GetClaims(info))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
            var encryptedJwt = tokenHandler.WriteToken(securityToken);
            return encryptedJwt;
        }
        private IEnumerable<Claim> GetClaims(TokenInfo info)
        {
            var claims = new List<Claim>
            {
                new Claim("TokenInfo", JsonConvert.SerializeObject(info)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, info.Id)
            };
            return claims;
        }
    }
}
