using Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FakestorePageObjects
{
    public class CartPage
    {
        private readonly RemoteWebDriver driver;
        private readonly string baseUrl = "https://fakestore.testelka.pl";
        private string CartUrl => baseUrl + "/koszyk";

        public IList<IWebElement> CartItems
        {
            get
            {
                _ = CartTable;
                return driver.FindElements(By.CssSelector("tr.cart_item"), 5);
            }
        }
        public IList<string> ItemIDs => CartItems.Select(element => element.FindElement(By.CssSelector("a")).GetAttribute("data-product_id")).ToList();
        private IWebElement CartTable => driver.FindElement(By.CssSelector("table.shop_table.cart"), 2);
        private IWebElement UpdateCart => driver.FindElement(By.CssSelector("button[name='update_cart']"), 2);
        private IWebElement GoToCheckoutButton => driver.FindElement(By.CssSelector("a.checkout-button"), 2);

        public IWebElement QuantityField
        {
            get
            {
                _ = CartTable;
                return driver.FindElement(By.CssSelector(".qty"), 2);
            }
        }

        public IWebElement CartEmptyMessage => driver.FindElement(By.CssSelector(".cart-empty.woocommerce-info"));
        private By Loaders => By.CssSelector(".blocUI");

        public CartPage(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        public CartPage GoTo()
        {
            driver.Navigate().GoToUrl(CartUrl);
            return this;
        }

        public CartPage RemoveItem(string productId)
        {
            driver.FindElement(By.CssSelector("a[data-product_id='" + productId + "']")).Click();
            WaitForLoadersDisappear();
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

        public CartPage ChangeQuantity(int quantity = 1)
        {
            QuantityField.Clear();
            QuantityField.SendKeys(quantity.ToString());
            UpdateCart.Click();
            WaitForLoadersDisappear();
            return this;
        }

        public CheckoutPage GoToCheckout()
        {
            GoToCheckoutButton.Click();
            return new CheckoutPage(driver);
        }


    }
}