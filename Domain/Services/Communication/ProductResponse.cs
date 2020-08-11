using supermarketapi.Domain.Models;

namespace supermarketapi.Domain.Services.Communication
{
    public class ProductResponse : BaseResponse<Product>
    {
        public ProductResponse(Product product) : base(product) { }

        public ProductResponse(string message) : base(message) { }
    }
}