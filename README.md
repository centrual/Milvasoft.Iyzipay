# Milvasoft.Iyzipay Library For .Net 5

[![NuGet](https://img.shields.io/nuget/v/Milvasoft.Iyzipay)](https://www.nuget.org/packages/Milvasoft.Iyzipay/)

[![NuGet](https://img.shields.io/nuget/dt/Milvasoft.Iyzipay)](https://www.nuget.org/packages/Milvasoft.Iyzipay/)

Unofficial Iyzipay client library that is maintained by Milvasoft, a fork of the iyzipay-dotnet

All methods have been rearranged to use async await.

The dispose pattern has been implemented in the class where the request was sent. This class can be disposed according to optional usage. The HttpRequestMessage and HttpResponse message objects created in each request were disposed of as a result of the request.

Required dependency injection operations in Iyzico API integration have been adapted to the .net core structure.

Supports .Net 5.0

You can sign up for an iyzico account at https://iyzico.com

# Requirements
One of the runtime environment is required from below
* .NET 5.0

# Installation

For now you'll need to install following libraries:

* To install Milvasoft.Iyzipay, run the following command in the Package Manager Console
```
Install-Package Milvasoft.Iyzipay
```
 Or you can download the latest .dll from:  https://github.com/Milvasoft/Milvasoft.Iyzipay

# Milvasoft.Iyzipay Usage

In Startup.cs;

```csharp 1

...
	    
 services.AddIyzicoIntegration(i =>
 {
     i.ApiKey = "your api key";
     i.SecretKey = "your secret key";
     i.BaseUrl = "https://sandbox-api.iyzipay.com";
     i.RestHttpClientLifeTime = ServiceLifetime.Transient;
     i.RestHttpClientV2LifeTime = ServiceLifetime.Transient;
 });

...

```

```csharp 1
        
using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;

private readonly IRestHttpClient _restHttpClient;

public PaymentController(IRestHttpClient restHttpClient)
{
    _restHttpClient = restHttpClient;
}

public async Task<IActionResult> CancelPaymentAsync()
{
    CreateCancelRequest request = new()
    {
        ConversationId = "123456789",
        Locale = Locale.TR.ToString(),
        PaymentId = "1",
        Ip = "85.34.78.112"
    };
    
    var cancel = new Cancel(_restHttpClient);
    
    cancel = await cancel.CreateAsync(request).ConfigureAwait(false);
    
    _restHttpClient.Dispose();
    
    if (cancel.Status == Status.SUCCESS.ToString())
        return Ok();
    else
        return BadRequest();
}

```

Or you can use like official Iyzipay library;

```csharp

IOptions options = new Options
{
   ApiKey = "your api key",
   SecretKey = "your secret key",
   BaseUrl = "https://sandbox-api.iyzipay.com"
}
		
CreatePaymentRequest request = new();
request.Locale = Locale.TR.ToString();
request.ConversationId = "123456789";
request.Price = "1";
request.PaidPrice = "1.2";
request.Currency = Currency.TRY.ToString();
request.Installment = 1;
request.BasketId = "B67832";
request.PaymentChannel = PaymentChannel.WEB.ToString();
request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

PaymentCard paymentCard = new();
paymentCard.CardHolderName = "John Doe";
paymentCard.CardNumber = "5528790000000008";
paymentCard.ExpireMonth = "12";
paymentCard.ExpireYear = "2030";
paymentCard.Cvc = "123";
paymentCard.RegisterCard = 0;
request.PaymentCard = paymentCard;

Buyer buyer = new();
buyer.Id = "BY789";
buyer.Name = "John";
buyer.Surname = "Doe";
buyer.GsmNumber = "+905350000000";
buyer.Email = "email@email.com";
buyer.IdentityNumber = "74300864791";
buyer.LastLoginDate = "2015-10-05 12:43:35";
buyer.RegistrationDate = "2013-04-21 15:12:09";
buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
buyer.Ip = "85.34.78.112";
buyer.City = "Istanbul";
buyer.Country = "Turkey";
buyer.ZipCode = "34732";
request.Buyer = buyer;

Address shippingAddress = new();
shippingAddress.ContactName = "Jane Doe";
shippingAddress.City = "Istanbul";
shippingAddress.Country = "Turkey";
shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
shippingAddress.ZipCode = "34742";
request.ShippingAddress = shippingAddress;

Address billingAddress = new();
billingAddress.ContactName = "Jane Doe";
billingAddress.City = "Istanbul";
billingAddress.Country = "Turkey";
billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
billingAddress.ZipCode = "34742";
request.BillingAddress = billingAddress;

List<BasketItem> basketItems = new();
BasketItem firstBasketItem = new();
firstBasketItem.Id = "BI101";
firstBasketItem.Name = "Binocular";
firstBasketItem.Category1 = "Collectibles";
firstBasketItem.Category2 = "Accessories";
firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
firstBasketItem.Price = "0.3";
basketItems.Add(firstBasketItem);

BasketItem secondBasketItem = new();
secondBasketItem.Id = "BI102";
secondBasketItem.Name = "Game code";
secondBasketItem.Category1 = "Game";
secondBasketItem.Category2 = "Online Game Items";
secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
secondBasketItem.Price = "0.5";
basketItems.Add(secondBasketItem);

BasketItem thirdBasketItem = new();
thirdBasketItem.Id = "BI103";
thirdBasketItem.Name = "Usb";
thirdBasketItem.Category1 = "Electronics";
thirdBasketItem.Category2 = "Usb / Cable";
thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
thirdBasketItem.Price = "0.2";
basketItems.Add(thirdBasketItem);
request.BasketItems = basketItems;

var client = new RequestHttpClient(new HttpClient(),options);

var payment = new Payment(client);

payment = await payment.CreateAsync(request).ConfigureAwait(false);

client.Dispose();

```
See other samples under Milvasoft.Iyzipay.Samples project.

# Testing

You can run particular sample by passing your credential info to "Milvasoft.Iyzipay.Samples/Sample.cs"

### Mock test cards

Test cards that can be used to simulate a *successful* payment:

Card Number      | Bank                       | Card Type
-----------      | ----                       | ---------
5890040000000016 | Akbank                     | Master Card (Debit)  
5526080000000006 | Akbank                     | Master Card (Credit)  
4766620000000001 | Denizbank                  | Visa (Debit)  
4603450000000000 | Denizbank                  | Visa (Credit)
4729150000000005 | Denizbank Bonus            | Visa (Credit)  
4987490000000002 | Finansbank                 | Visa (Debit)  
5311570000000005 | Finansbank                 | Master Card (Credit)  
9792020000000001 | Finansbank                 | Troy (Debit)  
9792030000000000 | Finansbank                 | Troy (Credit)  
5170410000000004 | Garanti Bankası            | Master Card (Debit)  
5400360000000003 | Garanti Bankası            | Master Card (Credit)  
374427000000003  | Garanti Bankası            | American Express  
4475050000000003 | Halkbank                   | Visa (Debit)  
5528790000000008 | Halkbank                   | Master Card (Credit)  
4059030000000009 | HSBC Bank                  | Visa (Debit)  
5504720000000003 | HSBC Bank                  | Master Card (Credit)  
5892830000000000 | Türkiye İş Bankası         | Master Card (Debit)  
4543590000000006 | Türkiye İş Bankası         | Visa (Credit)  
4910050000000006 | Vakıfbank                  | Visa (Debit)  
4157920000000002 | Vakıfbank                  | Visa (Credit)  
5168880000000002 | Yapı ve Kredi Bankası      | Master Card (Debit)  
5451030000000000 | Yapı ve Kredi Bankası      | Master Card (Credit)  

*Cross border* test cards:

Card Number      | Country
-----------      | -------
4054180000000007 | Non-Turkish (Debit)
5400010000000004 | Non-Turkish (Credit)    

Test cards to get specific *error* codes:

Card Number       | Description
-----------       | -----------
5406670000000009  | Success but cannot be cancelled, refund or post auth
4111111111111129  | Not sufficient funds
4129111111111111  | Do not honour
4128111111111112  | Invalid transaction
4127111111111113  | Lost card
4126111111111114  | Stolen card
4125111111111115  | Expired card
4124111111111116  | Invalid cvc2
4123111111111117  | Not permitted to card holder
4122111111111118  | Not permitted to terminal
4121111111111119  | Fraud suspect
4120111111111110  | Pickup card
4130111111111118  | General error
4131111111111117  | Success but mdStatus is 0
4141111111111115  | Success but mdStatus is 4
4151111111111112  | 3dsecure initialize failed
