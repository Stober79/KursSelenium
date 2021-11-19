using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace FakestorePageObjects
{
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;

        protected readonly string baseUrl;
        private By Loaders => By.CssSelector(".blocUI");
        protected BasePage(IWebDriver driver, string baseUrl)
        {
            this.driver = driver;
            this.baseUrl = baseUrl;
        }
        protected void WaitForLoadersDisappear()
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
