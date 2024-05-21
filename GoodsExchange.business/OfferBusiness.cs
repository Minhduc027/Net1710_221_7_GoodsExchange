using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.business
{
    internal class OfferBusiness : IOfferBusiness
    {
        private readonly Net1710_221_7_GoodsExchangeContext _context;
        public OfferBusiness(Net1710_221_7_GoodsExchangeContext context)
        {
            _context = context;
        }

        public async Task<IGoodsExchangeResult> Create(Offer offer)
        {
            try
            {
                _context.Offers.Add(offer);
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, "Offer created successfully", offer);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Error creating offer: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> Delete(int offerId)
        {
            try
            {
                var offer = await _context.Offers.FindAsync(offerId);
                if (offer == null)
                {
                    return new GoodsExchangeResult(-1, "Offer not found");
                }

                _context.Offers.Remove(offer);
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, "Offer deleted successfully");
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Error deleting offer: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> GetAll()
        {
            try
            {
                var offers = await _context.Offers.Include(o => o.Customer).Include(o => o.OfferDetails).ToListAsync(); //Eager Loading
                return new GoodsExchangeResult(0, "Offers retrieved successfully", offers);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Error retrieving offers: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> Update(Offer offer)
        {
            try
            {
                var existingOffer = await _context.Offers.FindAsync(offer.OfferId);
                if (existingOffer == null)
                {
                    return new GoodsExchangeResult(-1, "Offer not found");
                }

                existingOffer.CustomerId = offer.CustomerId;
                existingOffer.IsApproved = offer.IsApproved;
                existingOffer.OfferDate = offer.OfferDate;

                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, "Offer updated successfully", existingOffer);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Error updating offer: {ex.Message}");
            }
        }
    }
}
