using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakestorePageObjects
{
    public class DismissNoticeSection : BasePage
    {
        public DismissNoticeSection(RemoteWebDriver driver) : base(driver)  { }
        private IWebElement DismissLink => driver.FindElement(By.CssSelector(".woocommerce-store-notice__dismiss-link"));
        public void Dismiss()
        {
           DismissLink.Click();
        }
    }

}
