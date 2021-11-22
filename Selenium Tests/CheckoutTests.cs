using FakestorePageObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumTests
{
    class CheckoutTests : BaseTests
    {

        string CardNumber => testData.Card.Number;
        string CardExpirationDate => testData.Card.ExpirationDate;
        string CardCvc => testData.Card.Cvc;
        IList<string> ProductsUrls => new List<string>()
            {
            testData.Products[0].Url,
            testData.Products[1].Url
            };
        IList<string> ProductPriceText => new List<string>()
            {
            testData.Products[0].PriceText,
            testData.Products[1].PriceText
            };
        IList<int> ProductPrices => new List<int>()
            {
            testData.Products[0].Price,
            testData.Products[1].Price
            } ;
 

        [Test]
        public void FieldsValidationTest()
        {
            TestData.ValidationMessages messages = testData.ValidationMessages;
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CheckoutPage checkoutPage = productPage.GoTo(ProductsUrls[0]).AddToCart().GoToCart().GoToCheckout().FillInCardData(CardNumber, CardExpirationDate, CardCvc).PlaceOrder<CheckoutPage>();
            IList<string> errorList = checkoutPage.ErrorMessagesList.Select(element => element.Text).ToList();
            IList<string> expectedList = new List<string>{
                messages.Name,
                messages.Lastname,
                messages.Address,
                messages.Postcode,
                messages.City,
                messages.Phone,
                messages.Email,
                messages.Terms
            };
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _ = checkoutPage.ErrorList, "Error list was not found");
                Assert.AreEqual(expectedList.OrderBy(element => element), errorList.OrderBy(element => element), "Inccorect massages.");
            });
        }
        [Test]
        public void ReviewOrderOneProductTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CheckoutPage checkoutPage = productPage.GoTo(ProductsUrls[0]).AddToCart().GoToCart().GoToCheckout();
            float tax = CalculateTax(ProductPrices[0]);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProductPriceText[0], checkoutPage.ProductsTotalElement.Text, "Incorrect price .");
                Assert.AreEqual(ProductPriceText[0], checkoutPage.CartSubtotalElement.Text, "Incorrect price.");
                Assert.AreEqual(ProductPriceText[0] + " (zawiera " + FormatNumber(tax) + " VAT)", checkoutPage.OrderTotalElement.Text, "Incorrect vat.");
            });
        }
        [Test]
        public void ReviewOrderTwoProductTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CheckoutPage checkoutPage = productPage.GoTo(ProductsUrls[0]).AddToCart(2).GoTo(ProductsUrls[1]).AddToCart(3).GoToCart().GoToCheckout();

            float price = ProductPrices[0] * 2 + ProductPrices[1] * 3;
            Assert.Multiple(() =>
            {
                Assert.AreEqual(FormatNumber(ProductPrices[0] * 2), checkoutPage.ProductsTotalElements[0].Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(ProductPrices[0] * 2), checkoutPage.ProductsTotalElements[0].Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(ProductPrices[1] * 3), checkoutPage.ProductsTotalElements[1].Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(price), checkoutPage.CartSubtotalElement.Text, "Incorrect price.");
                Assert.AreEqual(FormatNumber(price) + " (zawiera " + FormatNumber(CalculateTax(price)) + " VAT)", checkoutPage.OrderTotalElement.Text, "Incorrect vat.");
            });
        }

        [Test]
        public void ChangeNumberOfItemsTest()
        {

            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            CheckoutPage checkoutPage = productPage.GoTo(ProductsUrls[0]).AddToCart().GoToCart().ChangeQuantity(2).GoToCheckout();

            float tax = CalculateTax(ProductPrices[0] * 2);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(FormatNumber(ProductPrices[0] * 2), checkoutPage.ProductsTotalElement.Text, "Incorrect price .");
                Assert.AreEqual(FormatNumber(ProductPrices[0] * 2), checkoutPage.CartSubtotalElement.Text, "Incorrect price.");
                Assert.AreEqual(FormatNumber(ProductPrices[0] * 2) + " (zawiera " + FormatNumber(tax) + " VAT)", checkoutPage.OrderTotalElement.Text, "Incorrect vat.");
            });
        }
        [Test]
        public void SuccesfulPaymentTest()
        {
            TestData.Customer customer = testData.Customer;
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            SuccesfullOrderPage sucessfullOrderPage = productPage.GoTo(ProductsUrls[0]).AddToCart().GoToCart().GoToCheckout().
                FillForm(customer.Name, customer.LastName,customer.Company,customer.Address,customer.Postcode, customer.City, customer.Phone, customer.Email).
            FillInCardData(CardNumber, CardExpirationDate, CardCvc).
            CheckReadCheckbox().
            PlaceOrder<SuccesfullOrderPage>();
            Assert.AreEqual("Zamówienie otrzymane", sucessfullOrderPage.OrderReciveMesseage.Text, "Inccorect header.");
        }
        [Test]
        public void SuccesfulPaymentExistingUserTest()
        {
            ProductPage productPage = new ProductPage(driver, config.BaseUrl);
            SuccesfullOrderPage sucessfullOrderPage = productPage.GoTo(ProductsUrls[0]).AddToCart().GoToCart().GoToCheckout().OpenLogInForm().FilLogInFields("katarzyna.palusik", "$AdminAdmin123").
            FillInCardData(CardNumber, CardExpirationDate, CardCvc).
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
