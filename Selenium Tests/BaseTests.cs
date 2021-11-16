using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SeleniumTests.Element;
using SeleniumTests.StartSetup;
using System;

namespace SeleniumTests
{
    class BaseTests
    {
        protected RemoteWebDriver driver;
        DriverOptions options;
        string baseUrl = "https://fakestore.testelka.pl/";

        [SetUp]
        public void Setup()
        {
            options = new ChromeOptions();
            //options = new FirefoxOptions();
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);
            Start.Setup(driver);
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver).Click();
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}
