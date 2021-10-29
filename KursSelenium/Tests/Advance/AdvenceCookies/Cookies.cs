using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KursSelenium.Tests.Advance.AdvenceCookies
{
    class Cookies
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl(Url.FakestoreFuerteventuraSotavento());
            Button.InfoList(driver).Click();
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void GetCookies()
        {
            IList<Cookie> cookies = driver.Manage().Cookies.AllCookies;
            Assert.AreEqual(1, cookies.Count, "");
        }
        [Test]
        public void GetCookie()
        {
            string cookieValue = driver.Manage().Cookies.GetCookieNamed("store_noticef7db772cd2958546f5ffc7e2822d64e8").Value;
            Assert.IsNotEmpty(cookieValue, "store_noticef7db772cd2958546f5ffc7e2822d64e8 is empty");

        }
        [Test]
        public void AddCookie()
        {
            Cookie newcookie = new Cookie("new cookie", "hello!", "fakestore.testelka.pl", "/", DateTime.Now + TimeSpan.FromSeconds(30));
            driver.Manage().Cookies.AddCookie(newcookie);
            int numberOfCookies = driver.Manage().Cookies.AllCookies.Count;
            Assert.AreEqual(2, numberOfCookies, "NUmber of cookies is not what expected");

        }
        [Test]
        public void DeleteCookieNamed()
        {
           
            driver.Manage().Cookies.DeleteCookieNamed("store_noticef7db772cd2958546f5ffc7e2822d64e8");
            int numberOfCookies = driver.Manage().Cookies.AllCookies.Count;
            Assert.AreEqual(2, numberOfCookies, "NUmber of cookies is not what expected");

        }
        [Test]
        public void DeleteCookie()
        {
            Cookie newcookie = new Cookie("new cookie", "hello!", "fakestore.testelka.pl", "/", DateTime.Now + TimeSpan.FromSeconds(30));
            driver.Manage().Cookies.AddCookie(newcookie);
            driver.Manage().Cookies.DeleteCookie(newcookie);
            int numberOfCookies = driver.Manage().Cookies.AllCookies.Count;
            Assert.AreEqual(1, numberOfCookies, "NUmber of cookies is not what expected");

        }
        [Test]
        public void GetStoreNoticeCookie()
        {

            IList<Cookie> cookies = driver.Manage().Cookies.AllCookies;
            Cookie storeNoticeCookie = null;
            foreach (Cookie cookie in cookies)
            {
                if (cookie.Name.StartsWith("store_notice"))
                    storeNoticeCookie = cookie;
            }
            Assert.AreEqual("hidden", storeNoticeCookie.Value, "Store notice cookie value is not what expected.");

        }
        [Test]
        public void GetStoreNoticeCookie2()
        {

            IList<Cookie> cookies = driver.Manage().Cookies.AllCookies;
            string cookieValue = cookies.Where(cookie => cookie.Name.StartsWith("store_notice")).Select(cookie => cookie.Value).First();
            Assert.AreEqual("hidden", cookieValue, "Store notice cookie value is not what expected.");

        }
    }
}
