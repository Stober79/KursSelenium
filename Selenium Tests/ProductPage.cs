using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTests
{
    public class ProductPage
    {
        private readonly RemoteWebDriver driver;
        private readonly string baseUrl = "https://fakestore.testelka.pl";
        private string ProductUrl => baseUrl + "/product";
        public int stock;
        private IWebElement AddToCartButton => driver.FindElement(By.CssSelector("button[name = 'add-to-cart']"), 2);
        public IWebElement GoToCartButton => driver.FindElement(By.CssSelector("div.woocommerce-message a"), 2);
        private IWebElement QuantityField => driver.FindElement(By.CssSelector(".qty"), 2);
        private IWebElement UpdateCart => driver.FindElement(By.CssSelector("button[name='update_cart']"), 2);
        private By Loaders => By.CssSelector(".blocUI");



        public ProductPage(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

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

        public ProductPage ChangeQuantityFieldOverTheStockValue()
        {
            QuantityField.Clear();
            QuantityField.SendKeys((stock + 1).ToString());
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
    }
}