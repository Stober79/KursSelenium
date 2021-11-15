using Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace FakestorePageObjects
{
    public class CheckoutPage
    {
        private RemoteWebDriver driver;
        private IWebElement CardNumberField => driver.FindElement(By.CssSelector("input[name='cardnumber']"), 5);
        private IWebElement CardDataField => driver.FindElement(By.CssSelector("input[name='exp-date']"), 2);
        private IWebElement CardCodeField => driver.FindElement(By.CssSelector("input[name='cvc']"), 2);
        private IWebElement CheckoutForm => driver.FindElement(By.CssSelector("form[name='checkout']"), 2);
        private IWebElement CardNumberFrame => driver.FindElement(By.CssSelector("div#stripe-card-element iframe"), 2);
        private IWebElement CardDataFrame => driver.FindElement(By.CssSelector("div#stripe-exp-element iframe"), 2);
        private IWebElement CardCodeFrame => driver.FindElement(By.CssSelector("div#stripe-cvc-element iframe"), 2);
        private IWebElement PlaceOrderButton => driver.FindElement(By.CssSelector("button#place_order"), 2);
        public IWebElement ErrorList => driver.FindElement(By.CssSelector("ul.woocommerce-error"), 2);
        public IList<IWebElement> ErrorMessagesList => driver.FindElements(By.CssSelector("ul.woocommerce-error li"), 2);
        public IWebElement CartSubtotalElement => driver.FindElement(By.CssSelector(".cart-subtotal bdi"), 2);
        public IWebElement ProductsTotalElement => driver.FindElement(By.CssSelector(".product-total bdi"), 2);
        public IWebElement OrderTotalElement => driver.FindElement(By.CssSelector(".order-total td"), 2);
        public IList<IWebElement> ProductsTotalElements => driver.FindElements(By.CssSelector(".product-total bdi"), 2);

        public CheckoutPage(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        public CheckoutPage FillInCardData(string cardNumber, string cardExpirationDate, string cardCvc)
        {
            _ = CheckoutForm;
            driver.SwitchTo().Frame(CardNumberFrame);
            CardNumberField.SendKeys(cardNumber);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(CardDataFrame);
            CardDataField.SendKeys(cardExpirationDate);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(CardCodeFrame);
            CardCodeField.SendKeys(cardCvc);
            driver.SwitchTo().DefaultContent();
            return this;
        }

        public CheckoutPage PlaceOrder()
        {
            PlaceOrderButton.Click();
            return this;
        }
    }
}