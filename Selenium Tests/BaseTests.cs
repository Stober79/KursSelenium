using FakestorePageObjects;
using Helpers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.Config;
using System;

namespace SeleniumTests
{
    class BaseTests
    {
        protected IWebDriver driver; 
        protected Configuration config;

        [OneTimeSetUp]
        public void SetupConfig()

        {
            config = new Configuration();
            IConfiguration configurationFile = new ConfigurationBuilder().AddJsonFile(@"Config\configuration.json").Build();
            configurationFile.Bind(config);
        }
        [SetUp]
        public void Setup()
        {
            driver = new DriverFactory().Create(config.Browser, config.IsRemote, config.RemoteAddress);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.Manage().Window.Maximize();
            MainPage mainPage = new MainPage(driver, config.BaseUrl);
            mainPage.GoTo().DismissNoticeSection.Dismiss();
        }


        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
       
    }
}

            
        

