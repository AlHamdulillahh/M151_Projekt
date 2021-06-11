﻿using Blog.Data.Models;
using Blog.Repository.Interfaces;
using Blog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<Category>> GetAll()
        {
            return _unitOfWork.Categories.GetAllAsync();
        }

        public Task<Category> Get(int id)
        {
            return _unitOfWork.Categories.GetAsync(id);
        }

        public Task<Category> Get(int id, string include)
        {
            return _unitOfWork.Categories.GetAsync(id, include);
        }

        public async Task<Category> Add(Category category)
        {
            _unitOfWork.Categories.Add(category);
            await _unitOfWork.CompleteAsync();
            return category;
        }

        public async Task<int> Delete(Category category)
        {
            _unitOfWork.Categories.Remove(category);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
