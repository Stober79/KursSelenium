using FakestorePageObjects;
using Helpers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.Config;
using SeleniumTests.TestData;
using System;

namespace SeleniumTests
{
    class BaseTests
    {
        protected IWebDriver driver; 
        protected Configuration config;
        protected Data testData;

        [OneTimeSetUp]
        public void Setup()

        {
            ConfigSetup();
            TestDataSetUp();
        }
        [SetUp]
        public void DriverSetup()
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
        private void ConfigSetup()
        {
            config = new Configuration();
            IConfiguration configurationFile = new ConfigurationBuilder().AddJsonFile(@"Config\configuration.json").Build();
            configurationFile.Bind(config);
        }
        private void TestDataSetUp()
        {
            testData = new Data();
            IConfiguration testDataFile = new ConfigurationBuilder().AddJsonFile(@"TestData\testData.json").Build();
            testDataFile.Bind(testData);
        }
       
    }
}

            
        

