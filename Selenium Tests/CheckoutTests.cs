using FakestorePageObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumTests
{
    class CheckoutTests : BaseTests
    {

        string cardNumber = "4242424242424242";
        string cardExpirationDate = "0222";
        string cardCvc = "123";
        IList<string> productsUrls = new List<string>()
            {
                "/product/wyspy-zielonego-przyladka-sal/",
                "/product/zmien-swoja-sylwetke-yoga-na-malcie/"
            };
        IList<string> productPriceText = new List<string>()
            {
                "5 399,00 zł",
                "3 299,00 zł"
            };
        IList<int> productPrices = new List<int>()
            {
                5399,
                3299
             };

        [Test]
        public void FieldsValidationTest()
        {

            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CheckoutPage checkoutPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().GoToCheckout().FillInCardData(cardNumber, cardExpirationDate, cardCvc).PlaceOrder<CheckoutPage>();
            IList<string> errorList = checkoutPage.ErrorMessagesList.Select(element => element.Text).ToList();
            IList<string> expectedList = new List<string>{
                "Imię płatnika jest wymaganym polem." ,
                "Nazwisko płatnika jest wymaganym polem.",
                "Ulica płatnika jest wymaganym polem.",
                 "Kod pocztowy płatnika nie jest prawidłowym kodem pocztowym.",
                 "Miasto płatnika jest wymaganym polem.",
                "Telefon płatnika jest wymaganym polem.",
                "Adres email płatnika jest wymaganym polem.",
                 "Proszę przeczytać i zaakceptować regulamin sklepu aby móc sfinalizować zamówienie."
            };
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _ = checkoutPage.ErrorList, "Error list was not found");
                Assert.AreEqual(errorList.OrderBy(element => element), errorList.OrderBy(element => element), "Inccorect massages.");
            });
        }
        [Test]
        public void ReviewOrderOneProductTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CheckoutPage checkoutPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().GoToCheckout();
            float tax = CalculateTax(productPrices[0]);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(productPriceText[0], checkoutPage.ProductsTotalElement.Text, "Incorrect price .");
                Assert.AreEqual(productPriceText[0], checkoutPage.CartSubtotalElement.Text, "Incorrect price.");
                Assert.AreEqual(productPriceText[0] + " (zawiera " + FormatNumber(tax) + " VAT)", checkoutPage.OrderTotalElement.Text, "Incorrect vat.");
            });
        }
        [Test]
        public void ReviewOrderTwoProductTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CheckoutPage checkoutPage = productPage.GoTo(productsUrls[0]).AddToCart(2).GoTo(productsUrls[1]).AddToCart(3).GoToCart().GoToCheckout();

            float price = productPrices[0] * 2 + productPrices[1] * 3;
            Assert.Multiple(() =>
            {
                Assert.AreEqual(FormatNumber(productPrices[0] * 2), checkoutPage.ProductsTotalElements[0].Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(productPrices[0] * 2), checkoutPage.ProductsTotalElements[0].Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(productPrices[1] * 3), checkoutPage.ProductsTotalElements[1].Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(price), checkoutPage.CartSubtotalElement.Text, "Incorrect price.");
                Assert.AreEqual(FormatNumber(price) + " (zawiera " + FormatNumber(CalculateTax(price)) + " VAT)", checkoutPage.OrderTotalElement.Text, "Incorrect vat.");
            });
        }

        [Test]
        public void ChangeNumberOfItemsTest()
        {

            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CheckoutPage checkoutPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().ChangeQuantity(2).GoToCheckout();

            float tax = CalculateTax(productPrices[0] * 2);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(FormatNumber(productPrices[0] * 2), checkoutPage.ProductsTotalElement.Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(productPrices[0] * 2), checkoutPage.CartSubtotalElement.Text, "Incorrect price.");
                Assert.AreEqual(FormatNumber(productPrices[0] * 2) + " (zawiera " + FormatNumber(tax) + " VAT)", checkoutPage.OrderTotalElement.Text, "Incorrect vat.");
            });
        }
        [Test]
        public void SuccesfulPaymentTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            SuccesfullOrderPage sucessfullOrderPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().GoToCheckout().FillForm("Katarzyna", "Palusik", "CC", "Toszecka", "44-100", "Gliwice", "123-456-789", "test@t.op").
            FillInCardData(cardNumber, cardExpirationDate, cardCvc).
            CheckReadCheckbox().
            PlaceOrder<SuccesfullOrderPage>();
            Assert.AreEqual("Zamówienie otrzymane", sucessfullOrderPage.OrderReciveMesseage.Text, "Inccorect header.");
        }
        [Test]
        public void SuccesfulPaymentExistingUserTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            SuccesfullOrderPage sucessfullOrderPage = productPage.GoTo(productsUrls[0]).AddToCart().GoToCart().GoToCheckout().OpenLogInForm().FilLogInFields("katarzyna.palusik", "$AdminAdmin123").
            FillInCardData(cardNumber, cardExpirationDate, cardCvc).
            CheckReadCheckbox().
            PlaceOrder<SuccesfullOrderPage>();
            Assert.AreEqual("Zamówienie otrzymane", sucessfullOrderPage.OrderReciveMesseage.Text, "Inccorect header.");
        }
        private float CalculateTax(float productPrice)
        {
            return (float)Math.Round(productPrice - (productPrice / 1.23), 2);
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
