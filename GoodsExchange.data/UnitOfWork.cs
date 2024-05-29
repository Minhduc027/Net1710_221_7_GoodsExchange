using GoodsExchange.data.Models;
using GoodsExchange.data.Repository;

namespace GoodsExchange.data
{
    public class UnitOfWork
    {
        private Net1710_221_7_GoodsExchangeContext _context;
        private PostRepository _postRepository;
        private CategoryRepository _categoryRepository;
        public UnitOfWork(Net1710_221_7_GoodsExchangeContext context)
        {
            _context = context;
        }
        public UnitOfWork()
        {
            _context = new Net1710_221_7_GoodsExchangeContext();
        }
        private CustomerRepository _customerRepository;
        public UnitOfWork() { }
        public PostRepository PostRepository
        {
            get { return _postRepository ??= new PostRepository(); }
        }
        public CategoryRepository CategoryRepository
        {
            get { return _categoryRepository ??= new CategoryRepository(); }
        }

        public CustomerRepository CustomerRepository
        {
            get { return _customerRepository ??= new CustomerRepository(); }
        }
    }
}
