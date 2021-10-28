using KursSelenium.Element;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;

namespace KursSelenium.Tests.DriverMethods
{
    class Waits
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver).Click();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }
        [Test]

        public void Test()
        {
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/product/grecja-limnos/");
            driver.FindElement(By.CssSelector("[name='add-to-cart'")).Click();
            driver.FindElement(By.CssSelector(".woocommerce-message .wc-forward")).Click();
            IWebElement quantityField = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='quantity']")));
            //IWebElement quantityField = wait.Until(driver=>driver.FindElement(By.CssSelector("[id^='quantity']")));
            quantityField.Clear();
            quantityField.SendKeys("2");
            driver.FindElement(By.CssSelector("[name='update_cart']")).Click();

            wait.Until(driver => driver.FindElements(By.CssSelector(".blockUI")).Count == 0);

            string amount = driver.FindElement(By.CssSelector(".order-total .amount")).Text;
            string expected = "6 400,00 zł";
            Assert.AreEqual(expected, amount, "Total price is not correct.");

        }
        [Test]

        public void GetLocalizationAndTagName()
        {
            IWebElement windsurfingImage = wait.Until(driver => driver.FindElement(By.CssSelector("[alt='Windsurfing']")));
            Point location = windsurfingImage.Location;
            Size size = windsurfingImage.Size;
            string tagName = windsurfingImage.TagName;
        }

        [Test]

        public void DisplayedCouponFieldTest()
        {
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/product/grecja-limnos/");
            driver.FindElement(By.CssSelector("[name='add-to-cart'")).Click();
            driver.FindElement(By.CssSelector(".woocommerce-message .wc-forward")).Click();
            wait.Until(driver => driver.FindElement((By.CssSelector(".checkout-button")))).Click();
            bool isCouponFieldDisplayed = wait.Until(driver => driver.FindElement(By.CssSelector("input#coupon_code"))).Displayed;
            Assert.IsFalse(isCouponFieldDisplayed);
        }
        [Test]
        public void SelecteRememberMeTest()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Checkbox.RememberMe(driver).Click();
            bool isSelected = wait.Until(driver => Checkbox.RememberMe(driver)).Selected;

            Assert.IsTrue(isSelected);
        }
        [Test]
        public void GetAtribiuteTest()
        {
            IWebElement search = Field.Search(driver);
            string placeholder = search.GetAttribute("placeholder");
            Assert.AreEqual("Szukaj produktów…", placeholder, "");
        }
        [Test]
        public void GetCSSStylesTest()
        {
            IWebElement search = Field.Search(driver);
            string color = search.GetCssValue("background-color");
            Assert.AreEqual("rgba(242, 242, 242, 1)", color, "");
        }
        [Test]
        public void GetPropertyTest()
        {
            driver.Navigate().GoToUrl(Url.FakestoreFuerteventuraSotavento());
            IWebElement amountOfProduct = driver.FindElement(By.CssSelector("input.qty"));
            string maxAmount = amountOfProduct.GetDomProperty("max");
            Assert.AreEqual("11844", maxAmount, "");
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }

    }
}
