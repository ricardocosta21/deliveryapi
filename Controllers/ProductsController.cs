using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Models.Queries;
using supermarketapi.Domain.Services;
using supermarketapi.Resources;
using supermarketapi.Persistence.Contexts;

namespace supermarketapi.Controllers
{
    [Route("/api/products")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        private AppDbContext _context;

        public ProductsController(IProductService productService, AppDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        /// <summary>
        /// Lists all existing products.
        /// </summary>
        /// <returns>List of products.</returns>
        //[HttpGet]
        //[Route("products")]
        //public async Task<IEnumerable<Product>> GetAllAsync()
        //{
        //    return await _productService.ListAsync();
        //}

        [HttpGet]
        [ProducesResponseType(typeof(bool), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IEnumerable<Product>> GetProductsListAsync(int categoryId, string clientUID)
        {
            return await _productService.ListProductsListAsync(categoryId, clientUID);
        }

        /// <summary>
        /// Saves a new product.
        /// </summary>
        /// <param name="resource">Product data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<bool> PostAsync([FromBody] Product product)
        {
            return await _productService.AddAsync(product);
        }

        /// <summary>
        /// Updates an existing product according to an identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <param name="resource">Product data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<bool> PutAsync([FromBody] Product product, string newName)
        {
            return await _productService.UpdateAsync(product, newName);
        }

        /// <summary>
        /// Deletes a given product according to an identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>Response for the request.</returns>
        //[HttpDelete("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<bool> DeleteAsync([FromBody] Product product)
        {
            return await _productService.DeleteAsync(product);
        }

        //[HttpDelete("all")]
        //[ProducesResponseType(typeof(bool), 200)]
        //[ProducesResponseType(typeof(ErrorResource), 400)]
        //public async Task<bool> DeleteAllAsync()
        //{
        //    return await _productService.DeleteAllAsync();
        //}
    }
}