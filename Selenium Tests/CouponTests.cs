using FakestorePageObjects;
using Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumTests
{
    class CouponTests: BaseTests
    {
        string minimalCoupnValue = "kwotowy300";
        int minimalValueCoupnValue = 300;
        string procentCoupon = "10procent";
        float procentValuCoupon = 0.1f;
        string windsurfingCoupon = "windsurfing350";
        int windsurfingCouponValue = 350;
        string oldCoupon = "starośćnieradość";
        IList<string> productsUrls = new List<string>()
        {
            "/windsurfing-w-lanzarote-costa-teguise/",
            "/product/gran-koscielcow/"
        };
        IList<float> productPrices = new List<float>()
        {
            3000,
            2999.99f,
            350
        };

        [Test]
        public void MinimaValueCouponTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage =  productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().FillInCouponField(minimalCoupnValue).ApplyCoupon();
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Kupon został pomyślnie użyty.", cartPage.CouponMassage.Text, "Coupon was not succesfully apply.");
                Assert.AreEqual("Kupon: " + minimalCoupnValue, cartPage.CartDiscount.Text, "Coupon was not apply.");
                Assert.AreEqual(FormatNumber(productPrices[0] - minimalValueCoupnValue), cartPage.OrderTotalWithoutTextElement.Text, "Total is not correct");
                Assert.AreEqual("300,00 zł", cartPage.CartDiscountValue.Text, "Discount value is not correct.");
            });
        }
        [Test]
        public void ToSmallValueToMinimaValueCouponTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[1]).AddToCart().GoToCart().FillInCouponField(minimalCoupnValue).ApplyCoupon();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, cartPage.ErrorList.Count, " Number of error massages is correct.");
                Assert.AreEqual("Minimalna wartość zamówienia dla tego kuponu to 3 000,00 zł.", cartPage.ErrorList[0].Text, "Coupon was not succesfully apply.");
                CustomAssert.ThrowsWebDriverTimeoutException(() => _ =
                cartPage.CartDiscountRow, "Cart discount element was found in cart summary");

            });
        }
        [Test]
        public void PercentCouponTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[1]).AddToCart().GoToCart().FillInCouponField(procentCoupon).ApplyCoupon();
            float roundDiscount = procentValuCoupon * productPrices[1];
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Kupon został pomyślnie użyty.", cartPage.CouponMassage.Text, "Coupon was not succesfully apply.");
                Assert.AreEqual("Kupon: " + procentCoupon, cartPage.CartDiscount.Text, "Coupon was not apply.");
                Assert.AreEqual(FormatNumber(productPrices[1] - roundDiscount), cartPage.OrderTotalWithoutTextElement.Text, "Total is not correct");
                Assert.AreEqual(FormatNumber(roundDiscount), cartPage.CartDiscountValue.Text, "Discount value is not correct.");
            });
        }
        [Test]
        public void CategoryCouponTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().FillInCouponField(windsurfingCoupon).ApplyCoupon();
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Kupon został pomyślnie użyty.", cartPage.CouponMassage.Text, "Coupon was not succesfully apply.");
                Assert.AreEqual("Kupon: " + windsurfingCoupon, cartPage.CartDiscount.Text, "Coupon was not apply.");
                Assert.AreEqual(FormatNumber(productPrices[0] - windsurfingCouponValue), cartPage.OrderTotalWithoutTextElement.Text, "Total is not correct");
                Assert.AreEqual(FormatNumber(windsurfingCouponValue), cartPage.CartDiscountValue.Text, "Discount value is not correct.");
            });
        }
        [Test]
        public void WrongCategoryCouponTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[1]).AddToCart().GoToCart().FillInCouponField(windsurfingCoupon).ApplyCoupon();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, cartPage.ErrorList.Count, " Number of error massages is correct.");
                Assert.AreEqual("Przepraszamy, tego kuponu nie można zastosować do wybranych produktów.", cartPage.ErrorList[0].Text, "Coupon was not succesfully apply.");
                CustomAssert.ThrowsWebDriverTimeoutException(() => _ =
                cartPage.CartDiscountRow, "Cart discount element was found in cart summary");
            });
        }
        [Test]
        public void OldCouponTest()
        {
            ProductPage productPage = new ProductPage(driver);
            CartPage cartPage = productPage.GoTo(productsUrls[1]).AddToCart().GoToCart().FillInCouponField(oldCoupon).ApplyCoupon();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, cartPage.ErrorList.Count, " Number of error massages is correct.");
                Assert.AreEqual("Ten kupon stracił ważność.", cartPage.ErrorList[0].Text, "Coupon was not succesfully apply.");
                CustomAssert.ThrowsWebDriverTimeoutException(() => _ =
                cartPage.CartDiscountRow, "Cart discount element was found in cart summary");
            });
        }
        private string FormatNumber(float number)
        {
            if (number < 1000)
            {
                return string.Format("{0:###.00}", number) + " zł";
            }
            else return string.Format("{0:### ###.00}", number) + " zł";
        }

    }

}
