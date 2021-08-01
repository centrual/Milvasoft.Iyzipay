﻿using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class PayWithIyzicoSample : Sample
    {

        [Test]
        public async Task Should_Initialize_PayWithIyzico()
        {
            CreatePayWithIyzicoInitializeRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Price = "1",
                PaidPrice = "1.2",
                Currency = Currency.TRY.ToString(),
                BasketId = "B67832",
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = "https://www.merchant.com/callback"
            };

            List<int> enabledInstallments = new()
            {
                2,
                3,
                6,
                9
            };
            request.EnabledInstallments = enabledInstallments;

            Buyer buyer = new()
            {
                Id = "BY789",
                Name = "John",
                Surname = "Doe",
                GsmNumber = "+905350000000",
                Email = "email@email.com",
                IdentityNumber = "74300864791",
                LastLoginDate = "2015-10-05 12:43:35",
                RegistrationDate = "2013-04-21 15:12:09",
                RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                Ip = "85.34.78.112",
                City = "Istanbul",
                Country = "Turkey",
                ZipCode = "34732"
            };
            request.Buyer = buyer;

            Address shippingAddress = new()
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742"
            };
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new()
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742"
            };
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new();
            BasketItem firstBasketItem = new()
            {
                Id = "BI101",
                Name = "Binocular",
                Category1 = "Collectibles",
                Category2 = "Accessories",
                ItemType = BasketItemType.PHYSICAL.ToString(),
                Price = "0.3"
            };
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new()
            {
                Id = "BI102",
                Name = "Game code",
                Category1 = "Game",
                Category2 = "Online Game Items",
                ItemType = BasketItemType.VIRTUAL.ToString(),
                Price = "0.5"
            };
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new()
            {
                Id = "BI103",
                Name = "Usb",
                Category1 = "Electronics",
                Category2 = "Usb / Cable",
                ItemType = BasketItemType.PHYSICAL.ToString(),
                Price = "0.2"
            };

            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            var payWithIyzicoInitialize = new PayWithIyzicoInitialize(RestHttpClient);

            payWithIyzicoInitialize = await payWithIyzicoInitialize.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(payWithIyzicoInitialize);

            Assert.AreEqual(Status.SUCCESS.ToString(), payWithIyzicoInitialize.Status);
            Assert.AreEqual(Locale.TR.ToString(), payWithIyzicoInitialize.Locale);
            Assert.AreEqual("123456789", payWithIyzicoInitialize.ConversationId);
            Assert.IsNotNull(payWithIyzicoInitialize.SystemTime);
            Assert.IsNull(payWithIyzicoInitialize.ErrorCode);
            Assert.IsNull(payWithIyzicoInitialize.ErrorMessage);
            Assert.IsNull(payWithIyzicoInitialize.ErrorGroup);
            Assert.IsNotNull(payWithIyzicoInitialize.CheckoutFormContent);
            Assert.IsNotNull(payWithIyzicoInitialize.PayWithIyzicoPageUrl);
        }

        [Test]
        public async Task Should_Retrieve_PayWithIyzico_Result()
        {
            RetrievePayWithIyzicoRequest request = new()
            {
                ConversationId = "123456789",
                Token = "cb3f2681-e397-473a-931c-2567fd235627"
            };

            var payWithIyzicoResult = new PayWithIyzico(RestHttpClient);

            payWithIyzicoResult = await payWithIyzicoResult.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(payWithIyzicoResult);

            Assert.AreEqual(Status.SUCCESS.ToString(), payWithIyzicoResult.Status);
            Assert.AreEqual(Locale.TR.ToString(), payWithIyzicoResult.Locale);
            Assert.AreEqual("123456789", payWithIyzicoResult.ConversationId);
            Assert.IsNotNull(payWithIyzicoResult.SystemTime);
            Assert.IsNull(payWithIyzicoResult.ErrorCode);
            Assert.IsNull(payWithIyzicoResult.ErrorMessage);
            Assert.IsNull(payWithIyzicoResult.ErrorGroup);
        }
    }
}
