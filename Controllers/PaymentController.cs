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
    [Route("/api/payment")]
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
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public JsonResult Post([FromBody] StripePayment paymentRequest)
        {
            var myCharge = new ChargeCreateOptions();
            myCharge.Source = paymentRequest.tokenId;
            //Always decide how much to charge on the server side, a trusted environment, as opposed to the client.
            myCharge.Amount = 150;
            myCharge.Customer = paymentRequest.customer;
            myCharge.Currency = "gbp";
            myCharge.Description = paymentRequest.productName;
            myCharge.Metadata = new Dictionary<string, string>();
            myCharge.Metadata["OurRef"] = "OurRef-" + Guid.NewGuid().ToString();

            var service = new ChargeService();
            Charge stripeCharge = service.Create(myCharge);

            return Json(stripeCharge);
        }


    }
   
}
