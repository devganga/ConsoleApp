//using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ConsoleApp.Services
{
    public class HttpRequesterService
    {

        public CookieContainer? cookieContainer { get; set; }

        public async Task<HttpResponseMessage> GetAsync(string apiUrl, CancellationToken token = default)
        {
            var _cookieContainer = cookieContainer ?? new CookieContainer();

            using (var handler = new HttpClientHandler() { CookieContainer = _cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = new Uri(apiUrl) })
            {
                //cookieContainer.Add(baseAddress, new Cookie("CookieName", "cookie_value"));
                var result = await client.GetAsync(apiUrl, token).ConfigureAwait(false);
                return result.EnsureSuccessStatusCode();
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string apiUrl, object postObject, CancellationToken token = default)
        {
            var json = JsonSerializer.Serialize(postObject, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var _cookieContainer = cookieContainer ?? new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = _cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = new Uri(apiUrl) })
            {
                //var content = new FormUrlEncodedContent(new[]
                //{
                //    new KeyValuePair<string, string>("foo", "bar"),
                //    new KeyValuePair<string, string>("baz", "bazinga"),
                //});
                //cookieContainer.Add(baseAddress, new Cookie("CookieName", "cookie_value"));
                var result = await client.PostAsync(apiUrl, content, token).ConfigureAwait(false);
                return result.EnsureSuccessStatusCode();
            }
        }

        public async Task<HttpResponseMessage> PutAsync(string apiUrl, object putObject, CancellationToken token = default)
        {
            var json = JsonSerializer.Serialize(putObject, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var _cookieContainer = cookieContainer ?? new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = _cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = new Uri(apiUrl) })
            {
                //var content = new FormUrlEncodedContent(new[]
                //{
                //    new KeyValuePair<string, string>("foo", "bar"),
                //    new KeyValuePair<string, string>("baz", "bazinga"),
                //});
                //cookieContainer.Add(baseAddress, new Cookie("CookieName", "cookie_value"));
                var result = await client.PutAsync(apiUrl, content, token).ConfigureAwait(false);
                return result.EnsureSuccessStatusCode();
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string apiUrl, CancellationToken token = default)
        {
            var _cookieContainer = cookieContainer ?? new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = _cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = new Uri(apiUrl) })
            {
                //var content = new FormUrlEncodedContent(new[]
                //{
                //    new KeyValuePair<string, string>("foo", "bar"),
                //    new KeyValuePair<string, string>("baz", "bazinga"),
                //});
                //cookieContainer.Add(baseAddress, new Cookie("CookieName", "cookie_value"));
                var result = await client.DeleteAsync(apiUrl, token).ConfigureAwait(false);
                return result.EnsureSuccessStatusCode();
            }
        }
    }
}
