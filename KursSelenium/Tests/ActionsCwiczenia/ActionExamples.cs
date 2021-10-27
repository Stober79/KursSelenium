using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Tests.ActionsCwiczenia
{
    class ActionExamples
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/actions/");
            Button.InfoList(driver).Click();

        }
        [Test]
        public void ExampleSelectedTest()
        {
            Actions action = new Actions(driver);
            action.MoveByOffset(500,600).Click().Build().Perform();
        }
        [Test]
        public void ContextClickTest()
        {
            IWebElement button = driver.FindElement(By.CssSelector("div[id=double-click]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);
            Actions action = new Actions(driver);
            action.DoubleClick(button).Build().Perform();
        }

        [Test]
        public void RightClickTest()
        {
            IWebElement button = driver.FindElement(By.CssSelector("a#menu-link"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);
            Actions action = new Actions(driver);
            IWebElement item = driver.FindElement(By.CssSelector("div#div-context-menu li.menu-main"));
            action.ContextClick(button).Click(item).Build().Perform();
            Assert.AreEqual(Url.FakestoreMainPage(), driver.Url, "Przekierownie nie powiodło się.");
        }

        [Test]
        public void SendKeysTest()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            IWebElement loginField = Field.Username(driver);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", loginField);
            Actions action = new Actions(driver);
            action.SendKeys(loginField, "test").Build().Perform();
            loginField.Clear();
            action.Click().SendKeys("test").Build().Perform();
            loginField.Clear();
            action.KeyDown(Keys.Shift).SendKeys("test").KeyUp(Keys.Shift).SendKeys("user").Build().Perform();

        }
        [Test]
        public void SelectMultipleElementsTest()
        {
            IWebElement list = driver.FindElement(By.CssSelector("ol#selectable"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", list);
            IList<IWebElement> listElements = list.FindElements(By.CssSelector("li"));
            Actions action = new Actions(driver);
            action.KeyDown(Keys.Control).Click(listElements[2]).Click(listElements[3]).KeyUp(Keys.Control).Build().Perform();
 

        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
            
    }
}
