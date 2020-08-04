using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Models.Queries;

namespace Supermarket.API.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<IEnumerable<Product>> ListProductsListAsync(int categoryId);
        Task<Product> FindByIdAsync(Product product);
        Task AddAsync(Product product);
        void Update(Product product);
        void Remove(Product product);
    }
}