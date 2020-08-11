using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Repositories;
using supermarketapi.Persistence.Contexts;

namespace supermarketapi.Domain.Repositories
{
    public interface IBasketProductRepository
    {
        Task<IEnumerable<BasketProduct>> ListAsync();
        Task<IEnumerable<BasketProduct>> ListAsync(string clientUID);
        //Task<BasketProduct> ListAsync(int clientId);
        //Task<BasketProduct> ListAsyncId(int id);
        void Add(BasketProduct bProduct);
        void Remove(BasketProduct bProduct);

        Task<BasketProduct> FindByIdAsync(int id);

        //void Update(Category category);

        //void RemoveAll();
    }
}
