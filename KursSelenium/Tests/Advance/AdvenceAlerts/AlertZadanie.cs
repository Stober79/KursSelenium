using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;


namespace KursSelenium.Tests.Advance.AdvenceAlerts
{
    class AlertZadanie
    {
        IWebDriver driver;
        string text;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/alerty/");
            Button.InfoList(driver).Click();
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void Test1()
        {
            driver.FindElement(By.CssSelector("button[onclick='confirmFunction()']")).Click();
            driver.SwitchTo().Alert().Accept();
            text = driver.FindElement(By.CssSelector("p#demo")).Text;
            Assert.AreEqual("Wybrana opcja to OK!", text, "Nie wybrano OK");
        }
        [Test]
        public void Test2()
        {
            driver.FindElement(By.CssSelector("button[onclick='confirmFunction()']")).Click();
            driver.SwitchTo().Alert().Dismiss();
            text = driver.FindElement(By.CssSelector("p#demo")).Text;
            Assert.AreEqual("Wybrana opcja to Cancel!", text, "Nie wybrano OK");
        }
        [Test]
        public void Test3()
        {
            driver.FindElement(By.CssSelector("button[onclick='promptFunction()']")).Click();
            driver.SwitchTo().Alert().SendKeys("Robert Steiman");
            driver.SwitchTo().Alert().Accept();
            text = driver.FindElement(By.CssSelector("p#prompt-demo")).Text;
            Assert.AreEqual("Cześć Robert Steiman! Jak leci?", text, "Nie wybrano OK");
        }
        [Test]
        public void Test4()
        {
            driver.FindElement(By.CssSelector("button[onclick='promptFunction()']")).Click();
            driver.SwitchTo().Alert().Accept();
            text = driver.FindElement(By.CssSelector("p#prompt-demo")).Text;
            Assert.AreEqual("Cześć Harry Potter! Jak leci?", text, "Nie wybrano OK");
        }
        [Test]
        public void Test5()
        {
            driver.FindElement(By.CssSelector("button[onclick='promptFunction()']")).Click();
            driver.SwitchTo().Alert().Dismiss();
            text = driver.FindElement(By.CssSelector("p#prompt-demo")).Text;
            Assert.AreEqual("Użytkownik anulował akcję.", text, "Nie wybrano OK");
        }
        [Test]
        public void Test6()
        {
            driver.FindElement(By.CssSelector("button[onclick='delayedAlertFunction()']")).Submit();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d =>
                {
                    try { return driver.SwitchTo().Alert(); }
                    catch (NoAlertPresentException) { return null; }
                });
            text = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();
            Assert.AreEqual("Miałem mały poślizg", text, "Nie wybrano OK");
        }
    }
}
