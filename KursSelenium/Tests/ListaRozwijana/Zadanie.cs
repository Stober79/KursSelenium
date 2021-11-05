using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace KursSelenium.Tests.ListaRozwijana
{
    class Zadanie
    {
        IWebDriver driver;
        IWebElement element;
        SelectElement selectElement;
        WebDriverWait wait;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl(Url.FakestoreFuerteventuraSotavento());
            Button.InfoList(driver).Click();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void ZadanieTest()
        {
            driver.FindElement(By.CssSelector("button[name='add-to-cart']")).Click();
            wait.Until(d => driver.FindElement(By.CssSelector("div.woocommerce-message>a.wc-forward"))).Click();
            driver.FindElement(By.CssSelector(".checkout-button")).Click();
            element = driver.FindElement(By.CssSelector("select#billing_country"));
            selectElement = new SelectElement(element);
            selectElement.SelectByValue("CU");
            IWebElement selected = selectElement.SelectedOption;
            Assert.AreEqual("Kuba", selected.Text, "Wybrano niepoprawny kraj.");

        }
    }
}
