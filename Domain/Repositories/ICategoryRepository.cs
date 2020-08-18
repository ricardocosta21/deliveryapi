using System.Collections.Generic;
using System.Threading.Tasks;
using supermarketapi.Domain.Models;

namespace supermarketapi.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListAsync();
        Task<IEnumerable<Category>> ListAsync(string clientUID);
        Task<Category> ListAsyncId(int id);
        Task AddAsync(Category category);
        Task<Category> FindByIdAsync(int id);
        void Update(Category category);
        void Remove(Category category);
        //void RemoveAll();
    }
}