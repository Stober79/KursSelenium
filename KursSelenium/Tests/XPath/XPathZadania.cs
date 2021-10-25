using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Tests.XPath
{
    class XPathZadania
    {
        IWebDriver driver;
        
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
        }

        [Test]
        public void Test()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver).Click();
            driver.FindElement(By.XPath(".//*[contains(@class,'recent-product')]//a[@data-product_id='4116']")).Click();
            driver.FindElement(By.XPath(".//*[contains(@class,'recent-product')]//a[@data-product_id='4114']")).Click();
            driver.FindElement(By.XPath(".//*[contains(@class,'recent-product')]//a[@data-product_id='393']")).Click();
            driver.FindElement(By.CssSelector("li.post-393>a[class='added_to_cart wc-forward']")).Click();
            IWebElement pole = driver.FindElement(By.XPath(".//td[@class='product-remove']//a[@data-product_id='393']/../following-sibling::td[@class='product-quantity']//input"));
            pole.Clear();
            pole.SendKeys("2"); 
            string kwota = "9 599,99 zł";
            string compare = driver.FindElement(By.XPath(".//tr[@class='cart-subtotal']//span")).Text;
            Assert.AreEqual(kwota, compare, "Different values");
        }

        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}
