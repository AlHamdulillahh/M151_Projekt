using Blog.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Auth
{
    public interface IAuthService
    {
        public Task<string> GenerateToken(IdentityUser user, JwtConfig config);
        public string GetUserId(ClaimsPrincipal user);
    }
}
