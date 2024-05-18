using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.business.Interface
{
    public interface IPostBusiness
    {
        public Task<IGoodsExchangeResult> GetAll();
        public Task<IGoodsExchangeResult> Create();
        public Task<IGoodsExchangeResult> Update();
        public Task<IGoodsExchangeResult> Delete();
        
    }
}
