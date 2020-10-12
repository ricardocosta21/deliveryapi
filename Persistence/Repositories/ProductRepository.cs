using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Models.Queries;
using supermarketapi.Domain.Repositories;
using supermarketapi.Persistence.Contexts;

namespace supermarketapi.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> ListProductsListAsync(int categoryId, string clientUID)
        {
            try
            {

                IList<Product> products = await _context.Products.Where(x => x.ClientUID == clientUID).ToListAsync();

                return (from Product product in products
                        where product.CategoryId == categoryId
                        select product).ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

       
        public async Task<Product> FindByIdAsync(Product product)
        {
            return await _context.Products
                                 .FirstOrDefaultAsync(p => p.Id == product.Id && p.CategoryId == product.CategoryId); // Since Include changes the method return, we can't use FindAsync
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<bool> RemoveAll(int categoryId, string clientUID)
        {
            try
            {
                var products = await _context.Products.Where(p => p.CategoryId == categoryId && p.ClientUID == clientUID).ToListAsync();

                foreach (var product in products)
                {
                    _context.Products.Remove(product);
                }
                return true;
            }

            catch(Exception ex)
            {
                return false;
            }
           
        }
    }
}