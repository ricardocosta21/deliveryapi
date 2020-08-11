using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Services;
using supermarketapi.Resources;
using System.Data.SqlClient;
using supermarketapi.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using System;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace supermarketapi.Controllers
{
    [Route("/api/basket")]
    [Produces("application/json")]
    [ApiController]
    public class BasketProductController : Controller
    {
        private readonly IBasketProductService _basketService;

        private AppDbContext _context;

        public BasketProductController(IBasketProductService basketService, AppDbContext context)
        {
            _basketService = basketService;
            _context = context;
        }

        

        //[HttpGet]
        //public async Task<IEnumerable<BasketProduct>> GetAsync()
        //{
        //    return await _basketService.ListAsync();
        //}

        [HttpGet]
        public async Task<IEnumerable<BasketProduct>> GetBasketProductAsync(string clientUID)
        {
            return await _basketService.ListAsync(clientUID);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<bool> PostAsync([FromBody] BasketProduct bProduct)
        {
            return await _basketService.AddAsync(bProduct);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _basketService.Remove(id);
        }

    }
}
