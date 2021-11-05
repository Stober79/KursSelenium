using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace SeleniumTests
{
    public class ProductPage
    {
        private RemoteWebDriver driver;
        private string baseUrl = "https://fakestore.testelka.pl";
        private string ProductUrl => baseUrl + "/product";
        private IWebElement AddToCartButton => driver.FindElement(By.CssSelector("button[name = 'add-to-cart']"), 2);
        private IWebElement GoToCartButton => driver.FindElement(By.CssSelector("div.woocommerce-message a"), 2);


        public ProductPage(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        public void GoTo(string productSlag)
        {
            driver.Navigate().GoToUrl(ProductUrl + productSlag);
        }

        public void AddToCart()
        {
            AddToCartButton.Click();
        }

        public void GoToCArt()
        {
            GoToCartButton.Click();
        }
    }
}