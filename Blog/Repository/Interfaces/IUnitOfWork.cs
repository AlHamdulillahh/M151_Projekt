using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; set; }
        IPostRepository Posts { get; set; }
        ICommentRepository Comments { get; set; }
        Task<int> CompleteAsync();

    }
}
