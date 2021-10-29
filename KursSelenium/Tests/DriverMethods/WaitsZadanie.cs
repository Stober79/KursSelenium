using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KursSelenium.Tests.DriverMethods
{
    class WaitsZadanie
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void Test1()
        {
            AddToCart();
            string color = driver.FindElement(By.CssSelector("div.woocommerce-message")).GetCssValue("background-color");
            string expected = "rgba(15, 131, 77, 1)";
            Assert.AreEqual(expected, color, "Background color does not mach");

        }
        [Test]
        public void Test2()
        {
            AddToCart();
            driver.FindElement(By.CssSelector("div.woocommerce-message a")).Click();
            string isButtonDisable = driver.FindElement(By.CssSelector("button[name='update_cart']")).GetDomProperty("disabled");
            bool iBD = driver.FindElement(By.CssSelector("button[name='update_cart']")).Enabled;
            Assert.AreEqual("True", isButtonDisable, "Button is not disable");
            Assert.IsFalse(iBD);

        }
        [Test]
        public void Test3()
        {
            AddToCart();
            driver.FindElement(By.CssSelector("div.woocommerce-message a")).Click();
            driver.FindElement(By.CssSelector("a.checkout-button")).Click();
            string isButtonNotVisble = driver.FindElement(By.XPath(".//button[@name ='login']/preceding-sibling::input")).GetAttribute("type");
            string expected = "hidden";
            bool isDisplayed = driver.FindElement(By.XPath(".//button[@name ='login']/preceding-sibling::input")).Displayed;
            Assert.AreEqual(expected, isButtonNotVisble, "Button is not hidden");
            Assert.IsFalse(isDisplayed);

        }
        [Test]
        public void Test4()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            driver.FindElement(By.CssSelector("section.storefront-product-categories li.product-category")).Click();
            string defaultValue = driver.FindElement(By.CssSelector("header.woocommerce-products-header+div select.orderby")).GetDomProperty("selectedIndex");
            bool selected = driver.FindElements(By.CssSelector("select.orderby"))[0].FindElement(By.CssSelector("option[value='menu_order']")).Selected;
            Assert.AreEqual("0", defaultValue, "");
            Assert.IsTrue(selected);
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }

        private void AddToCart()
        {
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/product/egipt-el-gouna/");
            Button.InfoList(driver).Click();
            driver.FindElement(By.CssSelector("button[name='add-to-cart']")).Click();
        }
    }
}
