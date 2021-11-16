using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakestorePageObjects
{
    public class SuccesfullOrderPage
    {
        private RemoteWebDriver driver;
        public  IWebElement OrderReciveMesseage => driver.FindElement(By.CssSelector("h1.entry-title"));
        public SuccesfullOrderPage(RemoteWebDriver driver)
        {
        this.driver = driver;
        }
    }
}
