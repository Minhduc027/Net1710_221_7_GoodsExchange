using GoodsExchange.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.business.Interface
{
    public interface ICommentBusiness
    {
        public Task<IGoodsExchangeResult> GetAll();
        public Task<IGoodsExchangeResult> GetById(int id);
        public Task<IGoodsExchangeResult> GetByDate(DateTime date);
        public Task<IGoodsExchangeResult> UpdateComment(Comment comment);
        public Task<IGoodsExchangeResult> CreateComment(Comment comment);
        public Task<IGoodsExchangeResult> DeleteConmment(int commentId);
    }
}
