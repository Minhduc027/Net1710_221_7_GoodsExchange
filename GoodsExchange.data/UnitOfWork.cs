using GoodsExchange.data.Models;
using GoodsExchange.data.Repository;

namespace GoodsExchange.data
{
    public class UnitOfWork
    {
        private Net1710_221_7_GoodsExchangeContext _context;
        private PostRepository _postRepository;
        public UnitOfWork() { }
        public PostRepository PostRepository
        {
            get { return _postRepository ??= new PostRepository(); }
        }
    }
}
