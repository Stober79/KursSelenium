using Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
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
        private IWebElement FirstNameField => driver.FindElement(By.CssSelector("input#billing_first_name"), 2);
        private IWebElement LastNameField => driver.FindElement(By.CssSelector("input#billing_last_name"), 2);
        private IWebElement CompanyField => driver.FindElement(By.CssSelector("input#billing_company"), 2);
        private IWebElement StreetField => driver.FindElement(By.CssSelector("input#billing_address_1"), 2);
        private IWebElement CityField => driver.FindElement(By.CssSelector("input#billing_city"), 2);
        private IWebElement PhoneField => driver.FindElement(By.CssSelector("input#billing_phone"), 2);
        private IWebElement EmailField => driver.FindElement(By.CssSelector("input#billing_email"), 2);
        private IWebElement PostCodeField => driver.FindElement(By.CssSelector("input#billing_postcode"), 2);
        private IWebElement ReadCheckbox => driver.FindElement(By.CssSelector("div.form-row input.input-checkbox"), 2);
        private IWebElement UserLogin => driver.FindElement(By.CssSelector("div.woocommerce-form-login-toggle a"), 2);
        private IWebElement LoginForm => driver.FindElement(By.CssSelector("form.login"), 2);
        private IWebElement UsernameField => driver.FindElement(By.CssSelector("input#username"), 2);
        private IWebElement PasswordField => driver.FindElement(By.CssSelector("input#password"), 2);
        private IWebElement LoginButton => driver.FindElement(By.CssSelector("button[name='login']"), 2);
        
        private By Loaders => By.CssSelector(".blocUI");

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

        public T PlaceOrder<T>()
        {
            PlaceOrderButton.Click();
            WaitForLoadersDisappear();
            return (T)Activator.CreateInstance(typeof(T), driver);
        }

        public CheckoutPage FillForm(string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8)
        {
            _ = CheckoutForm;
            FirstNameField.SendKeys(v1);
            LastNameField.SendKeys(v2);
            CompanyField.SendKeys(v3);
            StreetField.SendKeys(v4);
            PostCodeField.SendKeys(v5);
            CityField.SendKeys(v6);
            PhoneField.SendKeys(v7);
            EmailField.SendKeys(v8);
            return this;
        }

        public CheckoutPage CheckReadCheckbox()
        {
            ReadCheckbox.Click();
            return this;
        }
        private void WaitForLoadersDisappear()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            try
            {
                wait.Until(d => driver.FindElements(Loaders).Count == 0);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Element located by " + Loaders + " didn't disapear in 5 seconds");
                throw;
            }
        }

        public CheckoutPage OpenLogInForm()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            UserLogin.Click();
            _ = wait.Until(d => LoginForm.Displayed);
            return this;

        }
        public CheckoutPage FilLogInFields(string username, string password)
        {
            UsernameField.SendKeys(username);
            PasswordField.SendKeys(password);
            LoginButton.Click();
            return this;

        }
    }
}