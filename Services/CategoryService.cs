using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Services.Communication;
using Supermarket.API.Infrastructure;
using Supermarket.API.Persistence.Contexts;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Supermarket.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMemoryCache cache, IConfiguration config)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _configuration = config;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.ListAsync();
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
        public async Task<bool> DeleteAsync(int id)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return false;

            try
            {
                 _categoryRepository.Remove(existingCategory);
                 await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }
        }
    }
}