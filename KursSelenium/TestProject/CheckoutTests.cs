using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KursSelenium.TestProject
{
    class CheckoutTests
    {
        RemoteWebDriver driver;
        DriverOptions options;
        WebDriverWait wait;
        string cardNumber = "4242424242424242";
        string data = "02/22";
        string cardCode = "123";
        IWebElement AddToCart => driver.FindElement(By.CssSelector("button[name = 'add-to-cart']"), 2);
        IWebElement GoToCart => driver.FindElement(By.CssSelector("div.woocommerce-message a"), 2);
        IWebElement GoToPayment => driver.FindElement(By.CssSelector("a.checkout-button"), 2);
        IWebElement CardNumberField => driver.FindElement(By.CssSelector("input[name='cardnumber']"), 5);
        IWebElement CardDataField => driver.FindElement(By.CssSelector("input[name='exp-date']"), 2);
        IWebElement CardCodeField => driver.FindElement(By.CssSelector("input[name='cvc']"), 2);
        IWebElement CheckoutForm => driver.FindElement(By.CssSelector("form[name='checkout']"), 2);
        IWebElement CardNumberFrame => driver.FindElement(By.CssSelector("div#stripe-card-element iframe"), 2);
        IWebElement CardDataFrame => driver.FindElement(By.CssSelector("div#stripe-exp-element iframe"), 2);
        IWebElement CardCodeFrame => driver.FindElement(By.CssSelector("div#stripe-cvc-element iframe"), 2);
        IWebElement ReadCheckbox => driver.FindElement(By.CssSelector("div.form-row input.input-checkbox"), 2);
        IWebElement IBuyAndPay => driver.FindElement(By.CssSelector("button#place_order"), 2);
        IList<IWebElement> ErrorList => driver.FindElements(By.CssSelector("ul.woocommerce-error li"), 2);
        By Loaders => By.CssSelector(".blocUI");

        [SetUp]
        public void Setup()
        {
            options = new ChromeOptions();
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            Start.Setup(driver);
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver).Click();
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void FieldsValidationTest()
        {
            driver.Navigate().GoToUrl(Url.FakestoreFuerteventuraSotavento());
            AddToCart.Click();
            GoToCart.Click();
            GoToPayment.Click();
            _=CheckoutForm;
            driver.SwitchTo().Frame(CardNumberFrame);
            CardNumberField.SendKeys(cardNumber);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(CardDataFrame);
            CardDataField.SendKeys(data);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(CardCodeFrame);
            CardCodeField.SendKeys(cardCode);
            driver.SwitchTo().DefaultContent();
            IBuyAndPay.Click();
            WaitForElementDisappear(Loaders);
            IList<string> errorList = ErrorList.Select(element => element.Text).ToList();
            IList<string> expectedList = new List<string>{
                "Imię płatnika jest wymaganym polem." ,
                "Nazwisko płatnika jest wymaganym polem.",
                "Ulica płatnika jest wymaganym polem.",
                 "Kod pocztowy płatnika nie jest prawidłowym kodem pocztowym.",
                 "Miasto płatnika jest wymaganym polem.",
                "Telefon płatnika jest wymaganym polem.",
                "Adres email płatnika jest wymaganym polem.",
                 "Proszę przeczytać i zaakceptować regulamin sklepu aby móc sfinalizować zamówienie."
            };
            Assert.Multiple(()=>
            {
                Assert.DoesNotThrow(() => _ = ErrorList, "Error list was not found");
                Assert.AreEqual(errorList.OrderBy(element => element), errorList.OrderBy(element => element), "Inccorect massages.");
            });

        }
        private void WaitForElementDisappear(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            try
            {
                wait.Until(d => driver.FindElements(by).Count == 0);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Element located by " + by + " didn't disapear in 5 seconds");
                throw;
            }
        }
    }
}
