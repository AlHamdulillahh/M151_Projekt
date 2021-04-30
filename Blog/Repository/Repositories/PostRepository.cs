using Blog.Data;
using Blog.Data.Models;
using Blog.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository.Repositories
{
    public class PostRepository : BaseRepository<Post, int>, IPostRepository
    {
        public PostRepository(DataContext context) : base(context) { }
        public DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}
