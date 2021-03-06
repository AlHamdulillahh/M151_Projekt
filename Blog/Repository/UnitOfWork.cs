using Blog.Data;
using Blog.Repository.Interfaces;
using Blog.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            Posts = new PostRepository(_context);
            Comments = new CommentRepository(_context);
        }
        public ICategoryRepository Categories { get; set; }
        public IPostRepository Posts { get; set; }
        public ICommentRepository Comments { get; set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
