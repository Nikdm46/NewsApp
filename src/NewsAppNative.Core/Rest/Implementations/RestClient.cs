using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Base;
using MvvmCross.Logging;
using NewsAppNative.Core.DTO;
using NewsAppNative.Core.Extensions;

namespace NewsAppNative.Core.Rest.Implementations
{
    public class RestClient : IRestClient
    {
        private readonly IMvxJsonConverter _jsonConverter;
        private readonly IMvxLog _mvxLog;
        public RestClient(IMvxJsonConverter jsonConverter, IMvxLog mvxLog)
        {
            _jsonConverter = jsonConverter;
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
                        Status = ResponseStatus.Success,
                        Error = "",
                    };

                    if (method != HttpMethod.Get)
                    {
                        var json = _jsonConverter.SerializeObject(data);
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    }

                    HttpResponseMessage response = new HttpResponseMessage();
                    try
                    {
                        var ct = new CancellationTokenSource();
                        ct.CancelAfter(10000);
                        response = await httpClient.SendAsync(request, ct.Token).ConfigureAwait(false);                        

                        if (response?.StatusCode != null)
                        {
                            result.StatusCode = (int)response?.StatusCode;

                            if (!response.IsSuccessStatusCode)
                            {
                                result.Status = ResponseStatus.Error;
                                return result;
                            }
                        }                        

                        var stringSerialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        result.Content = _jsonConverter.DeserializeObject<List<TResult>>(stringSerialized);

                        return result;
                    }
                    catch (Exception ex)
                    {
                        result.Error = ex.GetLastMessage();

                        _mvxLog.Error(result.Error);

                        return result;
                    }
                }
            }
        }
    }
}
