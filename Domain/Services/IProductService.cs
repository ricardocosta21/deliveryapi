using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Models.Queries;
using Supermarket.API.Domain.Services.Communication;
using System.Collections.Generic;

namespace Supermarket.API.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<bool> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product, string name);
        Task<bool> DeleteAsync(Product product);
    }
}