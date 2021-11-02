using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Tests.Other
{
    class GridExpamle
    {
        RemoteWebDriver driver;

        [SetUp]
        public void Setup()
        {
            DriverOptions options = new ChromeOptions();
            driver = new RemoteWebDriver(options);//w tym miejscu wpisujemy adres klineta lub używamy Options jeśli client jest lokalnie
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
