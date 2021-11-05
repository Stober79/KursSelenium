using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace KursSelenium.Tests.RozneDrivery
{
    class DifferentDrivers
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            //Chrome potrzebuje drivera
            //driver = new ChromeDriver();
            //Firefox nie potrzebuje drivera
            driver = new FirefoxDriver();
            ////Opera potrzebuje drivera
            //driver = new OperaDriver();
            ////IE- potrzebuje drivera Protected Mode
            //driver = new InternetExplorerDriver();
            //EdgeOptions options = new EdgeOptions();
            //options.UseChromium = true;
            //driver = new EdgeDriver(options);
            Start.Setup(driver);
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void Test()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
        }
    }
}
