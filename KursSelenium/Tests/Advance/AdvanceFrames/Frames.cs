using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KursSelenium.Advance.AdvenceFrames
{
    class Frames
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/actions-przyklady/");
            Button.InfoList(driver);
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void FrameExampleTest()
        {
            driver.SwitchTo().Frame(0);// tu jest użyty numer ramki ale można użyć nazwy ramki( atrybut id lub name) lub id WebElementu
            driver.FindElement(By.XPath(".//li[text()='Item 5']"));
            driver.SwitchTo().DefaultContent(); //wychodzi do głównego dokumentu 
            //driver.SwitchTo().ParentFrame();do poprzedającej ramki jeśli nie było żadnej poprzedzającej to do główego dokumentu
        }
        [Test]
        public void FrameExampleTest1()
        {
            driver.SwitchTo().Frame("select");
            driver.FindElement(By.XPath(".//li[text()='Item 5']"));
        }
        [Test]
        public void FrameExampleTest2()
        {
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("#select")));
            driver.FindElement(By.XPath(".//li[text()='Item 5']"));
        }
    }
}
