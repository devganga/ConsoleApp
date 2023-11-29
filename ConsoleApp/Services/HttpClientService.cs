using System.Text;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using ConsoleApp.AppSettings;

namespace ConsoleApp.Services
{
    public interface IHttpClientService
    {
        CookieContainer CookieJar { get; }
        HttpClient Client { get; }
        void AddCookie(string key, string value);
        Task<HttpResponseMessage> Get(string apiUrl, CancellationToken token = default);
        Task<byte[]> GetByteArrayAsync(string apiUrl, CancellationToken token = default);
        Task<HttpResponseMessage> Post(string apiUrl, object postObject, CancellationToken token = default);
        Task<HttpResponseMessage> Put(string apiUrl, object putObject, CancellationToken token = default);
        Task<HttpResponseMessage> Delete(string apiUrl, CancellationToken token = default);
    }
    public class HttpClientService : IHttpClientService
    {
        private readonly CookieContainer _cookieJar;
        private readonly HttpClient _client;
        private readonly Uri _baseAddress;
        private readonly EndpointOptions _options;

        public CookieContainer CookieJar { get { return _cookieJar; } }
        public HttpClient Client { get { return _client; } }

        //private readonly HttpClient _httpClient;
        //private readonly CookieContainer cookies = new CookieContainer();
        public HttpClientService(IOptions<EndpointOptions> options)
        {
            _options = options.Value;
            _cookieJar = new CookieContainer();
            _baseAddress = new(_options.BaseUrl!);

            //CookieJar.Add(uri, new Cookie("71e940f10481cd4f86700a492f012d7d=360bc58f212cda6c3510f0a3c7fedab4; Path=/; HttpOnly;", "true")); // Adding a Cookie
            //Cookies.Add(baseAddress, new Cookie("JSESSIONID", $"0000JyHM9cBHIEpaPRpQ7d847--:2c87500e-5e7a-4897-81d8-1741ec3dd217")); // Adding a Cookie
            //Cookies.Add(baseAddress, new Cookie("71e940f10481cd4f86700a492f012d7d", "360bc58f212cda6c3510f0a3c7fedab4")); // Adding a Cookie
            //Cookies.Add(baseAddress, new Cookie("BIGipServerw.vidagateway-prod.volvocars.biz.new.80.pool", "1305030922.20480.0000")); // Adding a Cookie
            //
            _client = new(new HttpClientHandler() { CookieContainer = _cookieJar }) { BaseAddress = _baseAddress };

        }
        public void AddCookie(string key, string value)
        {
            _cookieJar.Add(_baseAddress, new Cookie(key, value));
        }

        public async Task<HttpResponseMessage> Delete(string apiUrl, CancellationToken token = default)
        {
            return await _client.DeleteAsync(apiUrl, token).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> Get(string apiUrl, CancellationToken token = default)
        {
            return await _client.GetAsync(apiUrl, token).ConfigureAwait(false);
        }
        public async Task<byte[]> GetByteArrayAsync(string apiUrl, CancellationToken token = default)
        {
            return await _client.GetByteArrayAsync(apiUrl, token).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> Post(string apiUrl, object postObject, CancellationToken token = default)
        {
            var json = JsonConvert.SerializeObject(postObject);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PostAsync(apiUrl, data, token).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> Put(string apiUrl, object putObject, CancellationToken token = default)
        {
            var json = JsonConvert.SerializeObject(putObject);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PutAsync(apiUrl, data, token).ConfigureAwait(false);
        }
    }
}
