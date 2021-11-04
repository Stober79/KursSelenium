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
        string baseUrl = "https://fakestore.testelka.pl";
        IList<string> productsUrls = new List<string>()
            {
            "/product/wyspy-zielonego-przyladka-sal/",
            "/product/zmien-swoja-sylwetke-yoga-na-malcie/"
            };
        IList<string> productPriceText = new List<string>()
        {
            "5 399,00 zł",
            "3 299,00 zł"
         };
        IList<int> productPrices = new List<int>()
        {
            5399,
            3299
         };
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
        IWebElement BuyAndPay => driver.FindElement(By.CssSelector("button#place_order"), 2);
        IWebElement CartSubtotalElement => driver.FindElement(By.CssSelector(".cart-subtotal bdi"), 2);
        IWebElement OrderTotalElement => driver.FindElement(By.CssSelector(".order-total td"), 2);
        IList<IWebElement> ErrorList => driver.FindElements(By.CssSelector("ul.woocommerce-error li"), 2);
        IList<IWebElement> ProductsTotalElements => driver.FindElements(By.CssSelector(".product-total bdi"), 2);
        IWebElement ProductsTotalElement => driver.FindElement(By.CssSelector(".product-total bdi"), 2);
        IWebElement QuantityField => driver.FindElement(By.CssSelector(".qty "), 2);
        IWebElement UpdateCart => driver.FindElement(By.CssSelector("button[name='update_cart']"), 2);
        IWebElement NumberOfItems => driver.FindElement(By.CssSelector("strong.product-quantity"), 2);
        IWebElement FirstNameField => driver.FindElement(By.CssSelector("input#billing_first_name"), 2);
        IWebElement LastNameField => driver.FindElement(By.CssSelector("input#billing_last_name"), 2);
        IWebElement CompanyField => driver.FindElement(By.CssSelector("input#billing_company"), 2);
        IWebElement StreetField => driver.FindElement(By.CssSelector("input#billing_address_1"), 2);
        IWebElement CityField => driver.FindElement(By.CssSelector("input#billing_city"), 2);
        IWebElement PhoneField => driver.FindElement(By.CssSelector("input#billing_phone"), 2);
        IWebElement EmailField => driver.FindElement(By.CssSelector("input#billing_email"), 2);
        IWebElement PostCodeField => driver.FindElement(By.CssSelector("input#billing_postcode"), 2);
        IWebElement UserLogin => driver.FindElement(By.CssSelector("div.woocommerce-form-login-toggle a"), 2);
        IWebElement LoginForm => driver.FindElement(By.CssSelector("form.login"), 2);
        IWebElement UsernameField => driver.FindElement(By.CssSelector("input#username"), 2);
        IWebElement PasswordField => driver.FindElement(By.CssSelector("input#password"), 2);
        IWebElement LoginButton => driver.FindElement(By.CssSelector("button[name='login']"), 2);

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
            AddFuerteventuraSotaventoToTheCart();
            _ =CheckoutForm;
            EnterPayCardData();
            BuyAndPay.Click();
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
        [Test]
        public void ReciveOneProductTest()
        {
            driver.Navigate().GoToUrl(baseUrl+productsUrls[0]);
            AddFuerteventuraSotaventoToTheCart();
            _ = CheckoutForm;
            float tax = CalculateTax(productPrices[0]);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(productPriceText[0], ProductsTotalElement.Text, "Incorrect price .");
                Assert.AreEqual(productPriceText[0], CartSubtotalElement.Text, "Incorrect price.");
                Assert.AreEqual(productPriceText[0]+" (zawiera "+FormatNumber(tax)+" VAT)",OrderTotalElement.Text, "Incorrect vat.");
            });


        }
        [Test]
        public void ReciveTwoProductTest()
        {
            driver.Navigate().GoToUrl(baseUrl+productsUrls[0]);
            QuantityField.Clear();
            QuantityField.SendKeys("2");
            AddToCart.Click();
            driver.Navigate().GoToUrl(baseUrl + productsUrls[1]);
            QuantityField.Clear();
            QuantityField.SendKeys("3");
            AddToCart.Click();
            GoToCart.Click();
            GoToPayment.Click();
            _ = CheckoutForm;
            float price = productPrices[0] * 2 + productPrices[1] * 3;
            Assert.Multiple(() =>
            {
                Assert.AreEqual(FormatNumber(productPrices[0]*2), ProductsTotalElements[0].Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(productPrices[0] * 2), ProductsTotalElements[0].Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(productPrices[1] * 3), ProductsTotalElements[1].Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(price), CartSubtotalElement.Text, "Incorrect price.");
                Assert.AreEqual(FormatNumber(price) + " (zawiera " + FormatNumber(CalculateTax(price)) + " VAT)", OrderTotalElement.Text, "Incorrect vat.");
            });
        }

        [Test]
        public void ChangeNumberOfItemsTest()
        {
            driver.Navigate().GoToUrl(baseUrl + productsUrls[0]);
            AddToCart.Click();
            GoToCart.Click();
            QuantityField.Clear();
            QuantityField.SendKeys("2");
            UpdateCart.Click();
            WaitForElementDisappear(Loaders);
            GoToPayment.Click();
            _ = CheckoutForm;
            float tax = CalculateTax(productPrices[0]*2);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(FormatNumber(productPrices[0] * 2), ProductsTotalElement.Text, "Incorrect price .");
                Assert.AreEqual("× 2", NumberOfItems.Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(productPrices[0] * 2), CartSubtotalElement.Text, "Incorrect price.");
                Assert.AreEqual(FormatNumber(productPrices[0] * 2) + " (zawiera " + FormatNumber(tax) + " VAT)", OrderTotalElement.Text, "Incorrect vat.");
            });

        }
        [Test]
        public void SuccesfulPaymentTest()
        {
            driver.Navigate().GoToUrl(baseUrl + productsUrls[0]);
            AddToCart.Click();
            GoToCart.Click();
            GoToPayment.Click();
             _ = CheckoutForm;
            FirstNameField.SendKeys("Katarzyna");
            LastNameField.SendKeys("Palusik");
            CompanyField.SendKeys("CC");
            StreetField.SendKeys("Toszecka");
            PostCodeField.SendKeys("44-100");
            CityField.SendKeys("Gliwice");
            PhoneField.SendKeys("123-456-789");
            EmailField.SendKeys("test@t.op");
            EnterPayCardData();
            ReadCheckbox.Click();
            BuyAndPay.Click();
            WaitForElementDisappear(Loaders);
            Assert.AreEqual("Zamówienie otrzymane", driver.FindElement(By.CssSelector("h1.entry-title")).Text, "Inccorect header.");

        }
        [Test]
        public void SuccesfulPaymentExistingUserTest()
        {
            driver.Navigate().GoToUrl(baseUrl + productsUrls[0]);
            AddToCart.Click();
            GoToCart.Click();
            GoToPayment.Click();
            UserLogin.Click();
            _ = wait.Until(d=>LoginForm.Displayed);
            UsernameField.SendKeys("katarzyna.palusik");
            PasswordField.SendKeys("$AdminAdmin123");
            LoginButton.Click();
            WaitForElementDisappear(Loaders);
            EnterPayCardData();
            ReadCheckbox.Click();
            BuyAndPay.Click();
            WaitForElementDisappear(Loaders);
            Assert.AreEqual("Zamówienie otrzymane", driver.FindElement(By.CssSelector("h1.entry-title")).Text, "Inccorect header.");



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
        private void AddFuerteventuraSotaventoToTheCart()
        {
           
            AddToCart.Click();
            GoToCart.Click();
            GoToPayment.Click();
        }
        private float CalculateTax(float productPrice)
        {
            return (float)Math.Round(productPrice - (productPrice / 1.23),2);
        }
        private string FormatNumber(float number)
        {
            return string.Format("{0:### ###.00}", number) +" zł";
        }
        private void EnterPayCardData()
        {
            driver.SwitchTo().Frame(CardNumberFrame);
            CardNumberField.SendKeys(cardNumber);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(CardDataFrame);
            CardDataField.SendKeys(data);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(CardCodeFrame);
            CardCodeField.SendKeys(cardCode);
            driver.SwitchTo().DefaultContent();
        }
    }
}