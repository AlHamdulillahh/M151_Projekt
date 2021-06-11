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
        public IPostService PostService { get; }
        public IAuthService AuthService { get; }
        public IMapper Mapper { get; }

        public PostController(IPostService postService, UserManager<IdentityUser> userManager, IMapper mapper)
        {
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
                return NotFound("The post you're searching for doesn't exist");
            }

            var viewModel = Mapper.Map<Post, PostViewModel>(post);
            viewModel.Comments = Mapper.Map<List<Comment>, List<CommentViewModel>>(post.Comments.ToList());
            viewModel.User = Mapper.Map<IdentityUser, UserViewModel>(post.User);

            return viewModel;

        }

        // PUT: api/Post/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutPost(int id, PostViewModel post)
        {

            var existingPost = await PostService.Get(id, "Category");
            if (existingPost == null) return NotFound("The post you're trying to edit doesn't exist");

            var modifiedPost = Mapper.Map(post, existingPost);
            await PostService.Update(modifiedPost);

            return NoContent();
        }

        // POST: api/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            post.UserId = AuthService.GetUserId(User);
            await PostService.Add(post);

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Post/5

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await PostService.Get(id, "Category");

            if (post == null)
            {
                return NotFound("The post you're trying to delete doesn't exist");
            }

            await PostService.Delete(post);

            return NoContent();
        }
    }
}
