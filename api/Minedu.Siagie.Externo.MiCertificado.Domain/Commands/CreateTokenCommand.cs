using MediatR;
using Minedu.Core.General.Communication;

namespace Minedu.Siagie.Externo.MiCertificado.Domain.Commands
{
    public class CreateTokenCommand:  IRequest<StatusResponse<string>>
    {
        public string Client { get; set; }
        public CreateTokenCommand(string client)
        {
            Client = client;
        }


    }
}
