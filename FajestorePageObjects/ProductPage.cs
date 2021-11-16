using Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace FakestorePageObjects
{
    public class ProductPage : BasePage
    {

        private string ProductUrl => baseUrl + "/product";
        public int stock;
        private IWebElement AddToCartButton => driver.FindElement(By.CssSelector("button[name = 'add-to-cart']"), 2);
        public IWebElement GoToCartButton => driver.FindElement(By.CssSelector("div.woocommerce-message a"), 2);
        private IWebElement QuantityField => driver.FindElement(By.CssSelector(".qty"), 2);


        public ProductPage(RemoteWebDriver driver) : base(driver) { }

        public ProductPage GoTo(string productSlag)
        {
            driver.Navigate().GoToUrl(ProductUrl + productSlag);
            return this;
        }

        public ProductPage AddToCart(int quantity = 1)
        {
            if (quantity <= 0)
            {

                QuantityField.Clear();
                QuantityField.SendKeys(quantity.ToString());
                AddToCartButton.Click();
            }
            else if (quantity != 1)
            {
                QuantityField.Clear();
                QuantityField.SendKeys(quantity.ToString());
                AddToCartButton.Click();
                _ = GoToCartButton;
            }
            else
            {
                AddToCartButton.Click();
                _ = GoToCartButton;
            }
            stock = Convert.ToInt32(driver.FindElement(By.CssSelector("p.stock")).Text.Replace(" w magazynie", ""));
            return this;
        }

        public CartPage GoToCart()
        {
            GoToCartButton.Click();
            return new CartPage(driver);
        }

        public bool IsQuantityFieldRangeUnderflowPresent()
        {
            IJavaScriptExecutor jse = driver;
            return (bool)jse.ExecuteScript("return arguments[0].validity.rangeUnderflow", QuantityField);
        }
        public bool IsQuantityFieldRangeOverflowPresent()
        {
            IJavaScriptExecutor jse = driver;
            return (bool)jse.ExecuteScript("return arguments[0].validity.rangeOverflow", QuantityField);
        }
    }

}