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

    public class PostBusiness : IPostBusiness
    {
        private static string CREATE_SUCCESS = "Create successfully!";
        private static string ERROR_EXECUTING_TASK = "Error while executing task: ";
        private static string NOT_FOUND = "Post not found";
        private static string DELETED = "Post deleted!";
        private static string SUCCESS = "Task executed successfully: ";
        private readonly Net1710_221_7_GoodsExchangeContext _context;
        public PostBusiness(Net1710_221_7_GoodsExchangeContext context)
        {
            _context = context;
        }

        public async Task<IGoodsExchangeResult> Create(Post postCreate)
        {
            try
            {
                await _context.AddAsync(postCreate);
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, CREATE_SUCCESS, postCreate);
            } catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> Delete(int postId)
        {
            try
            {
                var post = await _context.Posts.FindAsync(postId);
                if (post == null)
                {
                    return new GoodsExchangeResult(-1, NOT_FOUND);
                }
                //var comments = _context.Comments.Where(c => c.PostId == post.PostId).ToList();
                //_context.Comments.RemoveRange(comments);
                //await _context.SaveChangesAsync();

                //var offerDetails = _context.OfferDetails.Where(c => c.PostId == postId).ToList();
                //_context.OfferDetails.RemoveRange(offerDetails);
                //await _context.SaveChangesAsync();

                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, DELETED, post);
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetAll()
        {
            try
            {
                var postList = await _context.Posts.Include(c => c.Comments).Include(o => o.OfferDetails)
                    .ToListAsync();
                return new GoodsExchangeResult(0, SUCCESS + "Get all Post.", postList);
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetByCreateDate(DateTime createDate)
        {
            try
            {
                var result = await _context.Posts.Where(p => p.CreateDate == createDate).ToListAsync();

                return new GoodsExchangeResult(0, SUCCESS + "Get posts by create date.", result);
            } catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetById(int id)
        {
            try
            {
                var result = await _context.Posts.FindAsync(id);
                if(result == null)
                {
                    return new GoodsExchangeResult(-1, NOT_FOUND);
                }
                return new GoodsExchangeResult(0, SUCCESS + "Get post by Id.", result);
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetByUser(int userId)
        {
            try
            {
                var result = await _context.Posts.Include(p => p.PostOwnerId == userId).ToListAsync();
                if(result == null)
                {
                    return new GoodsExchangeResult(0, SUCCESS + "Get User's post", "This user haven't create any post yet!");
                }
                return new GoodsExchangeResult(0, SUCCESS + $"Get User {userId} post!", result);
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> Update(Post postUpdate)
        {
            try
            {
                var post =  await _context.Posts.FindAsync(postUpdate.PostId);
                if(post == null)
                {
                    return new GoodsExchangeResult(-1, NOT_FOUND);
                }
                post.Title = postUpdate.Title;
                post.Description = postUpdate.Description;
                post.Address = postUpdate.Address;
                post.Category = postUpdate.Category;
                post.CategoryId = postUpdate.Category.CategoryId;
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, SUCCESS + "Post updated!", post);
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + ex.Message);
            }
        }
    }
}
