using consoleApp.Services.Interface;
using ConsoleApp.AppSettings;
using ConsoleApp.Extensions;
using ConsoleApp.Models;
using ConsoleApp.Security;
using Microsoft.Extensions.Options;

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
        private readonly EndpointOptions options;

        public RunService(IHttpClientHelper<Photos> httpClient, IOptions<EndpointOptions> options,
            IFileEncrypt fileEncrypt, IHtml2PdfService html2Pdf, IApplicationConfigService applicationConfig,
            ITempFileService fileService, IOptions<ApplicationDataOptions> applicationDataOptions
            )
        {
            this.httpClient = httpClient;
            this.fileEncrypt = fileEncrypt;
            _html2Pdf = html2Pdf;
            _applicationConfig = applicationConfig;
            _fileService = fileService;
            _applicationDataOptions = applicationDataOptions;
            this.options = options.Value;
        }
        public async Task Run()
        {
            //Enumerable.Range(1, 1).ToList().ForEach(async x =>
            //{
            //    var photos = await httpClient.GetSingleItemRequest("https://jsonplaceholder.typicode.com/photos", default);
            //});

            var photos = await httpClient.GetMultipleItemsRequest(options.ApiEndPoint!);

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

            Parallel.ForEach(photos, async photo =>
            {
                var fileName = await _fileService.CreateTempFileAsync();
                //var fileName = await _fileService.CreateAsync($"{Guid.NewGuid()}","sis");
                await _fileService.UpdateAsync(fileName, photo.HtmlString());
                await _html2Pdf.ConvertToPdf(fileName, $"{Guid.NewGuid()}.pdf");
                await _fileService.DeleteAsync(fileName);
            });

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
