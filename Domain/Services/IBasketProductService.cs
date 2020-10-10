using System.Collections.Generic;
using System.Threading.Tasks;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Services.Communication;

namespace supermarketapi.Domain.Services
{
    public interface IBasketProductService
    {
        Task<IEnumerable<BasketProduct>> ListAsync();
        Task<IEnumerable<BasketProduct>> ListAsync(string clientUID);
        //Task<BasketProduct> ListAsync(int clientId);

        //Task<BasketProduct> ListAsync();
        //Task<Product> ListBasketProductsAsync(int clientId);
        Task<bool> AddAsync(BasketProduct bProduct);

        //Task<bool> IncrementAsync(int bProductId);

        Task<bool> DecrementAsync(int bProductId);

        Task<bool> Remove(int bProductId);
        //Task<bool> UpdateAsync(Category category, string newName);
        //Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAllAsync(string clientUID);

    }
}