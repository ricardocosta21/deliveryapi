using System.Threading.Tasks;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Models.Queries;
using supermarketapi.Domain.Services.Communication;
using System.Collections.Generic;

namespace supermarketapi.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<IEnumerable<Product>> ListProductsListAsync(int categoryId, string clientUID);
        Task<bool> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product, string name);
        Task<bool> DeleteAsync(Product product);
        //Task<bool> DeleteAllAsync();
    }
}