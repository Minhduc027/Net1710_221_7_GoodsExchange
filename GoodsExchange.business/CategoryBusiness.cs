using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;

namespace GoodsExchange.business
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly Net1710_221_7_GoodsExchangeContext _context;
        public CategoryBusiness(Net1710_221_7_GoodsExchangeContext context)
        { _context = context; }
        public Task<IGoodsExchangeResult> CreateCategory()
        {
            throw new NotImplementedException();
        }

        public Task<IGoodsExchangeResult> DeleteCategory()
        {
            throw new NotImplementedException();
        }

        public Task<IGoodsExchangeResult> GetAllCategory()
        {
            throw new NotImplementedException();
        }

        public Task<IGoodsExchangeResult> UpdateCategory()
        {
            throw new NotImplementedException();
        }
    }
}
