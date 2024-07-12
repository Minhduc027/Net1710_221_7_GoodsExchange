using GoodsExchange.data.Base;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsExchange.data.Repository
{
    public class OfferDetailRepository : GenericRepository<OfferDetail>
    {
        public OfferDetailRepository() { }

        public async Task<List<OfferDetail>> GetOfferDetailsWithDetailsAsync()
        {
            return await _context.OfferDetails
                .Include(od => od.Offer)
                .Include(od => od.Post)
                .ToListAsync();
        }
    }
}
