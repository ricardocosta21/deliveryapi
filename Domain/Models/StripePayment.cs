using System;

namespace Supermarket.API.Domain.Models
{
    public class StripePayment
    {
        public string customer { get; set; }
        public string tokenId { get; set; }
        public string productName { get; set; }
        public int amount { get; set; }
    }
}
