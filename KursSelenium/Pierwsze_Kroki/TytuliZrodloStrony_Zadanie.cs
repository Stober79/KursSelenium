using KursSelenium.StartSetup;
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
            Start.Setup(driver);
        }

        [Test]

        public void TitleTest()
        {
            driver.Navigate().GoToUrl("https://wikipedia.es/");
            string expectedTitle = "Wikipedia, la enciclopedia libre";
            Assert.AreEqual(expectedTitle, driver.Title, "Page title is not correct.");
        }

        [Test]

        public void PageSourceTest()
        {
            driver.Navigate().GoToUrl("https://wikipedia.es/");
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
