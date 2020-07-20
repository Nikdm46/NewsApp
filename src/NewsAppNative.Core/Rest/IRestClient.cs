using System.Net.Http;
using System.Threading.Tasks;
using NewsAppNative.Core.DTO;

namespace NewsAppNative.Core.Rest
{
    public interface IRestClient
    {
        Task<ResponseDTO<TResult>> MakeRequestAsync<TResult>(string url, HttpMethod method, object data = null) where TResult : class;
    }
}
