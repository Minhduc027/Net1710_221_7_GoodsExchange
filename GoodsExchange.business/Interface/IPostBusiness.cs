using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodsExchange.data.Models;

namespace GoodsExchange.business.Interface
{
    public interface IPostBusiness
    {
        public Task<IGoodsExchangeResult> GetAll();
        public Task<IGoodsExchangeResult> GetById(int id);
        public Task<IGoodsExchangeResult> GetByCreateDate(DateTime createDate);
        public Task<IGoodsExchangeResult> GetByUser(int userId);
        public Task<IGoodsExchangeResult> Create(Post post);
        public Task<IGoodsExchangeResult> Update(Post post);
        public Task<IGoodsExchangeResult> Delete(int postId);
        
    }
}
