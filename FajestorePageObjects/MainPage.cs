using OpenQA.Selenium.Remote;

namespace FakestorePageObjects
{
    public class MainPage : BasePage
    {
        public DismissNoticeSection DismissNoticeSection => new DismissNoticeSection(driver);
        public MainPage(RemoteWebDriver driver) : base(driver) { }

        public MainPage GoTo()
        {
            driver.Navigate().GoToUrl(baseUrl);
            return this;
        }
    }
}