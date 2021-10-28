using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace KursSelenium.WebTerminal
{
    class AspenWebTerminal
    {

        IWebDriver driver;

        [SetUp]

        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            driver = new ChromeDriver(chromeOptions);

            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().PageLoad = System.TimeSpan.FromSeconds(30);
        }

        [Test]
        public void LoadingPage()
        {
            Uri webTerminalUrl = new Uri("http://10.196.53.93/aspentech/aspenprocesspulse/");
            driver.Navigate().GoToUrl(webTerminalUrl);
            driver.Manage().Window.Maximize();
            string webTerminalTiitle = "Aspen Process Pulse - Web Terminal";
            Assert.AreEqual(webTerminalTiitle, driver.Title, "Title is not correct");
        }
        [Test]
        public void LogIn()
        {
            LoadingPage();
            driver.FindElement(By.Id("Username")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("admin1");
            driver.FindElement(By.XPath("//input[@class='btn btn-primary']")).Click();
            IWebElement logoutButton = driver.FindElement(By.XPath("//ul[@class='nav navbar-nav navbar-right']"));
            Assert.AreEqual(true, logoutButton.Displayed);

        }

        [Test]

        public void Logout()
        {
            LogIn();
            driver.FindElement(By.XPath("//ul[@class='nav navbar-nav navbar-right']")).Click();
            IWebElement logInButton = driver.FindElement(By.XPath("//input[@class='btn btn-primary']"));
            Assert.AreEqual(true, logInButton.Displayed);
        }

        [Test]

        public void ConfigurationStart()
        {
            LogIn();
            string configurationName = "opcda no model";
            IWebElement configName = driver.FindElement(By.XPath("//p[@title='opcda no model']"));
            Assert.AreEqual(configName.Text, configurationName, "Configuration does not exist");
            driver.FindElement(By.XPath("//a[@class='btn btn-default']")).Click();
            IWebElement startButton = driver.FindElement(By.XPath("//input[@value='Start']"));
            Assert.IsTrue(startButton.Enabled);
            IWebElement experimentID = driver.FindElement(By.XPath("//input[@name='Experiment ID']"));
            experimentID.Clear();
            experimentID.SendKeys("run configuration");
            startButton.Click();
            IWebElement showButton = driver.FindElement(By.XPath("//a[@class='btn btn-primary']"));
            Assert.AreEqual(true, showButton.Enabled);
        }

        [Test]

        public void ShowConfiguration()
        {
            ConfigurationStart();
            driver.FindElement(By.XPath("//a[@class='btn btn-primary']")).Click();
            IWebElement plotArea = driver.FindElement(By.XPath("//div[@class='row']"));
            Assert.IsTrue(plotArea.Displayed);
        }

        [Test]

        public void AddFlag()
        {
            ShowConfiguration();
            Actions action = new Actions(driver);
            action.MoveByOffset(600, 300).Perform();
            action.ContextClick().Perform();
            driver.FindElement(By.XPath("//a[@id='addFlagMenuItem']")).Click();
            IWebElement chooseFlag = driver.FindElement(By.XPath("//a[@class='chosen-single chosen-default']"));
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].Click()", chooseFlag);

        }
        [TearDown]
        public void QuitDriver()
        {
            driver.Quit();
        }
    }
}
