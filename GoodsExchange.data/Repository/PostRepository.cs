using GoodsExchange.data.Base;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.data.Repository
{
    public class PostRepository: GenericRepository<Post>
    {
        public PostRepository() { }
        public PostRepository(Net1710_221_7_GoodsExchangeContext context) => _context = context;
        public async Task<List<Post>> GetPostByCreateDateAsync(DateTime createDate)
        {
            return await _context.Posts.Where(p => p.CreateDate == createDate).ToListAsync();
        }
        public async Task<List<Post>> GetPostByCreateUserAsync(int userId)
        {
            return await _context.Posts.Include(p => p.PostOwnerId == userId).ToListAsync();
        }
        public async Task<List<Post>> GetAllPost()
        {
            return await _context.Posts.Include(p => p.Comments).ToListAsync();
        }
    }
}
