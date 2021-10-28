using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KursSelenium.Tests.Pierwsze_Kroki
{
    class TytuliZrodloStrony_Zadanie
    {
        IWebDriver driver;

        [SetUp]

        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
        }

        [Test]

        public void TitleTest()
        {
            driver.Navigate().GoToUrl(Url.WikipediaEs());
            string expectedTitle = "Wikipedia, la enciclopedia libre";
            Assert.AreEqual(expectedTitle, driver.Title, "Page title is not correct.");
        }

        [Test]

        public void PageSourceTest()
        {
            driver.Navigate().GoToUrl(Url.WikipediaEs());
            string content = "lang=\"es\"";
            Assert.IsTrue(driver.PageSource.Contains(content), "Page Source does not contain this text");
        }

        [TearDown]

        public void Quit()
        {
            driver.Quit();
        }
    }
}
