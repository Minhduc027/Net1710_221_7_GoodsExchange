using GoodsExchange.data.Base;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.data.Repository
{
    public class CommentRepository : GenericRepository<Comment>
    {
        public CommentRepository() { }
        public CommentRepository(Net1710_221_7_GoodsExchangeContext net1710_221_7_GoodsExchangeContext) => _context = net1710_221_7_GoodsExchangeContext;

        public async Task<List<Comment>> GetAllComments()
        {
            return await _context.Set<Comment>().Include(a => a.Customer).Include(a => a.Post).ToListAsync();
        }

        public async Task<IList<Comment>> GetByPostId(int id)
        {
            var c = await _context.Set<Comment>()
                          .Where(c => c.PostId == id) 
                          .Include(c => c.Customer)  
                          .ToListAsync();
            return c;
        }

        public async Task<IList<Comment>> SearchComments(string searchTerm)
        {
            return await _context.Set<Comment>()
                .Where(c=> c.Title.Contains(searchTerm))
                .Include(c => c.Customer)
                .Include(c => c.Post)
                .ToListAsync();
        }

    }
}
