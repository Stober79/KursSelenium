using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace KursSelenium.Tests.Pierwsze_Kroki
{
    class UstawieniaOkna
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
        public void WindowPositionTest()
        {
            Point start = driver.Manage().Window.Position;
            Assert.AreEqual(new Point(10, 10), start,"Start point is not correct");
        }
        [Test]
        public void WindowSizeTest()
        {
            Size size = driver.Manage().Window.Size;
            Assert.AreEqual(new Size(945, 1020), size, "Size is not correct");
        }

        [TearDown]

        public void Quit()
        {
            driver.Quit();
        }


    }
}
