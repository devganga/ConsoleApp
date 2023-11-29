using consoleApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class HttpClientParser : HttpClient
    {
        //private readonly HttpClient _client;
        //private readonly HttpClient _noCacheClient;

        //public  MyProperty { get; set; }
        //private readonly string _baseUrl;
        public HttpClientParser() : base()
        {
            //_client = new HttpMessageHandler();
           // _noCacheClient = new HttpClient();
        }
        //public HttpClientParser(HttpClientHandler httpClientHandler) : base(httpClientHandler)
        //{

        //}
        //public HttpClientParser(HttpMessageHandler handler) : this(handler, true)
        //{

        //}
        //public HttpClientParser(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
        //{

        //}

        public async new Task<string> GetStringAsync(string? requestUri, CancellationToken cancellationToken)
        {
            base.BaseAddress = new Uri(requestUri!);
            //var result = default(T);
            return await base.GetStringAsync(requestUri, cancellationToken).ConfigureAwait(false);
        }
        public async new Task<byte[]> GetByteArrayAsync(string? requestUri, CancellationToken cancellationToken)
        {
            return await base.GetByteArrayAsync(requestUri, cancellationToken).ConfigureAwait(false);
        }
        public async new Task<Stream> GetStreamAsync(string? requestUri, CancellationToken cancellationToken)
        {
            return await base.GetStreamAsync(requestUri, cancellationToken).ConfigureAwait(false);
        }

        public Task DeleteRequest(string apiUrl, CancellationToken token = default)
        {
            //await DeleteAsync(apiUrl, token).Wait();
            throw new NotImplementedException();
        }


    }
}
