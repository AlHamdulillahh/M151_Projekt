using AutoMapper;
using Blog.Data.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<IdentityUser, UserViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
        }
    }
}
