using GoodsExchange.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GoodsExchange.business.Interface
{
    internal interface ICustomerBusiness
    {
        public Task<IGoodsExchangeResult> CreateCustomer(Customer customer);
        public Task<IGoodsExchangeResult> DeleteCustomer(int customerId);
        public Task<IGoodsExchangeResult> GetAllCustomer();
        public Task<IGoodsExchangeResult> UpdateCustomer(Customer customer);
    }
}
