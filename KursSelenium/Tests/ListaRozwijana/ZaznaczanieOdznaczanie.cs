using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace KursSelenium.Tests.ListaRozwijana
{
    class ZaznaczanieOdznaczanie
    {
        IWebDriver driver;
        IWebElement element;
        SelectElement selectElement;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/lista-rozwijana/");
            Button.InfoList(driver).Click();
            element = driver.FindElement(By.CssSelector("#flavors-multiple"));
            selectElement = new SelectElement(element);
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]

        public void SelectTwoOptionsTest()
        {
            selectElement.SelectByIndex(2);
            selectElement.SelectByText("waniliowy");
            IList<IWebElement> selectedOptions = selectElement.AllSelectedOptions;
            string[] expectedOptinsText = { "waniliowy", "czekoladowy" };
            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, selectedOptions.Count);
                string[] actualText = { selectedOptions[0].Text, selectedOptions[1].Text };
                Assert.Contains(expectedOptinsText[0], actualText);
                Assert.Contains(expectedOptinsText[1], actualText);
            });

        }
    }
}
