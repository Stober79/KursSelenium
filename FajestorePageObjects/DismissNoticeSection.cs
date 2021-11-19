using OpenQA.Selenium;

namespace FakestorePageObjects
{
    public class DismissNoticeSection : BasePage
    {
        public DismissNoticeSection(IWebDriver driver, string baseUrl) : base(driver, baseUrl) { }
        private IWebElement DismissLink => driver.FindElement(By.CssSelector(".woocommerce-store-notice__dismiss-link"));
        public void Dismiss()
        {
            DismissLink.Click();
        }
    }

}
