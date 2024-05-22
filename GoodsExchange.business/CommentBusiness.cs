using GoodsExchange.business.Interface;
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
        private readonly Net1710_221_7_GoodsExchangeContext _context;


        public CommentBusiness(Net1710_221_7_GoodsExchangeContext context)
        {
            _context = context;
        }


        public async Task<IGoodsExchangeResult> GetAll()
        {

            try
            {
                var result = await _context.Comments.ToListAsync();
                return new GoodsExchangeResult(0, "Get all comment successfully", result);

            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Failed to get all comment: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> GetByDate(DateTime date)
        {
            try
            {
                var result = await _context.Comments
                    .Where(x => x.DateTime.Day == date.Day)
                    .ToListAsync();
                return new GoodsExchangeResult(0, "Get successfully", result);

            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Failed to get comment: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> GetById(int id)
        {
            try
            {
                var result = await _context.Comments
                    .FindAsync(id);
                if (result == null)
                    return new GoodsExchangeResult(-1, "Comment didn't exist");
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
               _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return new GoodsExchangeResult(0, "Get all comment successfully");

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
                var result = await _context.Comments
                    .FindAsync(commentId);
                if (result == null)
                    return new GoodsExchangeResult(-1, "Comment didn't exist");
                return new GoodsExchangeResult(0, "Get all comment successfully");

            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Failed to get comment: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> UpdateComment(Comment comment)
        {
            try
            {
                var existComment = await _context.Comments.FindAsync(comment.CommentId);
                if (existComment == null)
                {
                    return new GoodsExchangeResult(-1, "Comment didn't exist");
                }

                existComment.Content = comment.Content;
                existComment.Title = comment.Title;
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, "Update comment successfully", existComment);

            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Failed to update comment: {ex.Message}");
            }
        }
    }
}
