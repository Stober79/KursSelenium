using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.TestProject
{
    class CouponTests
    {
        RemoteWebDriver driver;
        DriverOptions options;
        string baseUrl = "https://fakestore.testelka.pl/";
        string minimalCoupnValue = "kwotowy300";
        int minimalValueCoupnValue = 300;
        string procentCoupon = "10procent";
        float procentValuCoupon = 0.1f;
        string windsurfingCoupon = "windsurfing350";
        int windsurfingCouponValue = 350;
        string oldCoupon = "starośćnieradość";
        IList<string> productsUrls = new List<string>()
        {
            "/windsurfing-w-lanzarote-costa-teguise/",
            "/product/gran-koscielcow/"
        };
        IList<float> productPrices = new List<float>()
        {
            3000,
            2999.99f,
            350
        };
        IWebElement AddtoTheCart => driver.FindElement(By.CssSelector("button[name='add-to-cart']"),2);
        IWebElement GoToCart => driver.FindElement(By.CssSelector("div.woocommerce-message a"), 2);
        IWebElement CouponCodeField => driver.FindElement(By.CssSelector("#coupon_code"), 2);
        IWebElement CouponMassage => driver.FindElement(By.CssSelector("div.woocommerce-message"), 2);
        IWebElement ApplyCouponButton => driver.FindElement(By.CssSelector("button[name='apply_coupon']"), 2);
        IWebElement CartDiscount => driver.FindElement(By.CssSelector("tr.cart-discount th"), 2);
        IWebElement CartDiscountRow => driver.FindElement(By.CssSelector("tr.cart-discount"), 2);
        IWebElement CartDiscountValue => driver.FindElement(By.CssSelector("tr.cart-discount span"), 2);
        IWebElement OrderTotal => driver.FindElement(By.CssSelector("tr.order-total"), 2);
        IWebElement OrderTotalWithoutTextElement => driver.FindElement(By.CssSelector("tr.order-total td strong"), 2);
        IList<IWebElement> ErrorList => driver.FindElements(By.CssSelector("ul.woocommerce-error li"), 2);

        By Loaders => By.CssSelector(".blocUI");

        [SetUp]
        public void Setup()
        {
            options = new ChromeOptions();
            //options = new FirefoxOptions();
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);
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
        public void MinimaValueCouponTest()
        {
            driver.Navigate().GoToUrl(baseUrl + productsUrls[0]);
            AddCoupon(minimalCoupnValue);
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Kupon został pomyślnie użyty.", CouponMassage.Text, "Coupon was not succesfully apply.");
                Assert.AreEqual("Kupon: " + minimalCoupnValue, CartDiscount.Text, "Coupon was not apply.");
                Assert.AreEqual(FormatNumber(productPrices[0] -minimalValueCoupnValue), OrderTotalWithoutTextElement.Text,"Total is not correct");
                Assert.AreEqual("300,00 zł", CartDiscountValue.Text, "Discount value is not correct.");
            });
        }
        [Test]
        public void ToSmallValueToMinimaValueCouponTest()
        {
            driver.Navigate().GoToUrl(baseUrl + productsUrls[1]);
            AddCoupon(minimalCoupnValue);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, ErrorList.Count, " Number of error massages is correct.");
                Assert.AreEqual("Minimalna wartość zamówienia dla tego kuponu to 3 000,00 zł.", ErrorList[0].Text, "Coupon was not succesfully apply.");
                Assert.Throws<WebDriverTimeoutException>(() => _ =
                CartDiscountRow, "Cart discount element was found in cart summary");
            });
        }
        [Test]
        public void PercentCouponTest()
        {
            driver.Navigate().GoToUrl(baseUrl + productsUrls[1]);
            AddCoupon(procentCoupon);
            float roundDiscount = procentValuCoupon * productPrices[1];
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Kupon został pomyślnie użyty.", CouponMassage.Text, "Coupon was not succesfully apply.");
                Assert.AreEqual("Kupon: " + procentCoupon, CartDiscount.Text, "Coupon was not apply.");
                Assert.AreEqual(FormatNumber(productPrices[1] - roundDiscount), OrderTotalWithoutTextElement.Text, "Total is not correct");
                Assert.AreEqual(FormatNumber(roundDiscount), CartDiscountValue.Text, "Discount value is not correct.");
            });
        }
        [Test]
        public void CategoryCouponTest()
        {
            driver.Navigate().GoToUrl(baseUrl + productsUrls[0]);
            AddCoupon(windsurfingCoupon);
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Kupon został pomyślnie użyty.", CouponMassage.Text, "Coupon was not succesfully apply.");
                Assert.AreEqual("Kupon: " + windsurfingCoupon, CartDiscount.Text, "Coupon was not apply.");
                Assert.AreEqual(FormatNumber(productPrices[0] - windsurfingCouponValue), OrderTotalWithoutTextElement.Text, "Total is not correct");
                Assert.AreEqual(FormatNumber(windsurfingCouponValue), CartDiscountValue.Text, "Discount value is not correct.");
            });


        }
        [Test]
        public void WrongCategoryCouponTest()
        {
            driver.Navigate().GoToUrl(baseUrl + productsUrls[1]);
            AddCoupon(windsurfingCoupon);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, ErrorList.Count, " Number of error massages is correct.");
                Assert.AreEqual("Przepraszamy, tego kuponu nie można zastosować do wybranych produktów.", ErrorList[0].Text, "Coupon was not succesfully apply.");
                Assert.Throws<WebDriverTimeoutException>(() => _ =
                CartDiscountRow, "Cart discount element was found in cart summary");
            });


        }
        [Test]
        public void OldCouponTest()
        {
            driver.Navigate().GoToUrl(baseUrl + productsUrls[1]);
            AddCoupon(oldCoupon);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, ErrorList.Count, " Number of error massages is correct.");
                Assert.AreEqual("Ten kupon stracił ważność.", ErrorList[0].Text, "Coupon was not succesfully apply.");
                Assert.Throws<WebDriverTimeoutException>(() => _ =
                CartDiscountRow, "Cart discount element was found in cart summary");
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
        private string FormatNumber(float number)
        {
            if (number < 1000)
            {
                return string.Format("{0:###.00}", number) + " zł";
            }
            else return string.Format("{0:### ###.00}", number) + " zł";
        }
        private void AddCoupon (string coupon)
        {
            AddtoTheCart.Click();
            GoToCart.Click();
            CouponCodeField.SendKeys(coupon);
            ApplyCouponButton.Click();
            WaitForElementDisappear(Loaders);
        }

    }
}
