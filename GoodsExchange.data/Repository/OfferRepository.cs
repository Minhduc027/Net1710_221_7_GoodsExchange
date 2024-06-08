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
    public class OfferRepository : GenericRepository<Offer>
    {
        public OfferRepository() { }
        public async Task<List<Offer>> GetApprovedOffersAsync()
        {
            return await _context.Offers.Where(o => o.IsApproved == true).ToListAsync();
        }

        public async Task<List<Offer>> GetOffersByCustomerIdAsync(int customerId)
        {
            return await _context.Offers.Where(o => o.CustomerId == customerId).ToListAsync();
        }

        public async Task<List<Offer>> GetLOffersWithDetailsAsync()
        {
            return await _context.Offers
                .Include(o => o.Customer)
                .Include(o => o.OfferDetails)
                .ToListAsync();
        }
    }
}