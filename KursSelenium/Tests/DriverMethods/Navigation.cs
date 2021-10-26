using System;
using System.Collections.Generic;
using System.Text;
using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;


namespace KursSelenium.Tests.DriverMethods
{
    class Navigation
    {
       
            IWebDriver driver;

            [SetUp]

            public void Setup()
            {
                driver = new ChromeDriver();
                Start.Setup(driver);
            }

            [Test]
            public void Test1()
            {
                 Uri googleUri = new Uri(Url.Google());
                 driver.Navigate().GoToUrl(googleUri);
                 string expctedUrl = "https://www.google.pl/";
                 Assert.AreEqual(expctedUrl, driver.Url, "Url do not match");
            }

            [Test]
            public void LogIn()
            {
                driver.Navigate().GoToUrl(Url.Onet());
                string expctedUrl = "https://www.onet.pl/";
                Assert.AreEqual(expctedUrl, driver.Url, "Url does not match");
                driver.FindElement(By.ClassName("cmp-intro_acceptAll")).Click();
                driver.FindElement(By.XPath("//a[@href='http://poczta.onet.pl/']")).Click();
                driver.FindElement(By.Id("mailFormLogin")).SendKeys("robertsteiman@op.pl");
                driver.FindElement(By.Id("mailFormPassword")).SendKeys("Jung1979!");
                driver.FindElement(By.XPath("//input[(@class='loginButton')]")).Click();
                IWebElement logout =  driver.FindElement(By.XPath("//a[@title='Wyloguj']"));
                Assert.AreEqual(true, logout.Displayed);
               
            }

            [Test]
            public void LogOut()
            {
                LogIn();
                driver.FindElement(By.XPath("//a[@title='Wyloguj']")).Click();
                string pocztaUrl = "https://www.onet.pl/poczta";
                var isEqual = driver.Url.StartsWith(pocztaUrl, StringComparison.OrdinalIgnoreCase);
                Assert.True(isEqual);
            }

            [Test]
            public void OpenAnotherWindow()
            {
                driver.Navigate().GoToUrl(Url.Onet());
                driver.FindElement(By.ClassName("cmp-intro_acceptAll")).Click();
                Actions action = new Actions(driver);
                IWebElement link = driver.FindElement(By.XPath("//a[@class='WeatherDay_tempValue__aHsz9']"));
                action.SendKeys(link, Keys.Enter).Perform();
                string pogodaUrl = "https://pogoda.onet.pl/prognoza-pogody";
                var isEqual = driver.Url.StartsWith(pogodaUrl, StringComparison.OrdinalIgnoreCase);
                Assert.True(isEqual);
            }

        [TearDown]
            public void QuitDriver()
            {
                driver.Quit();
            }
        
    }
}
