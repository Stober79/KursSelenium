using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Pierwsze_Kroki
{
    class TytulyIZrodlaStron
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

        public void RetriveTitleTest()
        {
            driver.Navigate().GoToUrl("https://pl.wikipedia.org");
            driver.Manage().Window.Maximize();
            Assert.AreEqual("Wikipedia, wolna encyklopedia", driver.Title, "Title is not correct");
        }

        [Test]

        public void RetrivePageSourceTest()
        {
            driver.Navigate().GoToUrl("https://www.google.pl");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.XPath("//button[@id='L2AGLb']")).Click();
            string metaContent = "/images/branding/googleg/1x/googleg_standard_color_128dp.png";
            Assert.IsTrue(driver.PageSource.Contains(metaContent), "Meta content not included in Page Source");
        }
        [TearDown]

        public void Quit()
        {
            driver.Quit();
        }
    }
}
