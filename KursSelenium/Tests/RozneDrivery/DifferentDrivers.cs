using KursSelenium.Element;
using KursSelenium.StartSetup;
using Microsoft.Edge.SeleniumTools;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using System;
using System.Collections.Generic;
using System.Text;

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
