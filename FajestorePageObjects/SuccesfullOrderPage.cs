using OpenQA.Selenium;

namespace FakestorePageObjects
{
    public class SuccesfullOrderPage : BasePage
    {
        public IWebElement OrderReciveMesseage => driver.FindElement(By.CssSelector("h1.entry-title"));
        public SuccesfullOrderPage(IWebDriver driver, string baseUrl) : base(driver, baseUrl) { }

    }
}
