using KursSelenium.Element;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace KursSelenium.Tests.DriverMethods
{
    class TimeOuts
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void TimeOutsTest()
        {
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/product/grecja-limnos/");
            Button.InfoList(driver);
            driver.FindElement(By.CssSelector("a.add_to_wishlist")).Click();
            driver.FindElement(By.CssSelector("span.feedback"));
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}
