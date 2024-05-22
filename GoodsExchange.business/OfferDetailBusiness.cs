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
    internal class OfferDetailBusiness : IOfferDetailBusiness
    {
        private static string CREATE_SUCCESS = "Create successfully!";
        private static string ERROR_EXECUTING_TASK = "Error while executing task: ";
        private static string NOT_FOUND = "OfferDetails not found";
        private static string DELETED = "OfferDetails deleted!";
        private static string SUCCESS = "Task executed successfully: ";
        private readonly Net1710_221_7_GoodsExchangeContext _context;
        public OfferDetailBusiness(Net1710_221_7_GoodsExchangeContext context)
        {
            _context = context;
        }

        public async Task<IGoodsExchangeResult> Create(OfferDetail offerDetail)
        {
            try
            {
                _context.OfferDetails.Add(offerDetail);
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, CREATE_SUCCESS, offerDetail);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> Delete(int offerDetailId)
        {
            try
            {
                var offer = await _context.OfferDetails.FindAsync(offerDetailId);
                if (offer == null)
                {
                    return new GoodsExchangeResult(-1, NOT_FOUND);
                }

                _context.OfferDetails.Remove(offer);
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, DELETED, offer);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetAll()
        {
            try
            {
                var offerDetails = await _context.OfferDetails.ToListAsync(); //Eager Loading
                return new GoodsExchangeResult(0, SUCCESS + "Get all offerDetails!", offerDetails);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> Update(OfferDetail offerDetail)
        {
            try
            {
                var existingOfferDetail = await _context.OfferDetails.FindAsync(offerDetail.OfferDetailId);
                if (existingOfferDetail == null)
                {
                    return new GoodsExchangeResult(-1, NOT_FOUND);
                }

                existingOfferDetail.Note = offerDetail.Note;
                existingOfferDetail.TraderItem = offerDetail.TraderItem;
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, SUCCESS + "Updated offerDetail!", existingOfferDetail);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + ex.Message);
            }
        }
    }
}
