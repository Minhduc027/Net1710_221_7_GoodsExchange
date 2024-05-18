using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.business
{

    public class PostBusiness : IPostBusiness
    {
        private readonly Net1710_221_7_GoodsExchangeContext _context;
        public PostBusiness(Net1710_221_7_GoodsExchangeContext context)
        {
            _context = context;
        }

        public Task<IGoodsExchangeResult> Create()
        {
            throw new NotImplementedException();
        }

        public Task<IGoodsExchangeResult> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<IGoodsExchangeResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IGoodsExchangeResult> Update()
        {
            throw new NotImplementedException();
        }
    }
}
