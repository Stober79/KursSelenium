using OpenQA.Selenium;

namespace FakestorePageObjects
{
    public class MainPage : BasePage
    {
        public DismissNoticeSection DismissNoticeSection => new DismissNoticeSection(driver, baseUrl);
        public MainPage(IWebDriver driver, string baseUrl) : base(driver, baseUrl) { }

        public MainPage GoTo()
        {
            driver.Navigate().GoToUrl(baseUrl);
            return this;
        }
    }
}