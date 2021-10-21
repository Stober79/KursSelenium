using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Pierwsze_Kroki
{
    class TytuliZrodloStrony_Zadanie
    {
        IWebDriver driver;

        [SetUp]

        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = System.TimeSpan.FromSeconds(10);
        }

        [Test]

        public void TitleTest()
        {
            driver.Navigate().GoToUrl("https://wikipedia.es/");
            driver.Manage().Window.Maximize();
            string expectedTitle = "Wikipedia, la enciclopedia libre";
            Assert.AreEqual(expectedTitle, driver.Title, "Page title is not correct.");
        }

        [Test]

        public void PageSourceTest()
        {
            driver.Navigate().GoToUrl("https://wikipedia.es/");
            driver.Manage().Window.Maximize();
            string content = "lang=\"es\"";
            Assert.IsTrue(driver.PageSource.Contains(content),"Page Source does not contain this text");
        }

        [TearDown]

        public void Quit()
        {
            driver.Quit();
        }
    }
}
