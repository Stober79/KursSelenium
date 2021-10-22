using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using KursSelenium.Element;

namespace KursSelenium.LokatoryProsteLokatory
{
    class ProsteLokatoryZadanie
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Test()
        {
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/moje-konto/");
            Button.InfoList(driver).Click();
            IWebElement header = driver.FindElement(By.TagName("h1"));
            IWebElement mojeKonto = driver.FindElement(By.ClassName("entry-title"));
            IWebElement eMail = driver.FindElement(By.Id("username"));
            IWebElement hsalo = driver.FindElement(By.Id("password"));
            IWebElement checkbox = driver.FindElement(By.Id("rememberme"));
            IWebElement zalogujSie = driver.FindElement(By.Name("login"));
            IWebElement pelenlink = driver.FindElement(By.LinkText("Nie pamiętasz hasła?"));
            IWebElement czesciowyLink = driver.FindElement(By.PartialLinkText("Nie pamiętasz"));
            IWebElement link = driver.FindElement(By.ClassName("login")).FindElement(By.TagName("a"));

        }

        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}
