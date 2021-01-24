namespace Minedu.Siagie.Identity.Dto.Security
{
    public class RenewTokenDto : TokenDto
    {
        public RenewTokenDto(string token, long renew): base(token, renew)
        {

        }
    }
}
