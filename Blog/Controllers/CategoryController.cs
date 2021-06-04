using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Data.Models;
using Blog.Services.Interfaces;
using AutoMapper;
using Blog.ViewModels;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;
        public ICategoryService CategoryService { get; }
        public IMapper Mapper { get; }

        public CategoryController(DataContext context, ICategoryService categoryService, IMapper mapper)
        {
            _context = context;
            CategoryService = categoryService;
            Mapper = mapper;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetCategories()
        {
            var categories = await CategoryService.GetAll();
            var model = Mapper.Map<List<Category>, List<CategoryViewModel>>(categories);
            return Ok(model);
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory([Bind("Id, Name")]Category category)
        {
            await CategoryService.Add(category);

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
