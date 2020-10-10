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
        private readonly IBasketProductRepository _basketProductRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public BasketProductService(IBasketProductRepository basketProductRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IMemoryCache cache, IConfiguration config)
        {
            _basketProductRepository = basketProductRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _configuration = config;
        }

        public async Task<IEnumerable<BasketProduct>> ListAsync()
        {
            return await _basketProductRepository.ListAsync();
        }

        public async Task<IEnumerable<BasketProduct>> ListAsync(string clientUID)
        {
            return await _basketProductRepository.ListAsync(clientUID);
        }

        public async Task<bool> AddAsync(BasketProduct bProduct)
        {
            try
            {
                var existingBasketProduct = await _basketProductRepository.FindByIdAsync(bProduct.Id);

                if (existingBasketProduct == null)
                {
                    bProduct.Quantity = 1;
                    _basketProductRepository.Add(bProduct);
                }
                else
                {
                    existingBasketProduct.Quantity++;
                    _basketProductRepository.Update(existingBasketProduct);
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
            var existingBasketProduct = await _basketProductRepository.FindByIdAsync(bProductId);

            if (existingBasketProduct == null)
                return false;

            try
            {
                if (existingBasketProduct.Quantity == 1)
                {
                    //removes item from list
                    _basketProductRepository.Remove(existingBasketProduct);
                }
                else
                {
                    existingBasketProduct.Quantity--;
                    _basketProductRepository.Update(existingBasketProduct);
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
            var existingBasketProduct = await _basketProductRepository.FindByIdAsync(bProductId);

            if (existingBasketProduct == null)
                return false;

            try
            {
                _basketProductRepository.Remove(existingBasketProduct);

                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }
        }

        public async Task<bool> DeleteAllAsync(string clientUID)
        {
            try
            {
                _basketProductRepository.RemoveAll(clientUID);
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
