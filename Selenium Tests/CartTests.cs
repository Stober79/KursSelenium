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
        IWebElement CartTable => driver.FindElement(By.CssSelector("table.shop_table.cart"), 2);
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
            CartPage cartPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart();
            
           Assert.Multiple(() =>
            {
                Assert.AreEqual(1,cartPage.CartItems.Count,"Number of products in cart is not 1.");
                Assert.AreEqual(productsIDs[0], cartPage.ItemIDs[0], "Product's in cart is not "+productsIDs[0]);
            });
        }
        [Test]
        public void TwoItemsOfProductAddedoCartTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[0]).AddToCart(2).GoToCart();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, cartPage.CartItems.Count, "Number of products in cart is not 1.");
                Assert.AreEqual(productsIDs[0], cartPage.ItemIDs[0], "Product's in cart is not " + productsIDs[0]);
                Assert.AreEqual("2", cartPage.QuantityField.GetAttribute("value"), "Number of items of the product is not 2");
            });
        }
        [Test]
        public void AddTwoProductsToTheCartTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[0]).AddToCart().GoTo(productsUrls[1]).AddToCart().GoToCart();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(productsIDs[0], cartPage.ItemIDs[0], "Product's in cart is not " + productsIDs[0]);
                Assert.AreEqual(productsIDs[1], cartPage.ItemIDs[1], "Product's in cart is not " + productsIDs[1]);
            });

        }
        [Test]
        public void CartIsEmptyTest()
        {
            CartPage cartPage = new CartPage(driver);
            cartPage.GoTo();

            Assert.DoesNotThrow(()=>
            _=cartPage.CartEmptyMessage, "Cart is not empty.");
        }
        [Test]
        public void CantAddZeroItemsTest()
        {
            ProductPage productPage = new ProductPage(driver);
            productPage.GoTo(productsUrls[0]).
                AddToCart(0);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(productPage.IsQuantityFieldRangeUnderflowPresent(), "The minimum value is not 1.");
               Assert.Throws<WebDriverTimeoutException>(() => _ = productPage.GoToCartButton, "Go to cart link wa found but it shouldn't. Nothing should be added to cart when you try add o items.");
            });
        }
        [Test]
        public void CanIncreaseNumberOfItemsTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().ChangeQuantity(3);
            Assert.AreEqual("3", cartPage.QuantityField.GetAttribute("value"), "Incorrect number of items.");

        }
        [Test]
        public void CanRemoveProductFromCartTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().RemoveItem(productsIDs[0]);

            Assert.DoesNotThrow(() =>
            _= cartPage.CartEmptyMessage, "There os no \"Empty Cart\" massage. Product was not removed from cart");


        }
        [Test]
        public void ChangingNumberOfItemsToZeroRemovesProductTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().ChangeQuantity(0);
            Assert.DoesNotThrow(() =>
            _=cartPage.CartEmptyMessage, "There os no \"Empty Cart\" massage. Product was not removed from cart");

        }
        [Test]
        public void CanChangeToMoreThanStockTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().ChangeQuantity(productPage.stock+1);

            Assert.IsTrue(productPage.IsQuantityFieldRangeOverflowPresent(), "blelel");

        }
    }
}