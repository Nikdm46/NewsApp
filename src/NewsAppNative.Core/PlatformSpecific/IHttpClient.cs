using System.Threading.Tasks;

namespace NewsAppNative.Core.PlatformSpecific
{
    public interface IHttpClient
    {
        Task<T> GetAsync<T>(string url) where T : class;
    }
}
