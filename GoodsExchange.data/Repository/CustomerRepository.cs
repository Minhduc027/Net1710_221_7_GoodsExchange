using GoodsExchange.data.Base;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.data.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository() { }

        public async Task<Customer> GetByEmail(string email, string phone)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email.Equals(email)
            && c.Phone.Equals(phone));
        }
    }
}
