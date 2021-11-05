using OpenQA.Selenium;
using System;

namespace SeleniumTests.StartSetup
{
    class Start
    {
        public static void Setup(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.Manage().Window.Maximize();
        }
    }
}
