using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

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
        [Test]
        public void WaitForAlert()
        {
            jse.ExecuteScript("setTimeout(function(){alert('Wpisz cokolwiek');},300);");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d =>
            {
                try { return driver.SwitchTo().Alert(); }
                catch (NoAlertPresentException) { return null; }
            });
            driver.SwitchTo().Alert().Accept();
        }
    }
}
