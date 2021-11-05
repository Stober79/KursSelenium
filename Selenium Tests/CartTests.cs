using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using SeleniumTests.Element;
using SeleniumTests.StartSetup;

namespace SeleniumTests
{
    public class CartTests
    {
        RemoteWebDriver driver;
        DriverOptions options;
        WebDriverWait wait;
        IJavaScriptExecutor jse;
        IWebElement addedProduct;
        string baseUrl = "https://fakestore.testelka.pl";
        IList<IWebElement> CartItems => driver.FindElements(By.CssSelector("tr.cart_item"), 5);
        IWebElement Qty => driver.FindElement(By.CssSelector(".qty"), 2);
        IWebElement UpdateCart => driver.FindElement(By.CssSelector("button[name='update_cart']"), 2);
        IWebElement GoToCart => driver.FindElement(By.CssSelector("div.woocommerce-message a"), 2);
        IWebElement MainPage => driver.FindElement(By.CssSelector("li#menu-item-197"), 2);
        IWebElement Cart => driver.FindElement(By.CssSelector("li#menu-item-200"), 2);
        IWebElement CartIsEmpty => driver.FindElement(By.CssSelector("p.cart-empty"), 2);
        IWebElement RemoveProduct => driver.FindElement(By.CssSelector("td.product-remove a"), 2);
        IWebElement AddToCart => driver.FindElement(By.CssSelector("button[name = 'add-to-cart']"), 2);
        By Loaders => By.CssSelector(".blocUI");

        IList<string> productsIDs = new List<string>()
        {
              "389",
              "62"
        };
        IList<string> productsUrls = new List<string>()
        {
                "/product/wyspy-zielonego-przyladka-sal/",
                "/product/zmien-swoja-sylwetke-yoga-na-malcie/"
        };

        [SetUp]
        public void Setup()
        {
            options = new ChromeOptions();
            //options = new FirefoxOptions();
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);
            Start.Setup(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver).Click();
            jse = (IJavaScriptExecutor)driver;

        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void AddProductToTheCartTest()
        {
            ProductPage productPage = new ProductPage(driver);
            productPage.GoTo(productsUrls[0]);
            productPage.AddToCart();
            productPage.GoToCArt();
            CartPage cartPage = new CartPage(driver);
           Assert.Multiple(() =>
            {
                Assert.AreEqual(1,cartPage.CartItems.Count,"Number of products in cart is not 1.");
                Assert.AreEqual(productsIDs[0], cartPage.ItemID, "Product's in cart is not "+productsIDs[0]);
            });
        }
        [Test]
        public void AddProductWithManyItemsToTheCartTest()
        {
            AddItemsToTheCart("5", "section.storefront-recent-products li.post-4116");
            GoToCart.Click();
            addedProduct = CartItems[0].FindElement(By.XPath(".//td[@class='product-remove']/ a[@data-product_id='4116']/../following-sibling::td[@class='product-quantity']//input"));
            Assert.Multiple(() =>
            {
                Assert.IsTrue(CartItems.Count == 1);
                Assert.AreEqual("5", addedProduct.GetAttribute("value"), "Incorrect number of items.");
            });
        }
        [Test]
        public void AddTwoProductsToTheCartTest()
        {
            AddItemsToTheCart("1", "section.storefront-recent-products li.post-4116");
            MainPage.Click();
            AddItemsToTheCart("1", "section.storefront-recent-products li.post-393");
            GoToCart.Click();
            addedProduct = CartItems[0].FindElement(By.CssSelector("td.product-name a[href='https://fakestore.testelka.pl/product/windsurfing-w-lanzarote-costa-teguise/']"));
            Assert.IsTrue(CartItems.Count == 2);

        }
        [Test]
        public void CartIsEmptyTest()
        {
            Cart.Click();
            bool isMassageCartEmptyDisplayed = CartIsEmpty.Displayed;
            Assert.IsTrue(isMassageCartEmptyDisplayed, "Cart is not empty.");
        }
        [Test]
        public void AddZeroItemsToTheCartTest()
        {
            AddItemsToTheCart("0", "section.storefront-recent-products li.post-4116");
            bool isNotPossitive = (bool)jse.ExecuteScript("return arguments[0].validity.rangeUnderflow", Qty);
            Assert.IsTrue(isNotPossitive, "The minimum value is not 1.");
        }
        [Test]
        public void ChangeNumerOfItemsInCartTest()
        {
            AddItemsToTheCart("1", "section.storefront-recent-products li.post-4116");
            GoToCart.Click();
            ChangeQty("3");
            addedProduct = CartItems[0].FindElement(By.XPath(".//td[@class='product-remove']/ a[@data-product_id='4116']/../following-sibling::td[@class='product-quantity']//input"));
            Assert.AreEqual("3", addedProduct.GetAttribute("value"), "Incorrect number of items.");

        }
        [Test]
        public void RemoveProductFromCartTest()
        {
            AddItemsToTheCart("1", "section.storefront-recent-products li.post-4116");
            GoToCart.Click();
            RemoveProduct.Click();
            WaitForElementDisappear(Loaders);
            Assert.DoesNotThrow(() =>
            driver.FindElement(By.CssSelector("p.cart-empty")), "There os no \"Empty Cart\" massage. Product was not removed from cart");


        }
        [Test]
        public void RemoveProductFromCartTest2()
        {
            AddItemsToTheCart("1", "section.storefront-recent-products li.post-4116");
            GoToCart.Click();
            ChangeQty("0");
            WaitForElementDisappear(Loaders);
            bool isMassageCartEmptyDisplayed = CartIsEmpty.Displayed;
            Assert.DoesNotThrow(() =>
            driver.FindElement(By.CssSelector("p.cart-empty")), "There os no \"Empty Cart\" massage. Product was not removed from cart");

        }
        [Test]
        public void AddMoreThanMaxItemsTest()
        {
            AddItemsToTheCart("1", "section.storefront-recent-products li.post-4116");
            string stock = driver.FindElement(By.CssSelector("p.stock")).Text;
            GoToCart.Click();
            int maxNumber = Convert.ToInt32(stock.Replace(" w magazynie", "")) + 1;
            ChangeQty(maxNumber);
            bool isOverMax = (bool)jse.ExecuteScript("return arguments[0].validity.rangeOverflow", Qty);
            Assert.IsTrue(isOverMax, "blelel");

        }

        private void AddItemsToTheCart(string numeberOfItems, string product)
        {
            driver.FindElement(By.CssSelector(product)).Click();
            Qty.Clear();
            Qty.SendKeys(numeberOfItems);
            AddToCart.Click();
        }
        private void ChangeQty(string number)
        {
            Qty.Clear();
            Qty.SendKeys(number);
            UpdateCart.Click();
        }
        private void ChangeQty(int number)
        {
            string numerConverted = Convert.ToString(number);
            Qty.Clear();
            Qty.SendKeys(numerConverted);
            UpdateCart.Click();
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