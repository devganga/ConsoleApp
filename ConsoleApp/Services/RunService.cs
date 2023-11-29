using consoleApp.Services.Interface;
using ConsoleApp.AppSettings;
using ConsoleApp.Extensions;
using ConsoleApp.Helpers;
using ConsoleApp.Models;
using ConsoleApp.Security;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;
using System.Linq;
using OpenQA.Selenium;
using ConsoleApp.Driver;
using OpenQA.Selenium.DevTools;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.Services
{
    public class RunService : IRunService
    {
        private readonly IHttpClientHelper<Photos> httpClient;
        private readonly IFileEncrypt fileEncrypt;
        private readonly IHtml2PdfService _html2Pdf;
        private readonly IApplicationConfigService _applicationConfig;
        private readonly ITempFileService _fileService;
        private readonly IOptions<ApplicationDataOptions> _applicationDataOptions;
        private readonly IFileSecureService _fileSecure;
        private readonly IHttpClientService _httpClientService;
        //private readonly ILogger<RunService> _logger;

        //private readonly IWebDriver _webDriver;
        private readonly EndpointOptions options;

        //private readonly CookieHelper _cookieHelper = CookieHelper.Instance;

        public RunService(IHttpClientHelper<Photos> httpClient, IOptions<EndpointOptions> options,
            IFileEncrypt fileEncrypt, IHtml2PdfService html2Pdf, IApplicationConfigService applicationConfig,
            ITempFileService fileService, IOptions<ApplicationDataOptions> applicationDataOptions,
            IFileSecureService fileSecure, IHttpClientService httpClientService
            //IDriverFixture webDriver
            //,ILogger<RunService> logger
            )
        {
            this.httpClient = httpClient;
            this.fileEncrypt = fileEncrypt;
            _html2Pdf = html2Pdf;
            _applicationConfig = applicationConfig;
            _fileService = fileService;
            _applicationDataOptions = applicationDataOptions;
            _fileSecure = fileSecure;
            _httpClientService = httpClientService;
            //_logger = logger;
            //_webDriver = webDriver.Driver;
            this.options = options.Value;
        }
        private async Task Init()
        {
            //var folderName = _applicationDataOptions.Value.FolderName;

            var appPath = _applicationDataOptions.Value.GetApplicationDataPath();

            var tempPath = _applicationDataOptions.Value.GetApplicationTempPath();
            Directory.CreateDirectory(appPath);
            Directory.CreateDirectory(tempPath);

            await Task.CompletedTask;
        }
        public async Task proxyTest()
        {
            //_logger.LogInformation("test");
            List<Dictionary<string, string>> proxy_list = new List<Dictionary<string, string>> {
                new Dictionary<string, string> {
                    {"protocol", "http"},
                    {"host", "41.207.187.178"},
                    {"port", "80"},
                },
                //...
                //new Dictionary<string, string> {
                //    {"protocol", "http"},
                //    {"host", "132.129.121.148"},
                //    {"port", "8080"},
                //},
                //new Dictionary<string, string> {
                //    {"protocol", "http"},
                //    {"host", "154.129.98.156"},
                //    {"port", "8080"},
                //},
                //new Dictionary<string, string> {
                //    {"protocol", "http"},
                //    {"host", "211.129.132.150"},
                //    {"port", "8080"},
                //},
                //new Dictionary<string, string> {
                //    {"protocol", "http"},
                //    {"host", "164.129.114.111"},
                //    {"port", "8080"},
                //}
            };

            // Generate a random number between 0 and 4 (inclusive)
            //Random rnd = new Random();
            //var randomIndex = rnd.Next(proxy_list.Count);
            //var proxy = new WebProxy
            //{
            //    Address = new Uri($"{proxy_list[randomIndex]["protocol"]}://{proxy_list[randomIndex]["host"]}:{proxy_list[randomIndex]["port"]}"),
            //    BypassProxyOnLocal = false,
            //    UseDefaultCredentials = false,
            //};


            // Now create a client handler that uses the proxy
            var httpClientHandler = new HttpClientHandler
            {
                //Proxy = proxy,
            };

            // Disable SSL verification
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // Finally, create the HTTP client object
            var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
            //var client = new HttpClient();
            var result = await client.GetStringAsync("https://api.ipify.org/");
            Console.WriteLine(result);

        }
        public async Task Run()
        {
            //CookieHelper _cookieHelper = CookieHelper.Instance;
            //for (int i = 0; i < 50; i++)
            //{

            //    Console.WriteLine($"{i}-{_cookieHelper.IsValueCreated}");
            //    //Console.WriteLine("");
            //}
            //_webDriver.Url = "https://api.publicapis.org/entries";
            //_webDriver.Manage().Network.
            //_webDriver.Close();
            var _httpClient = new HttpRequesterService();
            _httpClient.cookieContainer = new CookieContainer();
            //var devTools = (IDevTools)driver;

            //// create Dev Tools session
            //var session = devTools.GetDevToolsSession();
            //// add response received handler to dev tools session
            //session.Network.ResponseReceived += ResponseReceivedHandler;
            //await session.Network.Enable(new EnableCommandSettings());

            //_httpClient.cookieContainer = (CookieContainer)_webDriver.Manage().Cookies;
            //var httpResponseMessage = await _httpClient.GetAsync("https://api.publicapis.org/entries");

            //var response1 =  await httpResponseMessage.Content.ReadAsStringAsync();
            //var lstEntry = response1.JsonDeserialize<EndPointEntries>();

            //await Init();
            //await proxyTest();
            //Enumerable.Range(1, 1).ToList().ForEach(async x =>
            //{
            //    var photos = await httpClient.GetSingleItemRequest("https://jsonplaceholder.typicode.com/photos", default);
            //});

            //var photos = await httpClient.GetMultipleItemsRequest(options.ApiEndPoint!);
            //_httpClientService.Serialize()
            _httpClientService.AddCookie("self", "king");
            var response = await _httpClientService.Get(options.ApiEndPoint!);

            //var pp = new Photos { Id = 2, ThumbnailUrl = "sdfasd", AlbumId = 3, Title = "sdafasd", Url = "sdafasdf" };
            //var ss = pp.JsonSerialize();
            //var mm = ss.JsonDeserialize<Photos>();

            //var photos = new IEnumerable<Photos>();
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith(x =>
               {
                   var result = x.Result!.JsonDeserialize<IEnumerable<Photos>>();

                   //var dd = result.JsonSerialize();
                   //return x.Result;
                   //var res = x.Result;
                   //var photos = JsonSerializer.Deserialize<List<Photos>>(res);
                   //var photos = x.Result!.JsonDeserialize<List<Photos>>();
                   //var photos = JsonSerializer.Deserialize<IEnumerable<Photos>>(x.Result); // x.Result!.JsonDeserialize<IEnumerable<Photos>>();
                   //var photos = JsonConvert.DeserializeObject<IEnumerable<Photos>>(x.Result);

               });

                //var photos = JsonSerializer.Deserialize<List<Photos>>(result,);
                //var photos = res!.Deserialize<IEnumerable<Photos>>();

                //Parallel.ForEach(photos!.Take(50), async photo =>
                //{
                //    //var fileName = await _fileService.CreateTempFileAsync();
                //    var fileName = await _fileService.CreateAsync($"{photo.Id}", "sis");
                //    _fileSecure.Encrypt(Path.GetTempFileName(), photo.HtmlString());
                //    await _fileService.UpdateAsync(fileName, photo.HtmlString());
                //    await _html2Pdf.ConvertToPdf(fileName, $"{photo.Id}.pdf");
                //    await _fileService.DeleteAsync(fileName);
                //});

            }


            //var TempFolder = Path.Combine(Path.GetTempPath(), "iRobot");

            //var foldr = _applicationConfig.ApplicationDataOptions.FolderName;

            //Directory.CreateDirectory(TempFolder);
            //foreach (var photo in photos)
            //{
            //    var id = Guid.NewGuid();
            //    var outputFileName = Path.Combine(TempFolder, $"{id}.htm");

            //    File.WriteAllText(outputFileName, photo.ToString());

            //    await _html2Pdf.ConvertToPdf(outputFileName, $"{id}.pdf");
            //}

            //Parallel.ForEach(photos, async photo =>
            //{
            //    //var fileName = await _fileService.CreateTempFileAsync();
            //    var fileName = await _fileService.CreateAsync($"{photo.Id}", "sis");
            //    _fileSecure.Encrypt(Path.GetTempFileName(), photo.HtmlString());
            //    await _fileService.UpdateAsync(fileName, photo.HtmlString());
            //    await _html2Pdf.ConvertToPdf(fileName, $"{photo.Id}.pdf");
            //    //await _fileService.DeleteAsync(fileName);
            //});

            //httpClient.GetSingleItemRequest("sdf").

            //var now = DateTime.Now.AddDays(1).InHuman();



            //var ss = photos.Serialize();
            //var pp = ss.Deserialize<IEnumerable<Photos>>();

            //var s = 999.InCurrencyWords("buck");

            //var res = pp!.SelectMany(x=> x.Title!);

            //fileEncrypt.Encrypt(ss);

            //CookieContainer cookies = new CookieContainer();
            //HttpClientHandler handler = new HttpClientHandler();
            //handler.CookieContainer = cookies;

            //HttpClient client = new HttpClient(handler);
            ////HttpResponseMessage response = client.GetAsync("http://flipkart.com").Result;

            //Uri uri = new Uri("https://www.flipkart.com");
            //IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
            //foreach (Cookie cookie in responseCookies)
            //    Console.WriteLine(cookie.Name + ": " + cookie.Value);

            //Console.ReadLine();


            await Task.CompletedTask;
        }
    }
}
