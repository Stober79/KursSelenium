using KursSelenium.Element;
using KursSelenium.StartSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;

namespace KursSelenium.Advance.AdvenceJavaScript
{
    class JavaScript
    {
        IWebDriver driver;
        IJavaScriptExecutor jse;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            Start.Setup(driver);
            driver.Navigate().GoToUrl(Url.FakestoreFuerteventuraSotavento());
            Button.InfoList(driver).Click();
            jse = (IJavaScriptExecutor)driver;
        }
        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
        [Test]
        public void ClicButtonTest()
        {
            jse.ExecuteScript("console.log('test')");//console.log(;jfgjkd') to Jscript wyświtlający w konsoli podany tekst
            string tekst = (string)jse.ExecuteScript("return 'To tylko tekst'");//zwaraca tekst potrzebne rzutowanie
            IWebElement adToCartBUtton = driver.FindElement(By.CssSelector("[name='add-to-cart']"));
            jse.ExecuteScript("arguments[0].click()", adToCartBUtton);//arguments[0] to podajemy w ktory argument podany w liście ma kliknąć

        }
        [Test]
        public void AsyncScriptTest()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            jse.ExecuteAsyncScript("window.setTimeout(arguments[arguments.length - 1], 500);");
            stopwatch.Stop();
            Console.WriteLine("Elapsed time: " + stopwatch.ElapsedMilliseconds);

        }
    }
}
