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

namespace supermarketapi.Controllers
{
    [Route("/api/categories")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;


        private AppDbContext _context;

        public CategoriesController(ICategoryService categoryService, AppDbContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        /// <summary>
        /// Lists all category items.
        /// </summary>
        /// <returns>List os categories.</returns>
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryService.ListAsync();
        }

        [HttpGet("{clientUID}")]
        public async Task<IEnumerable<Category>> GetAllAsyncByClientUID(string clientUID)
        {
            return await _categoryService.ListAsync(clientUID);
        }


        /// <summary>
        /// Lists all categories.
        /// </summary>
        /// <returns>List os categories.</returns>
        //[HttpGet]
        //[Route("{id}")]
        //[ProducesResponseType(typeof(Task<Category>), 200)]
        //public async Task<Category> ListItemAsync(int id)
        //{
        //    return await _categoryService.ListItemAsync(id);
        //}

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="category">Category object data.</param>
        /// <returns>Response for the request.</returns>
        /// //
        /// Body
        //{
        // "id": 101,
        // "name": "Animais"
        //}
        [HttpPost]
        [ProducesResponseType(typeof(bool), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<bool> PostAsync([FromBody]Category category)
        {            
            return await _categoryService.AddAsync(category);
        }

        /// <summary>
        /// Updates an existing category according to an identifier.
        /// </summary>
        /// <param name="category">Category object.</param>
        /// <param name="newName">Updated category.name data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<bool> PutAsync([FromBody] Category category, string newName)
        {
            return await _categoryService.UpdateAsync(category, newName);
        }

        /// <summary>
        /// Deletes a given category according to an identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<bool> DeleteAsync([FromBody] Category category)
        {
            return await _categoryService.DeleteAsync(category);
        }

        //[HttpDelete("all")]
        //[ProducesResponseType(typeof(bool), 200)]
        //[ProducesResponseType(typeof(ErrorResource), 400)]
        //public async Task<bool> DeleteAllAsync()
        //{
        //    return await _categoryService.DeleteAllAsync();
        //}
    }
}