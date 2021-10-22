using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Element
{
    class Field
    {
        public static IWebElement Username(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.Id("username"));
            return element;
        }

        public static IWebElement Password(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.Id("password"));
            return element;
        }

        public static IWebElement RegisterPassword(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("#reg_password"));
            return element;
        }

        public static IWebElement RegisterUsername(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("#reg_email"));
            return element;
        }
        public static IWebElement CouponCode(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("#coupon_code"));
            return element;
        }

    }
}
