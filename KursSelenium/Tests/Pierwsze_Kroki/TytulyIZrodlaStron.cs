using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KursSelenium.Tests.Pierwsze_Kroki
{
    class TytulyIZrodlaStron
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
        }

        [Test]

        public void RetriveTitleTest()
        {
            driver.Navigate().GoToUrl(Url.WikipediaPl());
            Assert.AreEqual("Wikipedia, wolna encyklopedia", driver.Title, "Title is not correct");
        }

        [Test]

        public void RetrivePageSourceTest()
        {
            driver.Navigate().GoToUrl(Url.Google());
            driver.FindElement(By.XPath("//button[@id='L2AGLb']")).Click();
            string metaContent = "/images/branding/googleg/1x/googleg_standard_color_128dp.png";
            Assert.IsTrue(driver.PageSource.Contains(metaContent), "Meta content not included in Page Source");
        }
        [TearDown]

        public void Quit()
        {
            driver.Quit();
        }
    }
}
