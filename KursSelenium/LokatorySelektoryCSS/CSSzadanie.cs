using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using KursSelenium.Element;

namespace KursSelenium.LokatorySelektoryCSS
{
    class CSSzadanie
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad =  TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void Test()
        {
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/");
            Button.InfoList(driver).Click();
            driver.FindElement(By.CssSelector("section.storefront-recent-products a[data-product_id='393']")).Click();
            Button.SeeCart(driver).Click();
            IWebElement droplist = driver.FindElement(By.CssSelector("ul.site-header-cart span.amount"));
            Assert.IsTrue(droplist.Displayed);
        }

        [TearDown]
        public void Quuit()
        {
            driver.Quit();
        }
    }
}
