using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Repositories;
using supermarketapi.Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Linq;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace supermarketapi.Services
{
    public class BasketProductService : IBasketProductService
    {
        private readonly IBasketProductRepository _basketRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public BasketProductService(IBasketProductRepository basketRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IMemoryCache cache, IConfiguration config)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _configuration = config;
        }

        public async Task<IEnumerable<BasketProduct>> ListAsync()
        {
            return await _basketRepository.ListAsync();
        }

        public async Task<IEnumerable<BasketProduct>> ListAsync(string clientUID)
        {
            return await _basketRepository.ListAsync(clientUID);
        }

        public async Task<bool> AddAsync(BasketProduct bProduct)
        {
            try
            {
                var existingBasketProduct = await _basketRepository.FindByIdAsync(bProduct.Id);

                if (existingBasketProduct == null)
                {
                    bProduct.Quantity = 1;
                    _basketRepository.Add(bProduct);
                }
                else
                {
                    existingBasketProduct.Quantity++;
                    _basketRepository.Update(existingBasketProduct);
                }
                
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }
        }

        public async Task<bool> DecrementAsync(int bProductId)
        {
            var existingBasketProduct = await _basketRepository.FindByIdAsync(bProductId);

            if (existingBasketProduct == null)
                return false;

            try
            {
                if (existingBasketProduct.Quantity == 1)
                {
                    //removes item from list
                    _basketRepository.Remove(existingBasketProduct);
                }
                else
                {
                    existingBasketProduct.Quantity--;
                    _basketRepository.Update(existingBasketProduct);
                }

                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }
        }

        public async Task<bool> Remove(int bProductId)
        {
            var existingBasketProduct = await _basketRepository.FindByIdAsync(bProductId);

            if (existingBasketProduct == null)
                return false;

            try
            {
                _basketRepository.Remove(existingBasketProduct);

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
