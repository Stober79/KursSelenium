using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Element
{
    class Checkbox
    {
        public static IWebElement RememberMe(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("#rememberme"));
            return element;
        }
    }
}