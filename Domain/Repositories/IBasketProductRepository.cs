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

        void Add(BasketProduct bProduct);
        void Update(BasketProduct bProduct);
        void Remove(BasketProduct bProduct);

        Task<BasketProduct> FindByIdAsync(int id);

        Task<bool> RemoveAll(string clientUID);
    }
}
