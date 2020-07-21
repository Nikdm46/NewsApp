using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Logging;
using NewsAppNative.Core.DTO;
using NewsAppNative.Core.Extensions;
using Newtonsoft.Json;

namespace NewsAppNative.Core.Rest.Implementations
{
    public class RestClient : IRestClient
    {
        private readonly IMvxLog _mvxLog;
        public RestClient(IMvxLog mvxLog)
        {
            _mvxLog = mvxLog;
        }

        public async Task<ResponseDTO<TResult>> MakeRequestAsync<TResult>(string url, HttpMethod method, object data = null) where TResult : class
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage { RequestUri = new Uri(url), Method = method })
                {
                    var result = new ResponseDTO<TResult>()
                    {
                        IsSuccess = true,
                        Error = "",
                    };

                    if (method != HttpMethod.Get)
                    {
                        var json = JsonConvert.SerializeObject(data);
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    }

                    var response = new HttpResponseMessage();
                    try
                    {
                        var ct = new CancellationTokenSource();
                        ct.CancelAfter(10000);
                        response = await httpClient.SendAsync(request, ct.Token).ConfigureAwait(false);                        

                        if (response?.StatusCode != null)
                        {
                            result.StatusCode = response.StatusCode;

                            if (!response.IsSuccessStatusCode)
                            {
                                result.IsSuccess = false;
                                return result;
                            }
                        }

                        var stringSerialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        result.Content = JsonConvert.DeserializeObject<List<TResult>>(stringSerialized);

                        return result;
                    }
                    catch (Exception ex)
                    {
                        result.Error = ex.GetLastMessage();

                        _mvxLog.ErrorException("Request failed", ex);

                        return result;
                    }
                }
            }
        }
    }
}
