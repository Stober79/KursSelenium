using KursSelenium.Element;
using KursSelenium.StartSetup;
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
            Start.Setup(driver);
        }

        [Test]

        public void BackNavigationTest()
        {
            driver.Navigate().GoToUrl(Url.WikipediaPl());
            driver.Navigate().GoToUrl(Url.Nasa());
            driver.Navigate().Back();
            string wikpediaUrl = "https://pl.wikipedia.org/wiki/Wikipedia:Strona_g%C5%82%C3%B3wna";
            Assert.AreEqual(wikpediaUrl, driver.Url, "Url mismatch");

        }

        [Test]
        public void ForwordNavigationTest()
        {
            driver.Navigate().GoToUrl(Url.WikipediaPl());
            driver.Navigate().GoToUrl(Url.Nasa());
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
