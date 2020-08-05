using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services.Communication;

namespace Supermarket.API.Domain.Services
{
    public interface ICategoryService
    {
        //Task<List<Category>> ListAsync();
         Task<IEnumerable<Category>> ListAsync();
         Task<Category> ListItemAsync(int id);
         Task<bool> AddAsync(Category category);
         Task<bool> UpdateAsync(Category category, string newName);
         Task<bool> DeleteAsync(int id);
         Task<bool> DeleteAllAsync();
    }
}