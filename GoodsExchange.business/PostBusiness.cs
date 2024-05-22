using GoodExchange.commons;
using GoodsExchange.business.Interface;
using GoodsExchange.data.DAO;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.business
{

    public class PostBusiness : IPostBusiness
    {
        private readonly PostDAO _postDAO;

        public PostBusiness()
        {
            _postDAO = new PostDAO();
        }

        public async Task<IGoodsExchangeResult> Create(Post postCreate)
        {
            try
            {
                //await _context.AddAsync(postCreate);
                //await _context.SaveChangesAsync();
                int result = await _postDAO.CreateAsync(postCreate);
                if(result > 0) 
                { 
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.CREATE_SUCCESS, postCreate); 
                }
                else
                {
                    return new GoodsExchangeResult(Constant.NOTHING_WERE_CHANGED_STATUS, Constant.ERROR_EXECUTING_TASK, "Create failed!");
                }
            } catch (Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> Delete(int postId)
        {
            try
            {
                var post = await _postDAO.GetByIdAsync(postId);
                if (post == null)
                {
                    return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.NOT_FOUND);
                }
                //var comments = _context.Comments.Where(c => c.PostId == post.PostId).ToList();
                //_context.Comments.RemoveRange(comments);
                //await _context.SaveChangesAsync();

                //var offerDetails = _context.OfferDetails.Where(c => c.PostId == postId).ToList();
                //_context.OfferDetails.RemoveRange(offerDetails);
                //await _context.SaveChangesAsync();

                bool result = _postDAO.Remove(post);
                if (result)
                {
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.DELETED, post);
                }
                else
                {
                    return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK);
                }
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetAll()
        {
            try
            {
                var result = await _postDAO.GetAllAsync();
                //var postList = await _context.Posts.Include(c => c.Comments).Include(o => o.OfferDetails)
                //    .ToListAsync();
                if(result == null)
                {
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS_EMPTY);
                }else
                {
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS + "Get all Post.", result);
                }
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetByCreateDate(DateTime createDate)
        {
            try
            { 
                var result = await _postDAO.GetPostByCreateDateAsync(createDate);
                if(result == null)
                {
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS_EMPTY + $" No post created on: {createDate}.");
                }
                else
                {
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS + $" Get posts created on: {createDate}.", result);
                }             
            } catch (Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetById(int id)
        {
            try
            {
                var result = await _postDAO.GetByIdAsync(id);
                if(result == null)
                {
                    return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.NOT_FOUND);
                }
                return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS + "Get post by Id.", result);
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetByUser(int userId)
        {
            try
            {
                var result = await _postDAO.GetPostByCreateUserAsync(userId);
                if(result == null)
                {
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS_EMPTY + "Get User's post", "This user haven't create any post yet!");
                }
                return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS + $"Get User {userId} post!", result);
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> Update(Post postUpdate)
        {
            try
            {
                var post = await _postDAO.GetByIdAsync(postUpdate.PostId);
                if(post == null)
                {
                    return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.NOT_FOUND);
                }
                post.Title = postUpdate.Title;
                post.Description = postUpdate.Description;
                post.Address = postUpdate.Address;
                post.Category = postUpdate.Category;
                post.CategoryId = postUpdate.Category.CategoryId;
                _postDAO.Update(post);

                return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS + "Post updated!", post);
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }
    }
}
