using GoodsExchange.business.Interface;
using GoodsExchange.data;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.business
{
    public class CommentBusiness : ICommentBusiness
    {
        private readonly UnitOfWork _unitOfWork;


        public CommentBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }


        public async Task<IGoodsExchangeResult> GetAllComments()
        {

            try
            {
                var result = await _unitOfWork.CommentRepository.GetAllComments();
                return new GoodsExchangeResult(0, "Get all comment successfully", result);

            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Failed to get all comment: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> GetByPostId(int id)
        {
            try
            {
                var result = await _unitOfWork.CommentRepository.GetByPostId(id);
                return new GoodsExchangeResult(0, "Get all comment successfully", result);

            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Failed to get comment: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> CreateComment(Comment comment)
        {
            try
            {
                await _unitOfWork.CommentRepository.CreateAsync(comment);
                return new GoodsExchangeResult(0, "Create comment successfully");

            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Failed to add comment: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> DeleteConmment(int commentId)
        {
            try
            {
                var Existed = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
                if (Existed == null)
                    return new GoodsExchangeResult(-1, "Comment didn't exist");

                var result = await _unitOfWork.CommentRepository.RemoveAsync(Existed);
                
                return new GoodsExchangeResult(0, "Delete comment successfully");

            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Failed to delete comment: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> UpdateComment(Comment comment)
        {
            try
            {
                var existComment = await _unitOfWork.CommentRepository.UpdateAsync(comment);
                return new GoodsExchangeResult(0, "Update comment successfully", existComment);

            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Failed to update comment: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> GetById(int id)
        {
            try
            {
                var existComment = await _unitOfWork.CommentRepository.GetByIdAsync(id);
                if (existComment == null)
                {
                    return new GoodsExchangeResult(-1, "Comment didn't exist");
                }

                return new GoodsExchangeResult(0, "Update comment successfully", existComment);

            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Failed to update comment: {ex.Message}");
            }
        }
    }
}
