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

namespace KursSelenium.Tests.KilkaOkien
{
    class FewWindows
    {
        IWebDriver driver;
        IJavaScriptExecutor jse;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            jse = (IJavaScriptExecutor)driver;
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver).Click();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void DocumentationLinkTest()
        {
            string mainPageWindow = driver.CurrentWindowHandle;
            driver.FindElement(By.CssSelector("#doc")).Click();
            string documentationWindow = driver.WindowHandles[1];
            driver.SwitchTo().Window(documentationWindow);
            string text = driver.FindElement(By.CssSelector("h1.entry-title")).Text;
            Assert.AreEqual("Dokumentacja", text, "Nieprawidłowy nagłówek."); 
            driver.SwitchTo().Window(mainPageWindow);
            driver.FindElement(By.CssSelector("a.privacy-policy-link")).Click();
             text = driver.FindElement(By.CssSelector("h1.entry-title")).Text;
            Assert.AreEqual("Polityka prywatności", text, "Nieprawidłowy nagłówek.");
        }
        [Test]
        public void AddProductToTheWishListTest()
        {
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/product/wakacje-z-yoga-w-kraju-kwitnacej-wisni/");
            driver.FindElement(By.CssSelector(".add_to_wishlist ")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#yith-wcwl-message")));
            //wait.Until(d=> driver.FindElement(By.CssSelector("#yith-wcwl-message")));
            driver.FindElement(By.CssSelector("li#menu-item-248")).Click();
            string listaZyczen = driver.WindowHandles[1];
            driver.SwitchTo().Window(listaZyczen);
            string productLink = driver.FindElement(By.CssSelector("td.product-name>a")).Text;
            Assert.AreEqual("Wakacje z yogą w Kraju Kwitnącej Wiśni", productLink, "Produkt nie obecny na liście życzeń.");

        }
    }
}