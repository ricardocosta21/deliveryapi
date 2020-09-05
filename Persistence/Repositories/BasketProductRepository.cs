using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Repositories;
using supermarketapi.Persistence.Contexts;
using System.Linq;

namespace supermarketapi.Persistence.Repositories
{
    public class BasketProductRepository : BaseRepository, IBasketProductRepository
    {
        public BasketProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<BasketProduct>> ListAsync()
        {
            //return await _context.Categories
            //                     .AsNoTracking()
            //                     .ToListAsync();

            return await _context.BasketProducts.ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task<IEnumerable<BasketProduct>> ListAsync(string clientUID)
        {
            //return await _context.Categories
            //                     .AsNoTracking()
            //                     .ToListAsync();

            return await _context.BasketProducts.Where(x => x.ClientUID == clientUID).ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        //public async Task<BasketProduct> ListAsyncId(int id)
        //{
        //    return await _context.BasketProduct.FirstOrDefaultAsync(x => x.Id == id);
        //}

        public void Add(BasketProduct bProduct)
        {
            _context.BasketProducts.AddAsync(bProduct);
        }

        public async Task<BasketProduct> FindByIdAsync(int id)
        {
            return await _context.BasketProducts.FindAsync(id);
        }

        public void Update(BasketProduct bProduct)
        {
            _context.BasketProducts.Update(bProduct);
        }

        public void Remove(BasketProduct bProduct)
        {
            _context.BasketProducts.Remove(bProduct);
        }

        //public void Remove(Category basket)
        //{
        //    _context.BasketProduct.Remove(basket);
        //}

        //public void RemoveAll()
        //{
        //    foreach (var basket in _context.Categories)
        //    {
        //        _context.BasketProduct.Remove(basket);
        //    }
        //}
    }
}