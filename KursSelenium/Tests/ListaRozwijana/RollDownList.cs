using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace KursSelenium.Tests.ListaRozwijana
{
    class RollDownList
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
        public void IsMultipleTest()
        {

            Assert.IsTrue(selectElement.IsMultiple, "Is not multiple");
        }
        [Test]
        public void AllOptionsTest()
        {
            int numberOfAllOptions = selectElement.Options.Count;
            Assert.AreEqual(4, numberOfAllOptions, "Nie odpowiednia ilość opcji.");
        }
        [Test]
        public void WrappedElementTest()
        {
            IWebElement wrappedElement = selectElement.WrappedElement;
            string outerHTML = wrappedElement.GetAttribute("outerHTML");
        }
        [Test]
        public void AllSelectedOptionsTest()
        {
            selectElement.SelectByValue("vanilla");
            IList<IWebElement> selectedOptions = selectElement.AllSelectedOptions;
            Assert.AreEqual(1, selectedOptions.Count, "Nieporawan ilość zaznaczeń");
        }
    }
}
