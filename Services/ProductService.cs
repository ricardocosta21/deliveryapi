using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Models.Queries;
using supermarketapi.Domain.Repositories;
using supermarketapi.Domain.Services;
using supermarketapi.Domain.Services.Communication;
using supermarketapi.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace supermarketapi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMemoryCache cache, IConfiguration config)
        {
            _productRepository = productRepository;
            
            _unitOfWork = unitOfWork;
            _cache = cache;
            _configuration = config;
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
            // items per page. I have to compose a cache to avoid returning wrong data.
            
                return await _productRepository.ListAsync();
            
        }
        public async Task<IEnumerable<Product>> ListProductsListAsync(int categoryId, string clientUID)
        {
            // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
            // items per page. I have to compose a cache to avoid returning wrong data.

            return await _productRepository.ListProductsListAsync(categoryId, clientUID);
        }
        


        public async Task<bool> AddAsync(Product product)
        {
            try
            {
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }

        }

        public async Task<bool> UpdateAsync(Product product, string name)
        {
            var existingProduct = await _productRepository.FindByIdAsync(product);

            if (existingProduct == null)
                return false;

            var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
            if (existingCategory == null)
                return false;

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            try
            {
                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }
        }

        //public async Task<ProductResponse> DeleteAsync(int id)
        public async Task<bool> DeleteAsync(Product product)
        {
           
                var existingProduct = await _productRepository.FindByIdAsync(product);

            if (existingProduct == null)
                return false;

            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }
        }

        
        //public async Task<bool> DeleteAllAsync()
        //{                      
        //    try
        //    {
        //        _productRepository.RemoveAll();
        //        await _unitOfWork.CompleteAsync();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Do some logging stuff
        //        return false;
        //    }
        //}
    }
}