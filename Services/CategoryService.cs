using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Repositories;
using supermarketapi.Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace supermarketapi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IMemoryCache cache, IConfiguration config)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _configuration = config;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.ListAsync();
        }

        public async Task<IEnumerable<Category>> ListAsync(string clientUID)
        {
            return await _categoryRepository.ListAsync(clientUID);
        }

        public async Task<Category> ListItemAsync(int id)
        {
            return await _categoryRepository.ListAsyncId(id);
        }

        public async Task<bool> AddAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }
        }

        public async Task<bool> UpdateAsync( Category category, string newName)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(category.Id);

            if (existingCategory == null)
                return false;

            existingCategory.Name = newName;

            try
            {
                _categoryRepository.Update(existingCategory);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }
        }

        // Working
        public async Task<bool> DeleteAsync(Category category)
        {
            //var existingCategory = await _categoryRepository.FindByIdAsync(category.Id);

            if (category == null)
                return false;

            if (category.ClientUID == category.ClientUID)
            {
                try
                {

                    _categoryRepository.Remove(category);

                    await _unitOfWork.CompleteAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    // Do some logging stuff
                    return false;
                }
            }

            return false;
        }

        //public async Task<bool> DeleteAllAsync()
        //{
        //    try
        //    {
        //        _categoryRepository.RemoveAll();
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