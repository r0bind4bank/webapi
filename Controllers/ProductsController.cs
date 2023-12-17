using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public ProductsController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/products
        [HttpGet]
        public IActionResult GetProducts(int page = 1, int pageSize = 10)
        {
            // Implementacja listowania produktów ze stronnicowaniem, uwzględniając IsDeleted
            var pagedProducts = _dbContext.Products
                .Where(p => !p.IsDeleted)  // Dodaj warunek, aby uwzględniać tylko nieusunięte produkty
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(pagedProducts);
        }

        // GET api/products/all
        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            // Implementacja listowania wszystkich produktów bez stronnicowania
            var allProducts = _dbContext.Products.ToList();
            return Ok(allProducts);
        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST api/products
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            // Implementacja dodawania produktu
            product.CreationDate = DateTime.Now;
            product.IsDeleted = false;

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return Ok(product);
        }

        // PUT api/products/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            // Implementacja edytowania produktu
            existingProduct.Title = product.Title;
            existingProduct.Description = product.Description;
            existingProduct.ImageUrl = product.ImageUrl;

            _dbContext.SaveChanges();

            return Ok(existingProduct);
        }

        // DELETE api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Implementacja usuwania produktu za pomocą IsDeleted
            product.IsDeleted = true;

            _dbContext.SaveChanges();

            return Ok(product);
        }
    }
}
