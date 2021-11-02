using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursSelenium.Tests.Screenshots
{
    class TakingScreenshots
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl(Url.FakestoreMainPage());
            Button.InfoList(driver).Click();
        }
        [TearDown]
        public void Quit()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)//tylko jak failuje można użyć inne opcje niż Success
            { 
            Console.WriteLine("Screenshot :"+Takescreenshot());
            }
            driver.Quit();
        }
        [Test]
        public void GoToPainPageAndFindTest()
        {

            Assert.DoesNotThrow(()=>driver.FindElement(By.CssSelector("#woocommerce-product-search-field")));
        }
        private string Takescreenshot()
        {
            Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HHmmss");
            string testName = TestContext.CurrentContext.Test.Name;
            string fullPath = @"C:\Selenium w C#\Screenshots\" + testName + " " + dateTime + ".png";
            image.SaveAsFile(fullPath);
            return fullPath;
        }
    }
}
