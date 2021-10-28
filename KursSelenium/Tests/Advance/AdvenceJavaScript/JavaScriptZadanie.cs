using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KursSelenium.Advance.AdvenceJavaScript
{
    class JavaScriptZadanie
    {
        IWebDriver driver;
        IJavaScriptExecutor jse;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl(Url.FakestoreFuerteventuraSotavento());
            Button.InfoList(driver).Click();
            jse = (IJavaScriptExecutor)driver;
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void Test()
        {
            IWebElement opis = driver.FindElement(By.CssSelector("#tab-description"));
            jse.ExecuteScript("arguments[0].scrollIntoView(true)", opis);
            IWebElement stickyAddToCart = driver.FindElement(By.CssSelector(".storefront-sticky-add-to-cart"));
            Assert.IsTrue(stickyAddToCart.GetAttribute("class").Contains("storefront-sticky-add-to-cart--slideInDown"), "List is not visble.");
        }
    }

}
