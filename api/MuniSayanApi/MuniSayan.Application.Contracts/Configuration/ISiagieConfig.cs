
namespace MuniSayan.Application.Contracts.Configuration
{
    public interface ISiagieConfig
    {
        int MaxTrys { get; }
        int SecondsToWait { get; }
        string ServiceUrl { get; }
        int CacheExpireInMinutes { get; }
    }
}
