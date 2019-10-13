using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NewsAppNative.Core.Models;
using NewsAppNative.Core.PlatformSpecific;
using Newtonsoft.Json;
using NewsAppNative.Core.Extensions;


namespace NewsAppNative.iOS.PlatformSpecific
{
    public class IosHttpClient : IHttpClient
    {
        public async Task<T> GetAsync<T>(string url) where T : class
        {
            try
            {
                var ct = new CancellationTokenSource();
                ct.CancelAfter(10000);

                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync(url, ct.Token);

                ct.Token.ThrowIfCancellationRequested();

                if (response?.StatusCode != null)
                {
                    var statusCode = (int)response?.StatusCode;

                    if (!response.IsSuccessStatusCode)
                    {
                        response?.Dispose();
                        response = null;

                        if (statusCode == 404)
                            throw new UrlNotFoundException();

                        throw new TaskCanceledException();
                    }
                }


                using (var sr = response?.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
                {
                    var serializer = new JsonSerializer
                    {
                        DateParseHandling = DateParseHandling.None,
                        DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    };
                    using (var sr1 = new StreamReader(sr))
                    using (var jsonTextReader = new JsonTextReader(sr1))
                    {
                        return serializer.Deserialize<T>(jsonTextReader);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is System.OperationCanceledException || ex is TaskCanceledException || ex is TaskCanceledException)
                    throw new TimeoutException("Превышен интервал ожидания для запроса");

                throw new NetworkErrorException();
            }
        }

        public async Task<HttpStatusCode> Get(string url)
        {
            try
            {
                var ct = new CancellationTokenSource();
                ct.CancelAfter(5000);

                var httpClient = new HttpClient();
                //httpClient.AddBindingHeaders();

                var response = await httpClient.GetAsync(url, ct.Token);
                ct.Token.ThrowIfCancellationRequested();

                if (response?.StatusCode != null)
                {
                    var statusCode = (int)response?.StatusCode;

                    if (!response.IsSuccessStatusCode)
                    {
                        response?.Dispose();
                        response = null;

                        if (statusCode == 404)
                            throw new UrlNotFoundException();

                        throw new TaskCanceledException();
                    }
                }

                return response.StatusCode;
            }
            catch (Exception ex)
            {
                if (ex is UrlNotFoundException)
                    throw new UrlNotFoundException("Url not found");

                if (ex is System.OperationCanceledException || ex is TaskCanceledException || ex is TaskCanceledException)
                    throw new TimeoutException("Превышен интервал ожидания для запроса");

                if (ex is HttpRequestException he && he.InnerException != null && he.InnerException is WebException we)
                    throw new NetworkErrorException(we.GetLastMessage());

                throw new NetworkErrorException("Нет связи");
            }
        }
    }
}
