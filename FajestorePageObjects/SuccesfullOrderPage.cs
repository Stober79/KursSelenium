using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace FakestorePageObjects
{
    public class SuccesfullOrderPage : BasePage
    {
        public IWebElement OrderReciveMesseage => driver.FindElement(By.CssSelector("h1.entry-title"));
        public SuccesfullOrderPage(RemoteWebDriver driver) : base(driver) { }

    }
}
