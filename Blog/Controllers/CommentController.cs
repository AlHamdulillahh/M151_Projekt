using AutoMapper;
using Blog.Auth;
using Blog.Data.Models;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public ICommentService CommentService { get; }
        public IPostService PostService { get; }
        public IAuthService AuthService { get; }
        public IMapper Mapper { get;  }
        public CommentController(ICommentService service, IMapper mapper, IPostService postService,
            UserManager<IdentityUser> userManager)
        {
            CommentService = service;
            PostService = postService;
            AuthService = new AuthService(userManager);
            Mapper = mapper;
        }

        [HttpPost]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Comment>> PostComment(int id, Comment comment)
        {
            var post = await PostService.Get(id, "Category");

            if (post == null) return NotFound("The post you're trying to comment on doesn't exist");

            comment.UserId = AuthService.GetUserId(User);
            await CommentService.Add(comment);

            return Ok(comment);
        }
    }
}
