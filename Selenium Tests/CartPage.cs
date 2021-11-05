using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace SeleniumTests
{
    public class CartPage
    {
        private RemoteWebDriver driver;
        public IList<IWebElement> CartItems => driver.FindElements(By.CssSelector("tr.cart_item"), 5);
        public string ItemID => CartItems[0].FindElement(By.CssSelector("a")).GetAttribute("data-product_id");

        public CartPage(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        
    }
}