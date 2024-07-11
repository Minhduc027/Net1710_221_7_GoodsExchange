using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodsExchange.data.Models;

namespace GoodsExchange.business.Interface
{
    public interface ICategoryBusiness
    {
        public Task<IGoodsExchangeResult> GetAllCategory();
        public Task<IGoodsExchangeResult> CreateCategory(Category category);
        public Task<IGoodsExchangeResult> UpdateCategory(Category category);
        public Task<IGoodsExchangeResult> DeleteCategory(int categoryId);
        public Task<Category> GetCategoryById(int categoryId);
    }
}
