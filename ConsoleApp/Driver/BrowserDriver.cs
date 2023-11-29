using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ConsoleApp.Driver
{
    public interface IBrowserDriver
    {
        IWebDriver GetChromeDriver();
        IWebDriver GetFirefoxDriver();
        IWebDriver GetEdgeDriver();
        IWebDriver GetSafariDriver();

    }
    public class BrowserDriver : IBrowserDriver
    {
        public IWebDriver GetChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            return new ChromeDriver();
        }

        public IWebDriver GetFirefoxDriver()
        {

            new DriverManager().SetUpDriver(new FirefoxConfig());
            return new FirefoxDriver();
        }

        public IWebDriver GetEdgeDriver()
        {

            new DriverManager().SetUpDriver(new EdgeConfig());
            return new EdgeDriver();
        }

        public IWebDriver GetSafariDriver()
        {
            //new DriverManager().SetUpDriver(new SafariConfig());
            return new SafariDriver();
        }

    }
    public enum BrowserType
    {
        Edge,
        Chrome,
        Firefox,
        Safari
    }
}