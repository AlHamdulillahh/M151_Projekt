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
using Blog.ViewModels;
using Blog.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using AutoMapper;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly DataContext _context;
        public IPostService PostService { get; }
        public IAuthService AuthService { get; }
        public IMapper Mapper { get; }

        public PostController(DataContext context, IPostService postService, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _context = context;
            PostService = postService;
            AuthService = new AuthService(userManager);
            Mapper = mapper;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPosts(string query)
        {
            var includes = new List<string>()
            {
                "Category",
                "User"
            };
            var posts = await PostService.GetAll(includes);

            if(!String.IsNullOrEmpty(query))
            {
                posts = await PostService.Search(query);
            }

            var model = Mapper.Map<List<Post>, List<PostViewModel>>(posts.ToList());
            return model;
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostViewModel>> GetPost(int id)
        {
            var includes = new List<string>()
            {
                "Category",
                "User",
                "Comments"
            };
            var post = await PostService.Get(id, includes);

            if (post == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<Post, PostViewModel>(post);
            viewModel.Comments = Mapper.Map<List<Comment>, List<CommentViewModel>>(post.Comments.ToList());

            return viewModel;

        }

        // PUT: api/Post/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, PostViewModel post)
        {

            var existingPost = await PostService.Get(id, "Category");
            if (existingPost == null) return NotFound();

            var modifiedPost = Mapper.Map(post, existingPost);
            await PostService.Update(modifiedPost);

            return NoContent();
        }

        // POST: api/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            post.UserId = AuthService.GetUserId(User);
            await PostService.Add(post);

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Post/5

        /// <summary>
        /// Deletes a specific Post.
        /// </summary>
        /// <param name="id"></param>     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            // await PostService.Delete(id);
            var post = await PostService.Get(id, "Category");

            if (post == null)
            {
                return NotFound();
            }

            await PostService.Delete(post);

            return NoContent();
        }
    }
}
