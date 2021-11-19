using FakestorePageObjects;
using Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace SeleniumTests
{
    class BaseTests
    {
        protected IWebDriver driver;
        private readonly bool isRemote = false;
        private readonly Uri remoteAddress = new Uri("http://localhost:4444/wd/hub");
        public string baseUrl = "https://fakestore.testelka.pl/";
        protected string browser = "chrome";

        [SetUp]
        public void Setup()
        {      
            driver = new DriverFactory().Create(browser, isRemote, remoteAddress);
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
                
            //local Firefox new FirefoxDriver
            //local Firefox new ChromeDriver
            
        

