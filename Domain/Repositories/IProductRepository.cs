using System.Collections.Generic;
using System.Threading.Tasks;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Models.Queries;

namespace supermarketapi.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<IEnumerable<Product>> ListProductsListAsync(int categoryId);
        Task<Product> FindByIdAsync(Product product);
        Task AddAsync(Product product);
        void Update(Product product);
        void Remove(Product product);
        void RemoveList(int categoryId);
        void RemoveAll();
    }
}