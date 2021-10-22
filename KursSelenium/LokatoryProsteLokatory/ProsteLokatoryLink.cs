using KursSelenium.Element;
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
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
        }

        [Test]
        public void LocatingByLink()
        {
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/product/fuerteventura-sotavento/");
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
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/product/fuerteventura-sotavento/");
            Button.InfoList(driver).Click();
            Button.AddToCart(driver).Click();
            Button.GoToCart(driver).Click();
            Assert.DoesNotThrow(()=> Element.Button.GoToPayment(driver), "Go to Payment not found");//wyrazenie Lambda


        }

        [Test]
        public void IsNotPaymentButttonOnPage()
        {
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/koszyk/");//pusty koszyk
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
