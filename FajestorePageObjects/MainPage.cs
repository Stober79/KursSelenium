using OpenQA.Selenium;

namespace FakestorePageObjects
{
    public class MainPage : BasePage
    {
        public DismissNoticeSection DismissNoticeSection => new DismissNoticeSection(driver);
        public MainPage(IWebDriver driver) : base(driver) { }

        public MainPage GoTo()
        {
            driver.Navigate().GoToUrl(baseUrl);
            return this;
        }
    }
}