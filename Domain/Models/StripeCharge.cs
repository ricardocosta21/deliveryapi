using System;

namespace Supermarket.API.Domain.Models
{
    public class StripeCharge
    {
        public int Id { get; set; }
        public string productName { get; set; }
    }
}
