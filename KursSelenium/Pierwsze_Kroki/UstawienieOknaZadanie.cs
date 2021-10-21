using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace KursSelenium.Pierwsze_Kroki
{
    class UstawienieOknaZadanie
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = System.TimeSpan.FromSeconds(10);
            driver.Manage().Window.Position = new Point(445, 30);
            driver.Manage().Window.Size = new Size(854,480);
        }

        [Test]
        public void SizeTest()
        {
            Size size = driver.Manage().Window.Size;
            driver.Navigate().GoToUrl("https://onet.pl");
            Assert.AreEqual(new Size(854, 480), size, "Niepoprawna pozycja.");
        }

        [Test]
        public void PointTest()
        {
            Point point = driver.Manage().Window.Position;
            driver.Navigate().GoToUrl("https://onet.pl");
            Assert.AreEqual(new Point(445, 30), point, "Niepoprawna pozycja.");
        }

        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}
