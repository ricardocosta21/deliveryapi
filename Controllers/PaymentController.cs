using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services;
using Supermarket.API.Resources;
using System.Data.SqlClient;
using Supermarket.API.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace supermarketapi.Controllers
{
    [Route("/api/create-payment-intent")]
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

        public IActionResult Index()
        {
            return View();
        }

        

        [HttpPost]
        //public IActionResult Post([FromBody] StripeCharge request)
        public IActionResult Post()
        {
            var total = "0";
            var paymentIntents = new PaymentIntentService();
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(total),
                Currency = "gbp",
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

        private int CalculateOrderAmount(string items)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            return 1400;
        }


    }
   
}
