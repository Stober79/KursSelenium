using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using KursSelenium.Element;
using KursSelenium.StartSetup;

namespace KursSelenium.LokatoryProsteLokatory
{
    class MetodyLokalizujace
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
        }

        [Test]

        public void LocatingElementsTest()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver).Click();
            IWebElement search = Field.Search(driver);
            search.SendKeys("el gouna");
            search.Submit();
            Assert.AreEqual("Egipt – El Gouna – FakeStore", driver.Title, "Title is not correct");
        }
        [Test]
        public void LocatingElementsClassTest()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMainPage()); ;
            Button.InfoList(driver).Click();
            driver.FindElement(By.TagName("header")).FindElement(By.ClassName("search-field"));
            IWebElement search = driver.FindElement(By.ClassName("search-field"));
            search.SendKeys("el gouna");
            search.Submit();
            Assert.AreEqual("Egipt – El Gouna – FakeStore", driver.Title, "Title is not correct");
        }

        [Test]
        public void LocatingElementsClassTest2()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver).Click();
            IWebElement header = driver.FindElement(By.TagName("header"));
            IWebElement search = header.FindElement(By.ClassName("search-field"));
            search.SendKeys("el gouna");
            search.Submit();
            Assert.AreEqual("Egipt – El Gouna – FakeStore", driver.Title, "Title is not correct");
        }

        [Test]
        public void LocatingElementsNameTest()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver).Click();
            IWebElement search = driver.FindElement(By.CssSelector(".search-field"));
            search.SendKeys("el gouna");
            search.Submit();
            Assert.AreEqual("Egipt – El Gouna – FakeStore", driver.Title, "Title is not correct");
        }

        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}
