using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Tests.Advance.AdvenceAlerts
{
    class Alerts
    {
        IWebDriver driver;
        IJavaScriptExecutor jse;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl(Url.FakestoreFuerteventuraSotavento());
            Button.InfoList(driver).Click();
            jse = (IJavaScriptExecutor)driver;
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void Test()
        {

        }
    }
}
