using GoodsExchange.business.Interface;
using GoodsExchange.data.DAO;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodExchange.commons;
using GoodsExchange.data;

namespace GoodsExchange.business
{
    public class OfferBusiness : IOfferBusiness
    {
        //private readonly Net1710_221_7_GoodsExchangeContext _context;
        //public OfferBusiness(Net1710_221_7_GoodsExchangeContext context)
        //{
        //    _context = context;
        //}
        //private readonly OfferDAO _offerDAO;
        //public OfferBusiness()
        //{
        //    _offerDAO = new OfferDAO();
        //}
        private readonly UnitOfWork _unitOfWork;
        public OfferBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IGoodsExchangeResult> GetById(int offerId)
        {
            try
            {
                var offer = await _unitOfWork.OfferRepository.GetByIdAsync(offerId);
                if (offer == null)
                {
                    return new GoodsExchangeResult(-1, Constant.NOT_FOUND);
                }
                return new GoodsExchangeResult(0, Constant.SUCCESS, offer);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }
        public async Task<IGoodsExchangeResult> Create(Offer offer)
        {
            try
            {
                //_context.Offers.Add(offer);
                //await _context.SaveChangesAsync();
                //await _offerDAO.CreateAsync(offer);
                _unitOfWork.OfferRepository.PrepareCreate(offer);
                offer.OfferDate = DateTime.Now;
                await _unitOfWork.OfferRepository.SaveAsync();

                return new GoodsExchangeResult(0, Constant.CREATE_SUCCESS, offer);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> Delete(int offerId)
        {
            try
            {
                //var offer = await _context.Offers.FindAsync(offerId);
                //var offer = await _offerDAO.GetByIdAsync(offerId);
                var offer = await _unitOfWork.OfferRepository.GetByIdAsync(offerId);
                if (offer == null)
                {
                    return new GoodsExchangeResult(-1, Constant.NOT_FOUND);
                }

                //_context.Offers.Remove(offer);
                //await _context.SaveChangesAsync();
                //await _offerDAO.RemoveAsync(offer);
                _unitOfWork.OfferRepository.PrepareRemove(offer);
                await _unitOfWork.OfferRepository.SaveAsync();
                return new GoodsExchangeResult(0, Constant.SUCCESS);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetAll()
        {
            try
            {
                //var offers = await _context.Offers.Include(o => o.Customer).Include(o => o.OfferDetails).ToListAsync(); //Eager Loading
                //var offers = await _offerDAO.GetOffersWithDetailsAsync();
                var offers = await _unitOfWork.OfferRepository.GetLOffersWithDetailsAsync();
                if (offers == null || offers.Count == 0)
                {
                    return new GoodsExchangeResult(0, Constant.SUCCESS_EMPTY);
                }
                return new GoodsExchangeResult(0, Constant.SUCCESS, offers);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }
        public async Task<IGoodsExchangeResult> GetAllCustomers()
        {
            try
            {
                var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
                if (customers == null || customers.Count == 0)
                {
                    return new GoodsExchangeResult(0, Constant.SUCCESS_EMPTY);
                }
                return new GoodsExchangeResult(0, Constant.SUCCESS, customers);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> Update(Offer offer)
        {
            try
            {
                //var existingOffer = await _context.Offers.FindAsync(offer.OfferId);
                //var existingOffer = await _offerDAO.GetByIdAsync(offer.OfferId);
                var existingOffer = await _unitOfWork.OfferRepository.GetByIdAsync(offer.OfferId);
                if (existingOffer == null)
                {
                    return new GoodsExchangeResult(-1, Constant.NOT_FOUND);
                }

                existingOffer.CustomerId = offer.CustomerId;
                existingOffer.IsApproved = offer.IsApproved;
                existingOffer.OfferDate = DateTime.Now;

                //await _context.SaveChangesAsync();
                //await _offerDAO.UpdateAsync(existingOffer);
                _unitOfWork.OfferRepository.PrepareUpdate(existingOffer);
                await _unitOfWork.OfferRepository.SaveAsync();
                return new GoodsExchangeResult(0, Constant.SUCCESS, existingOffer);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }
    }
}