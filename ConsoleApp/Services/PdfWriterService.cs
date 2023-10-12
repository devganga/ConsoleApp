using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using System.Diagnostics;
using consoleApp.AppSettings;
using consoleApp.Services.Interface;

namespace consoleApp.Services
{
    public class PdfWriterService : IRunService
    {
        private readonly IWebDriver _webDriver;
        private readonly AdrenalinOptions _options;
        private readonly SampleOptions _options1;
        public PdfWriterService(IWebDriver webDriver, IOptions<AdrenalinOptions> options, IOptions<SampleOptions> options1)
        {
            _options = options.Value;
            _options1 = options1.Value;
            _webDriver = webDriver;
        }
        public async Task Run()
        {
            //_webDriver.Url= _options.Url;
            string fileName = $"1-{Guid.NewGuid()}";
            File.AppendAllText($"{fileName}.html", $"<h1>{fileName}</h1>");

            var arg = $"{fileName}.html {fileName}.pdf";
            var startInfo = new ProcessStartInfo(@"wkhtmltopdf.exe")
            {
                UseShellExecute = false,
                Arguments = arg,
            };
            var cmd = new Process { StartInfo = startInfo };
            cmd.Start();
            cmd.WaitForExit();
            await Task.CompletedTask;
        }

    }
}
