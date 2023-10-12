using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consoleApp.Services.Interface;
using System.Net;

namespace consoleApp.Services
{
    public class HttpClientHelper<T> : IHttpClientHelper<T>
    {
        private readonly CookieContainer cookies = new CookieContainer();
        //HttpClientHandler handler = new HttpClientHandler();
        //handler.CookieContainer = cookies;

        private readonly HttpClient Client;       
        public HttpClientHelper()
        {
            Uri uri = new Uri("http://localhost");
            //CookieJar.Add(uri, new Cookie("71e940f10481cd4f86700a492f012d7d=360bc58f212cda6c3510f0a3c7fedab4; Path=/; HttpOnly;", "true")); // Adding a Cookie
            cookies.Add(uri, new Cookie("JSESSIONID", $"0000JyHM9cBHIEpaPRpQ7d847--:2c87500e-5e7a-4897-81d8-1741ec3dd217")); // Adding a Cookie
            cookies.Add(uri, new Cookie("71e940f10481cd4f86700a492f012d7d", "360bc58f212cda6c3510f0a3c7fedab4")); // Adding a Cookie
            cookies.Add(uri, new Cookie("BIGipServerw.vidagateway-prod.volvocars.biz.new.80.pool", "1305030922.20480.0000")); // Adding a Cookie

            Client = new(new HttpClientHandler() { CookieContainer = cookies });
        }

        /// <summary>
        /// For getting a bytes from a web api uaing GET
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        ///     api get method, e.g. "products/1" to get a product with an id of 1</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The item requested</returns>
        public async Task<byte[]> GetByteArrayItemRequest(string apiUrl, CancellationToken token = default)
        {
            return await Client.GetByteArrayAsync(apiUrl, token).ConfigureAwait(false);
        }

        /// <summary>
        /// For getting a single item from a web api uaing GET
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        ///     api get method, e.g. "products/1" to get a product with an id of 1</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The item requested</returns>
        public async Task<T> GetSingleItemRequest(string apiUrl, CancellationToken token = default)
        {
            var result = default(T);
            var response = await Client.GetAsync(apiUrl, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    if (typeof(T).Namespace != "System")
                    {
                        result = JsonConvert.DeserializeObject<T>(x.Result);
                    }
                    else result = (T)Convert.ChangeType(x.Result, typeof(T));
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
            return result!;
        }

        /// <summary>
        /// For getting multiple (or all) items from a web api using GET
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        ///     api get method, e.g. "products?page=1" to get page 1 of the products</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The items requested</returns>
        public async Task<T[]> GetMultipleItemsRequest(string apiUrl, CancellationToken token = default)
        {
            T[]? result = null;
            var response = await Client.GetAsync(apiUrl, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith((Task<string> x) =>
                {
                    result = JsonConvert.DeserializeObject<T[]>(x.Result);
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
            return result!;
        }

        /// <summary>
        /// For creating a new item over a web api using POST
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        ///     api post method, e.g. "products" to add products</param>
        /// <param name="postObject">The object to be created</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The item created</returns>
        public async Task<T> PostRequest(string apiUrl, T postObject, CancellationToken token = default)
        {
            var json = JsonConvert.SerializeObject(postObject);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            T? result = default;
            var response = await Client.PostAsync(apiUrl, data, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith((Task<string> x) =>
                {
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
            return result!;
        }

        /// <summary>
        /// For updating an existing item over a web api using PUT
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        ///     api put method, e.g. "products/3" to update product with id of 3</param>
        /// <param name="putObject">The object to be edited</param>
        /// <param name="cancellationToken"></param>
        public async Task PutRequest(string apiUrl, T putObject, CancellationToken token = default)
        {
            var json = JsonConvert.SerializeObject(putObject);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(apiUrl, data, token).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }

        /// <summary>
        /// For deleting an existing item over a web api using DELETE
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        ///     api delete method, e.g. "products/3" to delete product with id of 3</param>
        /// <param name="cancellationToken"></param>
        public async Task DeleteRequest(string apiUrl, CancellationToken token = default)
        {
            var response = await Client.DeleteAsync(apiUrl, token).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }
    }

}
