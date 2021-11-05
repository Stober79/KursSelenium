using OpenQA.Selenium;

namespace SeleniumTests.Element
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