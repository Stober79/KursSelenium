using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace KursSelenium.Tests.Other
{
    class GridExpamle
    {
        RemoteWebDriver driver;
        DriverOptions options;

        [SetUp]
        public void Setup()
        {
            options = new ChromeOptions();
            options.PlatformName = "WINDOWS";
            //driver = new RemoteWebDriver(options);//w tym miejscu wpisujemy adres klineta lub używamy Options jeśli client jest lokalnie
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);
            Start.Setup(driver);
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
