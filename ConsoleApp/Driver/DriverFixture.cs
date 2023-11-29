using ConsoleApp.AppSettings;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;

namespace ConsoleApp.Driver
{
    public interface IDriverFixture
    {
        IWebDriver Driver { get; }
    }
    public class DriverFixture : IDriverFixture
    {
        IWebDriver driver;
        private readonly BrowserOptions options;
        private readonly IBrowserDriver browserDriver;

        //DI is happening
        public DriverFixture(IOptions<BrowserOptions> options, IBrowserDriver browserDriver)
        {
            this.options = options.Value;
            this.browserDriver = browserDriver;
            driver = GetWebDriver();
        }
        public IWebDriver Driver => driver;
        private IWebDriver GetWebDriver()
        {
            return options.BrowserType switch
            {
                BrowserType.Chrome => browserDriver.GetChromeDriver(),
                BrowserType.Firefox => browserDriver.GetFirefoxDriver(),
                BrowserType.Edge => browserDriver.GetEdgeDriver(),
                BrowserType.Safari => browserDriver.GetSafariDriver(),
                _ => browserDriver.GetChromeDriver()
            };
        }
        public void Dispose()
        {
            driver.Quit();
        }
    }

}