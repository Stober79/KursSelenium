using OpenQA.Selenium;

namespace KursSelenium.Element
{
    class Link
    {
        public static IWebElement RegisterPassword(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("#p.lost_password"));
            return element;
        }
        public static IWebElement PrivatePolicy(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("a.woocommerce-privacy-policy-link"));
            return element;
        }
        public static IWebElement MainPage(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("li.menu-item-197 a"));
            return element;
        }
        public static IWebElement MainPageCrumbs(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("nav.woocommerce-breadcrumb a"));
            return element;
        }
        public static IWebElement Shop(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("li.menu-item-198 a"));
            return element;
        }

        public static IWebElement Cart(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("li.menu-item-200 a"));
            return element;
        }
        public static IWebElement Order(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("li.menu-item-199 a"));
            return element;
        }
        public static IWebElement MyAccount(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("li.menu-item-201 a"));
            return element;
        }
        public static IWebElement WishList(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("li.menu-item-202 a"));
            return element;
        }
        public static IWebElement ProductCategories_Windsurfing(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("li.cat-item-18"));
            return element;
        }

        public static IWebElement ProductCategories_Climbing(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("li.cat-item-16"));
            return element;
        }
        public static IWebElement ProductCategories_YougaAndPilates(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("li.cat-item-19"));
            return element;
        }
        public static IWebElement ProductCategories_Sailing(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("li.cat-item-17"));
            return element;
        }
        public static IWebElement Documentation(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("div.textwidget.custom-html-widget a"));
            return element;
        }
        public static IWebElement PrivacePolicyBootomPageLing(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("a.privacy-policy-link"));
            return element;
        }
        public static IWebElement BuildUsing(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("div.site-info a:last-child"));
            return element;
        }
        public static IWebElement CartContent(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("ul#site-header-cart a"));
            return element;
        }
        public static IWebElement GoToPayment(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.CssSelector("div.cart_totals  a"));
            return element;
        }
    }
}
