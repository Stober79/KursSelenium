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
        private const string correctEmail = "katarzyna.palusik@codeconcept.pl";
        private const string incorrectEmail = "katarzyna@codeconcept.pl";
        private const string username = "katarzyna.palusik";
        private const string correctPassword = "$AdminAdmin123";
        private const string incorrectUsername = "user";
        private const string incorrectPassword = "test";
        private string  zalogowany => driver.FindElement(By.CssSelector("div.woocommerce-MyAccount-content>p:first-of-type")).Text;
        private string error => driver.FindElement(By.CssSelector("ul.woocommerce-error")).Text;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl(Url.FakestoreMyAccount());
            Button.InfoList(driver).Click();

        }

        [Test]
        public void SearchTest()
        {
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
            Login(correctEmail, correctPassword);
            Assert.IsTrue(zalogowany.Contains(username), "Niepoprawny użytkownik");

        }
        [Test]
        public void SuccessLoginUserName()
        {
            Login(username, correctPassword);
            Assert.IsTrue(zalogowany.Contains(username), "Niepoprawny użytkownik");

        }
        [Test]
        public void BothFieldsAreEmpty()
        {
            Login("", "");
            Assert.IsTrue(error.Contains("Nazwa użytkownika jest wymagana."), "Niepoprawny użytkownik");

        }
        [Test]
        public void BothFieldsAreIncorrect()
        {
            Login(incorrectUsername, incorrectPassword);
            Assert.IsTrue(error.Contains("Brak "+incorrectUsername+" wśród zarejestrowanych w witrynie użytkowników. Jeśli nie masz pewności co do nazwy użytkownika, użyj adresu e-mail."), "Niepoprawny użytkownik");

        }
        [Test]
        public void UserNameIsEmpty()
        {
            Login("", correctPassword);
            Assert.IsTrue(error.Contains("Nazwa użytkownika jest wymagana."), "Niepoprawny użytkownik");

        }

        [Test]
        public void PasswordIsEmpty()
        {
            Login(username, "");
            Assert.IsTrue(error.Contains("Hasło jest puste."), "Niepoprawny użytkownik");

        }
        [Test]
        public void PasswordIsIncorrectUserNameLogin()
        {
            Login(username, incorrectPassword);
            Assert.IsTrue(error.Contains("Wprowadzone hasło dla użytkownika " +username+" jest niepoprawne"), "Niepoprawny użytkownik");

        }
        [Test]
        public void PasswordIsIncorrectEmailLogin()
        {
            Login(correctEmail, incorrectPassword);
            Assert.IsTrue(error.Contains("Dla adresu email "+correctEmail+" podano nieprawidłowe hasło."), "Niepoprawny użytkownik");

        }
        [Test]
        public void UserNameIsIncorrect()
        {
            Login(incorrectUsername, correctPassword);
            Assert.IsTrue(error.Contains("Brak "+incorrectUsername+" wśród zarejestrowanych w witrynie użytkowników"), "Niepoprawny użytkownik");

        }
        [Test]
        public void WrongEmail()
        {
            Login(incorrectEmail, correctPassword);
            Assert.IsTrue(error.Contains("Nieznany adres email. Proszę sprawdzić ponownie lub wypróbować swoją nazwę użytkownika."), "Niepoprawny użytkownik");

        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        private void Login(string username,string password)
        {
            Field.Username(driver).SendKeys(username);
            Field.Password(driver).SendKeys(password);
            Button.LogIn(driver).Click();

        }
    }

}
