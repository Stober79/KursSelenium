using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Pierwsze_Kroki
{
    class PierwszeKrokiZadanie
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

        public void BackNavigationTest()
        {
            driver.Navigate().GoToUrl("https://pl.wikipedia.org");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.nasa.gov");
            driver.Navigate().Back();
            string wikpediaUrl = "https://pl.wikipedia.org/wiki/Wikipedia:Strona_g%C5%82%C3%B3wna";
            Assert.AreEqual(wikpediaUrl, driver.Url, "Url mismatch");

        }

        [Test]
        public void ForwordNavigationTest()
        {
            driver.Navigate().GoToUrl("https://pl.wikipedia.org");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.nasa.gov");
            driver.Navigate().Back();
            driver.Navigate().Forward();
            string nasaUrl = "https://www.nasa.gov/";
            Assert.AreEqual(nasaUrl, driver.Url, "Url mismatch");

        }

        [TearDown]

        public void Quit()

        {
            driver.Close();
            driver.Quit();
        }
    }
}
