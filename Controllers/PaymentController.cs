using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Services;
using supermarketapi.Resources;
using System.Data.SqlClient;
using supermarketapi.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Stripe;
using System.Linq;

namespace supermarketapi.Controllers
{
    [Route("/api/pay")]
    [Produces("application/json")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }



        [HttpPost]
        public IActionResult Post([FromBody]IEnumerable<BasketProduct> basketProducts)
        {
            //var total = "0";
            var paymentIntents = new PaymentIntentService();
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(basketProducts),
                Currency = "gbp",
                Metadata = new Dictionary<string, string>
                {
                    {"OrderId", "6735"},
                },
            });

            //var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            //{
            //    Amount = 1099,
            //    Currency = "gbp",
            //});

            //var myCharge = new ChargeCreateOptions();
            //myCharge.Source = chargeRequest.tokenId;
            ////Always decide how much to charge on the server side, a trusted environment, as opposed to the client.
            //myCharge.Amount = 150;
            //myCharge.Customer = chargeRequest.customer;
            //myCharge.Currency = "gbp";
            //myCharge.Description = chargeRequest.productName;
            //myCharge.Metadata = new Dictionary<string, string>();
            //myCharge.Metadata["OurRef"] = "OurRef-" + Guid.NewGuid().ToString();

            //var service = new ChargeService();
            //Charge stripeCharge = service.Create(myCharge);

            return Json(new { clientSecret = paymentIntent.ClientSecret });

        }

        private int CalculateOrderAmount(IEnumerable<BasketProduct>  basketProducts)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            

            return basketProducts.Sum(x => x.Price);
        }


    }
   
}
