using Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FakestorePageObjects
{
    public class CartPage : BasePage
    {

        private string CartUrl => baseUrl + "/koszyk";

        public IList<IWebElement> CartItems
        {
            get
            {
                _ = CartTable;
                return driver.FindElements(By.CssSelector("tr.cart_item"), 5);
            }
        }
        public IList<string> ItemIDs => CartItems.Select(element => element.FindElement(By.CssSelector("a")).GetAttribute("data-product_id")).ToList();
        private IWebElement CartTable => driver.FindElement(By.CssSelector("table.shop_table.cart"), 2);
        private IWebElement UpdateCart => driver.FindElement(By.CssSelector("button[name='update_cart']"), 2);
        private IWebElement GoToCheckoutButton => driver.FindElement(By.CssSelector("a.checkout-button"), 2);
        private IWebElement CouponCodeField => driver.FindElement(By.CssSelector("#coupon_code"), 2);
        public IWebElement CouponMassage => driver.FindElement(By.CssSelector("div.woocommerce-message"), 2);
        private IWebElement ApplyCouponButton => driver.FindElement(By.CssSelector("button[name='apply_coupon']"), 2);
        public IWebElement CartDiscount => driver.FindElement(By.CssSelector("tr.cart-discount th"), 2);
        public IWebElement CartDiscountRow => driver.FindElement(By.CssSelector("tr.cart-discount"), 2);
        public IWebElement CartDiscountValue => driver.FindElement(By.CssSelector("tr.cart-discount span"), 2);
        public IWebElement OrderTotalWithoutTextElement => driver.FindElement(By.CssSelector("tr.order-total td strong"), 2);
        public IList<IWebElement> ErrorList => driver.FindElements(By.CssSelector("ul.woocommerce-error li"), 2);

        public IWebElement QuantityField
        {
            get
            {
                _ = CartTable;
                return driver.FindElement(By.CssSelector(".qty"), 2);
            }
        }

        public IWebElement CartEmptyMessage => driver.FindElement(By.CssSelector(".cart-empty.woocommerce-info"));

        public CartPage ApplyCoupon()
        {
            ApplyCouponButton.Click();
            return this;
        }

        public CartPage(IWebDriver driver) : base(driver) { }


        public CartPage GoTo()
        {
            driver.Navigate().GoToUrl(CartUrl);
            return this;
        }

        public CartPage RemoveItem(string productId)
        {
            driver.FindElement(By.CssSelector("a[data-product_id='" + productId + "']")).Click();
            WaitForLoadersDisappear();
            return this;
        }

        public CartPage ChangeQuantity(int quantity = 1)
        {
            QuantityField.Clear();
            QuantityField.SendKeys(quantity.ToString());
            UpdateCart.Click();
            WaitForLoadersDisappear();
            return this;
        }

        public CheckoutPage GoToCheckout()
        {
            GoToCheckoutButton.Click();
            return new CheckoutPage(driver);
        }

        public CartPage FillInCouponField(string coupnValue)
        {
            CouponCodeField.SendKeys(coupnValue);
            return this;
        }
    }
}