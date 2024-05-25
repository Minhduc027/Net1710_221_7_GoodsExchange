using GoodsExchange.data.Base;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.data.DAO
{
    public class OfferDAO : BaseDAO<Offer>
    {
        public OfferDAO() { }
        public async Task<List<Offer>> GetApprovedOffersAsync()
        {
            return await _dbSet.Where(o => o.IsApproved == true).ToListAsync();
        }

        public async Task<List<Offer>> GetOffersByCustomerIdAsync(int customerId)
        {
            return await _dbSet.Where(o => o.CustomerId == customerId).ToListAsync();
        }

        public async Task<List<Offer>> GetOffersWithDetailsAsync()
        {
            return await _dbSet
                .Include(o => o.Customer)
                .Include(o => o.OfferDetails)
                .ToListAsync();
        }
    }
}
