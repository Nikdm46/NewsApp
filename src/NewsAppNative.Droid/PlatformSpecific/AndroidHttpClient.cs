using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NewsAppNative.Core.Extensions;
using Java.Net;
using NewsAppNative.Core.Models;
using NewsAppNative.Core.PlatformSpecific;
using System.Net;

namespace NewsAppNative.Droid.PlatformSpecific
{
    public class AndroidHttpClient : IHttpClient
    {

        public async Task<T> GetAsync<T>(string url) where T : class
        {
            try
            {
                var ct = new CancellationTokenSource();
                ct.CancelAfter(10000);

                var httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync(url, ct.Token);

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
                if (ex is ConnectException)
                    throw new NetworkErrorException("Нет связи");

                if (ex is UrlNotFoundException)
                    throw new UrlNotFoundException("Неверный адрес сервера");

                if (ex is OperationCanceledException || ex is TaskCanceledException || ex is TaskCanceledException)
                    throw new TimeoutException("Превышен интервал ожидания для запроса");

                if (ex is HttpRequestException he && he.InnerException != null && he.InnerException is WebException we)
                    throw new NetworkErrorException(we.GetLastMessage());

                throw new NetworkErrorException(ex.GetLastMessage());
            }
        }
    }
}
