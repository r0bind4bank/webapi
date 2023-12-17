using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/products/{productId}/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public CommentsController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/products/{productId}/comments
        [HttpGet]
        public IActionResult GetComments(int productId)
        {
            var commentsForProduct = _dbContext.Comments
                .Where(c => c.ProductId == productId && !c.IsDeleted)
                .ToList();

            return Ok(commentsForProduct);
        }

        // GET api/products/comments/all
        [HttpGet("all")]
        public IActionResult GetAllComments()
        {
            var allComments = _dbContext.Comments
                .Where(c => !c.IsDeleted)
                .ToList();

            return Ok(allComments);
        }

        // GET api/products/{productId}/comments/{id}
        [HttpGet("{id}")]
        public IActionResult GetCommentById(int productId, int id)
        {
            var comment = _dbContext.Comments
                .FirstOrDefault(c => c.ProductId == productId && c.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // POST api/products/{productId}/comments
        [HttpPost]
        public IActionResult AddComment(int productId, [FromBody] Comment comment)
        {
            // Implementacja dodawania komentarza
            comment.CreationDate = DateTime.Now;
            comment.ProductId = productId;
            comment.IsDeleted = false;

            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();

            return Ok(comment);
        }

        // PUT api/products/{productId}/comments/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateComment(int productId, int id, [FromBody] Comment comment)
        {
            var existingComment = _dbContext.Comments
                .FirstOrDefault(c => c.ProductId == productId && c.Id == id);

            if (existingComment == null)
            {
                return NotFound();
            }

            // Implementacja edytowania komentarza
            existingComment.Description = comment.Description;
            _dbContext.SaveChanges();

            return Ok(existingComment);
        }

        // DELETE api/products/{productId}/comments/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int productId, int id)
        {
            var comment = _dbContext.Comments
                .FirstOrDefault(c => c.ProductId == productId && c.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            // Implementacja usuwania komentarza
            comment.IsDeleted = true;
            _dbContext.SaveChanges();

            return Ok(comment);
        }
    }
}
