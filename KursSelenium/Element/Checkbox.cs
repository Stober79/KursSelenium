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
        public static IWebElement CreateAccount(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.XPath(".//*[@id ='createaccount']"));
            return element;
        }
    }
}