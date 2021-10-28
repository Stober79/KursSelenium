using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Advance.AdvanceFrames
{
    class FramesZadanie
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/cwiczenia-z-ramek/");
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
            driver.SwitchTo().Frame("main-frame");
            bool isButtonEnabled = driver.FindElement(By.CssSelector("input[name='main-page']")).Enabled;
            Assert.IsFalse(isButtonEnabled, "Button is enabled");
            
        }
        [Test]
        public void Test2()
        {
            driver.SwitchTo().Frame("main-frame");
            driver.SwitchTo().Frame("image");
            string tekst = driver.FindElement(By.XPath(".//img[@alt='Wakacje']/..")).GetAttribute("href");
            string expected = "https://fakestore.testelka.pl/";
            Assert.AreEqual(expected, tekst,"Niepoprawny link");

        }
        [Test]
        public void Test3()
        {
            driver.SwitchTo().Frame("main-frame");
            driver.SwitchTo().Frame("image");
            driver.SwitchTo().Frame(0);
            bool isButtonEnabled = driver.FindElement(By.CssSelector("p>a.button")).Enabled;
            Assert.IsTrue(isButtonEnabled, "Button is disabled");

        }
        [Test]
        public void Test4()
        {
            driver.SwitchTo().Frame("main-frame");
            driver.SwitchTo().Frame("image");
            driver.SwitchTo().Frame(0);
            driver.FindElement(By.CssSelector("p>a.button")).Click();
            driver.SwitchTo().ParentFrame().SwitchTo().ParentFrame();
            driver.FindElement(By.CssSelector("a[name='climbing']")).Click();
            bool isDisplayd = driver.FindElement(By.CssSelector("image.custom-logo")).Displayed;
            Assert.IsTrue(isDisplayd, "Image is not displayed");

        }
    }
}
