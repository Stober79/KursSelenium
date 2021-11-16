using FakestorePageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace SeleniumTests
{
    class BaseTests
    {
        protected RemoteWebDriver driver;
        DriverOptions options;
        public string baseUrl = "https://fakestore.testelka.pl/";

        [SetUp]
        public void Setup()
        {
            options = new ChromeOptions();
            //options = new FirefoxOptions();
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.Manage().Window.Maximize();
            MainPage mainPage = new MainPage(driver);
            mainPage.GoTo().DismissNoticeSection.Dismiss();
        }


        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}
