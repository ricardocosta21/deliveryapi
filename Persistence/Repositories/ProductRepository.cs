using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Models.Queries;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> ListProductsListAsync(int categoryId)
        {
            try
            {
                IEnumerable<Product> products = await _context.Products.ToListAsync();
                List<Product> productsCategory = new List<Product>();

                foreach (var product in products)
                {
                    if (product.CategoryId == categoryId)
                    {
                        productsCategory.Add(product);
                    }
                }

                return productsCategory;
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
    }
}