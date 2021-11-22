using FakestorePageObjects;
using Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace SeleniumTests
{
    class CartTests : BaseTests
    {

        IList<string> ProductsIDs => new List<string>()
        {
              testData.Products[0].Id,
              testData.Products[1].Id
        };
        IList<string> ProductsUrls => new List<string>()
        {
            testData.Products[0].Url,
            testData.Products[1].Url
        };
        [Test]
        public void AddProductToTheCartTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CartPage cartPage = productPage.GoTo(ProductsUrls[0]).AddToCart().GoToCart();

            Assert.Multiple(() =>
             {
                 Assert.AreEqual(1, cartPage.CartItems.Count, "Number of products in cart is not 1.");
                 Assert.AreEqual(ProductsIDs[0], cartPage.ItemIDs[0], "Product's in cart is not " + ProductsIDs[0]);
             });
        }
        [Test]
        public void TwoItemsOfProductAddedoCartTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CartPage cartPage = productPage.GoTo(ProductsUrls[0]).AddToCart(2).GoToCart();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, cartPage.CartItems.Count, "Number of products in cart is not 1.");
                Assert.AreEqual(ProductsIDs[0], cartPage.ItemIDs[0], "Product's in cart is not " + ProductsIDs[0]);
                Assert.AreEqual("2", cartPage.QuantityField.GetAttribute("value"), "Number of items of the product is not 2");
            });
        }
        [Test]
        public void AddTwoProductsToTheCartTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CartPage cartPage = productPage.GoTo(ProductsUrls[0]).AddToCart().GoTo(ProductsUrls[1]).AddToCart().GoToCart();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProductsIDs[0], cartPage.ItemIDs[0], "Product's in cart is not " + ProductsIDs[0]);
                Assert.AreEqual(ProductsIDs[1], cartPage.ItemIDs[1], "Product's in cart is not " + ProductsIDs[1]);
            });

        }
        [Test]
        public void CartIsEmptyTest()
        {
            CartPage cartPage = new CartPage(driver, config.BaseUrl);
            cartPage.GoTo();

            Assert.DoesNotThrow(() =>
            _ = cartPage.CartEmptyMessage, "Cart is not empty.");
        }
        [Test]
        public void CantAddZeroItemsTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            productPage.GoTo(ProductsUrls[0]).
                AddToCart(0);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(productPage.IsQuantityFieldRangeUnderflowPresent(), "The minimum value is not 1.");
                CustomAssert.ThrowsWebDriverTimeoutException(() => _ = productPage.GoToCartButton, "Go to cart link wa found but it shouldn't. Nothing should be added to cart when you try add o items.");
            });
        }
        [Test]
        public void CanIncreaseNumberOfItemsTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CartPage cartPage = productPage.GoTo(ProductsUrls[0]).AddToCart().GoToCart().ChangeQuantity(3);
            Assert.AreEqual("3", cartPage.QuantityField.GetAttribute("value"), "Incorrect number of items.");

        }
        [Test]
        public void CanRemoveProductFromCartTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CartPage cartPage = productPage.GoTo(ProductsUrls[0]).AddToCart().GoToCart().RemoveItem(ProductsIDs[0]);

            Assert.DoesNotThrow(() =>
            _ = cartPage.CartEmptyMessage, "There os no \"Empty Cart\" massage. Product was not removed from cart");


        }
        [Test]
        public void ChangingNumberOfItemsToZeroRemovesProductTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CartPage cartPage = productPage.GoTo(ProductsUrls[0]).AddToCart().GoToCart().ChangeQuantity(0);
            Assert.DoesNotThrow(() =>
            _ = cartPage.CartEmptyMessage, "There os no \"Empty Cart\" massage. Product was not removed from cart");


        }
        [Test]
        public void CanChangeToMoreThanStockTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CartPage cartPage = productPage.GoTo(ProductsUrls[0]).AddToCart().GoToCart().ChangeQuantity(productPage.stock + 1);

            Assert.IsTrue(productPage.IsQuantityFieldRangeOverflowPresent(), "blelel");

        }
    }
}