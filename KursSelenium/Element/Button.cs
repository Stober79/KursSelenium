using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Element
{
    class Button
    {
        public static IWebElement InfoList(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector(".woocommerce-store-notice__dismiss-link"));
            return element;
        }

        public static IWebElement AddToCart(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.Name("add-to-cart"));
            return element;
        }

        public static IWebElement GoToCart(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.LinkText("Zobacz koszyk"));
            return element;
        }

        public static IWebElement SeeCart(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("section.storefront-recent-products a[title='Zobacz koszyk']"));
            return element;
        }

        public static IWebElement GoToPayment(IWebDriver driver)
        {
            IWebElement element = driver.FindElement((By.LinkText("Przejdź do płatności"))); 
            return element;
        }
        public static IWebElement LogIn(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("button[name='login']"));
            return element;
        }
        public static IWebElement Register(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("button[name='register']"));
            return element;
        }
        public static IWebElement ReturnToShop(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("p.return-to-shop"));
            return element;
        }
        public static IWebElement ApplyCoupon(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("button[name='apply_coupon']"));
            return element;
        }

    }
}
