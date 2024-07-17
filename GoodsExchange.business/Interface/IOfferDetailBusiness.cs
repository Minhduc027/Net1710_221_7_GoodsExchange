using GoodsExchange.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.business.Interface
{
    public interface IOfferDetailBusiness
    {
        public Task<IGoodsExchangeResult> GetAll();
        public Task<IGoodsExchangeResult> GetById(int offerDetailId);
        public Task<IGoodsExchangeResult> Create(OfferDetail offerDetail);
        public Task<IGoodsExchangeResult> Update(OfferDetail offerDetail);
        public Task<IGoodsExchangeResult> Delete(int offerDetailId);
    }
}
