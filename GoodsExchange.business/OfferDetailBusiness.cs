using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using GoodExchange.commons;
using GoodsExchange.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsExchange.business
{
    public class OfferDetailBusiness : IOfferDetailBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public OfferDetailBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IGoodsExchangeResult> GetAll()
        {
            try
            {
                var offerDetails = await _unitOfWork.OfferDetailRepository.GetOfferDetailsWithDetailsAsync();
                if (offerDetails == null || offerDetails.Count == 0)
                {
                    return new GoodsExchangeResult(0, Constant.SUCCESS_EMPTY);
                }
                return new GoodsExchangeResult(0, Constant.SUCCESS, offerDetails);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetById(int offerDetailId)
        {
            try
            {
                var offerDetail = await _unitOfWork.OfferDetailRepository.GetByIdAsync(offerDetailId);
                if (offerDetail == null)
                {
                    return new GoodsExchangeResult(-1, Constant.NOT_FOUND);
                }
                return new GoodsExchangeResult(0, Constant.SUCCESS, offerDetail);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> Create(OfferDetail offerDetail)
        {
            try
            {
                _unitOfWork.OfferDetailRepository.PrepareCreate(offerDetail);
                await _unitOfWork.OfferDetailRepository.SaveAsync();

                return new GoodsExchangeResult(0, Constant.CREATE_SUCCESS, offerDetail);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> Update(OfferDetail offerDetail)
        {
            try
            {
                var existingOfferDetail = await _unitOfWork.OfferDetailRepository.GetByIdAsync(offerDetail.OfferDetailId);
                if (existingOfferDetail == null)
                {
                    return new GoodsExchangeResult(-1, Constant.NOT_FOUND);
                }

                existingOfferDetail.TraderItem = offerDetail.TraderItem;
                existingOfferDetail.Note = offerDetail.Note;
                existingOfferDetail.OfferId = offerDetail.OfferId;
                existingOfferDetail.PostId = offerDetail.PostId;
                _unitOfWork.OfferDetailRepository.PrepareUpdate(existingOfferDetail);
                await _unitOfWork.OfferDetailRepository.SaveAsync();

                return new GoodsExchangeResult(0, Constant.SUCCESS, existingOfferDetail);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> Delete(int offerDetailId)
        {
            try
            {
                var offerDetail = await _unitOfWork.OfferDetailRepository.GetByIdAsync(offerDetailId);
                if (offerDetail == null)
                {
                    return new GoodsExchangeResult(-1, Constant.NOT_FOUND);
                }

                _unitOfWork.OfferDetailRepository.PrepareRemove(offerDetail);
                await _unitOfWork.OfferDetailRepository.SaveAsync();

                return new GoodsExchangeResult(0, Constant.SUCCESS);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }
    }
}
