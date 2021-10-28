using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace KursSelenium.Tests.ActionsCwiczenia
{
    class ActionsPrzesuwanie
    {
        IWebDriver driver;
        Actions action;
        IJavaScriptExecutor jse;
        IWebElement drag;
        IWebElement drop;
        string expected => drag.GetAttribute("style");
        string dropped => drop.Text;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl(Url.FakeStoreAction());
            Button.InfoList(driver).Click();
            jse = (IJavaScriptExecutor)driver;
            action = new Actions(driver);
            drag = driver.FindElement(By.CssSelector("#draggable"));
            drop = driver.FindElement(By.CssSelector("#droppable"));
            jse.ExecuteScript("arguments[0].scrollIntoView(true)", drag);

        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void Test()
        {
            action.DragAndDrop(drag, drop).Build().Perform();
            string firstPosition = "position: relative; left: 145px; top: 25px;";
            string wasDropped = "Dropped!";
            Assert.AreEqual(wasDropped, dropped, "Element is not dropped in correct place.");
            Assert.AreEqual(firstPosition, expected, "Element was not moved to the corect position.");

        }
        [Test]
        public void Test1()
        {
            action.DragAndDrop(drag, drop).Build().Perform();
            string firstPosition = "position: relative; left: -55px; top: 25px;";
            action.DragAndDropToOffset(drag, -200, 0).Build().Perform();
            Assert.AreEqual(firstPosition, expected, "Element was not moved to the corect position.");

        }
        [Test]
        public void Test2()
        {
            action.ClickAndHold(drag).MoveByOffset(198, 87).Release().Build().Perform();
            string wasDropped = "Dropped!";
            Assert.AreEqual(wasDropped, dropped, "Element is not dropped in correct place.");
        }
        [Test]
        public void Test3()
        {
            action.ClickAndHold(drag).MoveByOffset(220, 87).Release().Build().Perform();
            string wasNotDropped = "Drop here";
            Assert.AreEqual(wasNotDropped, dropped, "Element is not dropped in correct place.");
        }
    }
}
