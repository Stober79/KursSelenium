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
    class ActionsZadanie
    {
        IWebDriver driver;
        Actions action;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/actions/");
            Button.InfoList(driver).Click();
            action = new Actions(driver);

        }
        [Test]
        public void RightClickMenuTest()
        {
            IWebElement button = driver.FindElement(By.CssSelector("a#menu-link"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);
            IWebElement item = driver.FindElement(By.CssSelector("div#div-context-menu li.menu-cart"));
            action.ContextClick(button).Click(item).Build().Perform();
            Assert.AreEqual(Url.FakestoreCart(), driver.Url, "Przekierownie nie powiodło się.");
        }
        [Test]
        public void DoubleClickMenuTest()
        {
            IWebElement button = driver.FindElement(By.CssSelector("div[id=double-click]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);
            action.DoubleClick(button).Build().Perform();
            string color = button.GetCssValue("background-color");
            Assert.AreEqual("rgba(245, 93, 122, 1)", color, "Color does not change.");
        }
        [Test]
        public void ShowProvidedTextTest()
        {
            string testString = "test3";
            IWebElement field = driver.FindElement(By.CssSelector("#input"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", field);
            IWebElement button = driver.FindElement(By.CssSelector("button[onclick='zatwierdzTekst()']"));
            action.SendKeys(field,"test3").Click(button).Build().Perform();
            string tekst = driver.FindElement(By.CssSelector("#output")).Text;
            Assert.AreEqual("Wprowadzony tekst: "+testString, tekst, "Output is not correct.");
        }

        [Test]
        public void SelectMultipleBoxesTest()
        {
            int i;
            IWebElement list = driver.FindElement(By.CssSelector("ol#selectable"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", list);
            IList<IWebElement> listElements = list.FindElements(By.CssSelector("li"));
            action.KeyDown(Keys.Control).Click(listElements[2]).Click(listElements[3]).Click(listElements[5]).KeyUp(Keys.Control).Build().Perform();

            for (i = 0;i< listElements.Count;i++)
            {
               if(listElements[i].GetAttribute("class") == "ui-state-default ui-selectee ui-selected")
                {
                    string containColor = listElements[i].GetCssValue("background-color");
                    Assert.AreEqual("rgba(243, 152, 20, 1)", listElements[i].GetCssValue("background-color"),"Color does not change");
               }
            }
        }

        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}
