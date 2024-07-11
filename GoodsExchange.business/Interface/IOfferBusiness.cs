using GoodsExchange.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.business.Interface
{
    public interface IOfferBusiness
    {
        public Task<IGoodsExchangeResult> GetAll();
        public Task<IGoodsExchangeResult> GetById(int offerId);
        public Task<IGoodsExchangeResult> Create(Offer offer);
        public Task<IGoodsExchangeResult> Update(Offer offer);
        public Task<IGoodsExchangeResult> Delete(int offerId);
        public Task<IGoodsExchangeResult> GetAllCustomers();
    }
}
