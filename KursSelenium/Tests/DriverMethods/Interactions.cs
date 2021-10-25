using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Tests.DriverMethods
{
    class Interactions
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);

        }

        [Test]
        public void SearchTest()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver);
            IWebElement search = Field.Search(driver);
            search.SendKeys("Fuerteventura");
            search.Clear();
            search.SendKeys("Grecja");
            search.Clear();
            search.SendKeys("Fuerteventura");
            search.Submit();//Submit działa na formularzach form
            Assert.AreEqual(Url.FakestoreFuerteventuraSotavento(), driver.Url, "nie znaleziono produktu");
        }

        [Test]
        public void SuccessLoginEmail()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Button.InfoList(driver).Click();
            Field.Username(driver).SendKeys("katarzyna.palusik@codeconcept.pl");
            Field.Password(driver).SendKeys("$AdminAdmin123");
            Button.LogIn(driver).Click();
            String zalogowany = driver.FindElement(By.CssSelector("div.woocommerce-MyAccount-content>p")).Text;
            Assert.IsTrue(zalogowany.Contains("katarzyna.palusik"), "Niepoprawny użytkownik");

        }
        [Test]
        public void SuccessLoginUseName()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Button.InfoList(driver).Click();
            Field.Username(driver).SendKeys("katarzyna.palusik");
            Field.Password(driver).SendKeys("$AdminAdmin123");
            Button.LogIn(driver).Click();
            String zalogowany = driver.FindElement(By.CssSelector("div.woocommerce-MyAccount-content>p")).Text;
            Assert.IsTrue(zalogowany.Contains("katarzyna.palusik"), "Niepoprawny użytkownik");

        }
        [Test]
        public void BothFieldsAreEmpty()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Button.InfoList(driver).Click();
            Button.LogIn(driver).Click();
            String error = driver.FindElement(By.CssSelector("ul.woocommerce-error")).Text;
            Assert.IsTrue(error.Contains("Nazwa użytkownika jest wymagana."), "Niepoprawny użytkownik");

        }
        [Test]
        public void BothFieldsAreIncorrect()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Button.InfoList(driver).Click();
            Field.Username(driver).SendKeys("user");
            Field.Password(driver).SendKeys("test");
            Button.LogIn(driver).Click();
            String error = driver.FindElement(By.CssSelector("ul.woocommerce-error")).Text;
            Assert.IsTrue(error.Contains("Brak user wśród zarejestrowanych w witrynie użytkowników. Jeśli nie masz pewności co do nazwy użytkownika, użyj adresu e-mail."), "Niepoprawny użytkownik");

        }
        [Test]
        public void UserNameIsEmpty()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Button.InfoList(driver).Click();
            Field.Password(driver).SendKeys("$AdminAdmin123");
            Button.LogIn(driver).Click();
            String error = driver.FindElement(By.CssSelector("ul.woocommerce-error")).Text;
            Assert.IsTrue(error.Contains("Nazwa użytkownika jest wymagana."), "Niepoprawny użytkownik");

        }

        [Test]
        public void PasswordIsEmpty()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Button.InfoList(driver).Click();
            Field.Username(driver).SendKeys("katarzyna.palusik");
            Button.LogIn(driver).Click();
            String error = driver.FindElement(By.CssSelector("ul.woocommerce-error")).Text;
            Assert.IsTrue(error.Contains("Hasło jest puste."), "Niepoprawny użytkownik");

        }
        [Test]
        public void PasswordIsIncorrectUserNameLogin()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Button.InfoList(driver).Click();
            Field.Username(driver).SendKeys("katarzyna.palusik");
            Field.Password(driver).SendKeys("$Admin");
            Button.LogIn(driver).Click();
            String error = driver.FindElement(By.CssSelector("ul.woocommerce-error")).Text;
            Assert.IsTrue(error.Contains("Wprowadzone hasło dla użytkownika katarzyna.palusik jest niepoprawne"), "Niepoprawny użytkownik");

        }
        [Test]
        public void PasswordIsIncorrectEmailLogin()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Button.InfoList(driver).Click();
            Field.Username(driver).SendKeys("katarzyna.palusik@codeconcept.pl");
            Field.Password(driver).SendKeys("$Admin");
            Button.LogIn(driver).Click();
            String error = driver.FindElement(By.CssSelector("ul.woocommerce-error")).Text;
            Assert.IsTrue(error.Contains("Dla adresu email katarzyna.palusik@codeconcept.pl podano nieprawidłowe hasło."), "Niepoprawny użytkownik");

        }
        [Test]
        public void UserNameIsIncorrect()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Button.InfoList(driver).Click();
            Field.Username(driver).SendKeys("katarzyna");
            Field.Password(driver).SendKeys("$AdminAdmin123");
            Button.LogIn(driver).Click();
            String error = driver.FindElement(By.CssSelector("ul.woocommerce-error")).Text;
            Assert.IsTrue(error.Contains("Brak katarzyna wśród zarejestrowanych w witrynie użytkowników"), "Niepoprawny użytkownik");

        }
        [Test]
        public void WrongEmail()
        {
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Button.InfoList(driver).Click();
            Field.Username(driver).SendKeys("t@onet.pl");
            Field.Password(driver).SendKeys("$AdminAdmin123");
            Button.LogIn(driver).Click();
            String error = driver.FindElement(By.CssSelector("ul.woocommerce-error")).Text;
            Assert.IsTrue(error.Contains("Nieznany adres email. Proszę sprawdzić ponownie lub wypróbować swoją nazwę użytkownika."), "Niepoprawny użytkownik");

        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }

}
