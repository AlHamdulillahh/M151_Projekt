using Blog.Auth;
using Blog.Helpers;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtConfig _jwtConfig;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAuthService _authService;
        public AccountController(JwtConfig jwtConfig, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _jwtConfig = jwtConfig;
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = new AuthService(userManager);
        }

        [HttpPost]
        [Route("api/Account/Login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    return NotFound(new { ErrorMessage = $"User with Username {model.UserName} not found" });
                }

                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    return BadRequest(new { ErrorMessage = "Combination of password and Username is incorrect" });
                }

                return Ok(new { token = await _authService.GenerateToken(user, _jwtConfig) });
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Route("api/Account/Users")]
        public async Task<ActionResult<List<UserViewModel>>> GetUsers()
        {
            var users = _userManager.Users.ToList();
            var viewModelBin = new List<UserViewModel>();

            foreach (var user in users)
            {
                var viewModel = new UserViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                };

                viewModelBin.Add(viewModel);
            }

            return Ok(viewModelBin);
        }
    }
}
