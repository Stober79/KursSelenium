using FakestorePageObjects;
using Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace SeleniumTests
{
    class CartTests : BaseTests
    {

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
        [Test]
        public void AddProductToTheCartTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart();

            Assert.Multiple(() =>
             {
                 Assert.AreEqual(1, cartPage.CartItems.Count, "Number of products in cart is not 1.");
                 Assert.AreEqual(productsIDs[0], cartPage.ItemIDs[0], "Product's in cart is not " + productsIDs[0]);
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

            Assert.DoesNotThrow(() =>
            _ = cartPage.CartEmptyMessage, "Cart is not empty.");
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
                CustomAssert.ThrowsWebDriverTimeoutException(() => _ = productPage.GoToCartButton, "Go to cart link wa found but it shouldn't. Nothing should be added to cart when you try add o items.");
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
            _ = cartPage.CartEmptyMessage, "There os no \"Empty Cart\" massage. Product was not removed from cart");


        }
        [Test]
        public void ChangingNumberOfItemsToZeroRemovesProductTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().ChangeQuantity(0);
            Assert.DoesNotThrow(() =>
            _ = cartPage.CartEmptyMessage, "There os no \"Empty Cart\" massage. Product was not removed from cart");


        }
        [Test]
        public void CanChangeToMoreThanStockTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().ChangeQuantity(productPage.stock + 1);

            Assert.IsTrue(productPage.IsQuantityFieldRangeOverflowPresent(), "blelel");

        }
    }
}