using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.business.Interface
{
    internal interface ICategoryBusiness
    {
        public Task<IGoodsExchangeResult> GetAllCategory();
        public Task<IGoodsExchangeResult> CreateCategory();
        public Task<IGoodsExchangeResult> UpdateCategory();
        public Task<IGoodsExchangeResult> DeleteCategory();
    }
}
