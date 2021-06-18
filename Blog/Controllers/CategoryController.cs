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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public ICategoryService CategoryService { get; }
        public IMapper Mapper { get; }

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
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
        public async Task<ActionResult<Category>> PostCategory([Bind("Id, Name")] Category category)
        {
            await CategoryService.Add(category);

            return Ok(category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await CategoryService.Get(id);
            if (category == null) return NotFound("The category you're trying to delete doesn't exist");

            await CategoryService.Delete(category);

            return NoContent();
        }
    }
}
