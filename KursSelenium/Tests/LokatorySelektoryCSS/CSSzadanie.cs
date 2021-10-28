using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KursSelenium.Tests.LokatorySelektoryCSS
{
    class CSSzadanie
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
        }

        [Test]
        public void Test()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver).Click();
            driver.FindElement(By.CssSelector("section.storefront-recent-products a[data-product_id='393']")).Click();
            Button.SeeCart(driver).Click();
            IWebElement droplist = driver.FindElement(By.CssSelector("ul.site-header-cart span.amount"));
            Assert.IsTrue(droplist.Displayed);
        }

        [TearDown]
        public void Quuit()
        {
            driver.Quit();
        }
    }
}
