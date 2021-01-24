using MediatR;
using Minedu.Core.General.Communication;
using Minedu.Siagie.Externo.MiCertificado.Domain.Commands;
using Minedu.Siagie.Externo.MiCertificado.Dto.Request;
using Minedu.Siagie.Externo.MiCertificado.Dto.Response;
using Minedu.Siagie.Identity.Application.Contract;
using System;
using System.Threading.Tasks;

namespace Minedu.Siagie.Identity.Application.Services
{
    public class JsonWebTokenService : IJsonWebTokenService
    {
        private readonly IMediator _mediator;

        public JsonWebTokenService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<StatusResponse<string>> Create(TokenRequestDto dto)
        {
            var response = new StatusResponse<string>();

                var createCommand = new CreateTokenCommand
                (
                     dto.Client
                    );
                var cltToken = new System.Threading.CancellationToken();
  
                return await _mediator.Send(createCommand, cltToken);
            
        }

        public async Task<string> Read(TokenDecrypDto dto)
        {
        //    var decrypCommand = new DecrypTokenCommand
        //    (
        //         dto.Token
        //        );
        //    var cltToken = new System.Threading.CancellationToken();
        //    var commandResult = await _mediator.Send(decrypCommand, cltToken);

        //    return commandResult;
        return "hoa";
        }

        public Task<TokenResponseDto> Renew(string key, string userEmail)
        {
            throw new NotImplementedException();
        }

        public void Revoke(string key)
        {
            throw new NotImplementedException();
        }

    }
}
