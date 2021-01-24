using Newtonsoft.Json;

namespace Minedu.Siagie.Externo.SIPE.Api.Infrastructure
{
    public static class SecurityParam
    {
        public static T To<T>(string _SecurityParam)
        {
            return JsonConvert.DeserializeObject<T>(_SecurityParam);
        }
    }
}
