using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using webapi.Models;

namespace webapi.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _dbContext;

        public HomeController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Home
        public IActionResult Index(int? productId)
        {
            var products = productId.HasValue
                ? _dbContext.Products.Where(p => p.Id == productId).ToList()
                : _dbContext.Products.ToList();

            return View(products);
        }

        // GET: Home/List
        public IActionResult List()
        {
            var products = _dbContext.Products.ToList();
            return View("Index", products);
        }

        // GET: Home/ProductDetails/{id}
        public IActionResult ProductDetails(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var comments = _dbContext.Comments.Where(c => c.ProductId == id && !c.IsDeleted).ToList();

            ViewBag.Product = product;
            ViewBag.Comments = comments;

            return View();
        }
    }
}
