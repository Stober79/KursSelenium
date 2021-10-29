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
            jse = (IJavaScriptExecutor)driver;
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void AlertTest()
        {
            jse.ExecuteScript("alert('Cześć jestem alertem')");
            driver.SwitchTo().Alert().Accept();
        }
        [Test]
        public void ConfirmTest()
        {
            jse.ExecuteScript("confirm('Wybierz OK lub Anuluj')");
            driver.SwitchTo().Alert().Dismiss();
        }
        [Test]
        public void PromptTest()
        {
            jse.ExecuteScript("prompt('Wpisz cokolwiek')");
            driver.SwitchTo().Alert().SendKeys("coś tam");
            driver.SwitchTo().Alert().Accept();
        }
    }
}
