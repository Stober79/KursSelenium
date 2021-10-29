using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KursSelenium.Tests.Advance.AdvenceCookies
{
    class CookieZadanie
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/product/wspinaczka-island-peak/");
            Button.InfoList(driver).Click();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void Test1()
        {
            driver.FindElement(By.CssSelector("button[name='add-to-cart']")).Click();
            string cookieValue = driver.Manage().Cookies.GetCookieNamed("woocommerce_recently_viewed").Value;
            Assert.AreEqual("42", cookieValue, "Cookie Value is not correct.");
        }
        [Test]
        public void Test2()
        {
            driver.FindElement(By.CssSelector("a.add_to_wishlist ")).Click();
            //wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div#yith-wcwl-message"))); ten był mojej konstukcji :)))))
            wait.Until(driver => driver.Manage().Cookies.AllCookies.Count == 4);
            IList<Cookie> cookieList = driver.Manage().Cookies.AllCookies;
            string cookieValue = cookieList.Where(cookie => cookie.Name.StartsWith("yith_wcwl_session")).Select(cookie => cookie.Value).First();
            Assert.IsNotEmpty(cookieValue, "Cookie Value is  empty.");
        }
    }
}
