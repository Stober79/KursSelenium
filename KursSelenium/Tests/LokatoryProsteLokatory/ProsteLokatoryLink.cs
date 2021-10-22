using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.LokatoryProsteLokatory
{
    class ProsteLokatoryLink
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
        }

        [Test]
        public void LocatingByLink()
        {
            driver.Navigate().GoToUrl(Url.FakestoreFuerteventuraSotavento());
            Button.InfoList(driver).Click();
            Button.AddToCart(driver).Click();
            Button.GoToCart(driver).Click();
            IWebElement goToCart = Button.GoToPayment(driver);
            TestDelegate findLinkGoToPayment = new TestDelegate(FindLinkGoToPayment);//delegata potrzebuje funkcji
            Assert.IsTrue(goToCart.Displayed, "Element is not displayed");
            Assert.DoesNotThrow(findLinkGoToPayment);//uzywa delegaty


        }

        [Test]
        public void LocatingByLinkLambda()
        {
            driver.Navigate().GoToUrl(Url.FakestoreFuerteventuraSotavento());
            Button.InfoList(driver).Click();
            Button.AddToCart(driver).Click();
            Button.GoToCart(driver).Click();
            Assert.DoesNotThrow(()=> Element.Button.GoToPayment(driver), "Go to Payment not found");//wyrazenie Lambda


        }

        [Test]
        public void IsNotPaymentButttonOnPage()
        {
            driver.Navigate().GoToUrl(Url.FakestoreCart());//pusty koszyk
            Button.InfoList(driver).Click();
            Assert.Throws < NoSuchElementException>(()=> Button.GoToPayment(driver), "Go to payment link was not found");//wyrazenie Lambda


        }
        private void FindLinkGoToPayment()//funkcja delgaty
        {
            driver.FindElement(By.LinkText("Przejdź do płatności"));
        }

        [TearDown]

        public void Quit()
        {
            driver.Quit();
        }
    }

}
