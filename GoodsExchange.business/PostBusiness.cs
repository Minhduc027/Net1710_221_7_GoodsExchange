using GoodExchange.commons;
using GoodsExchange.business.Interface;
using GoodsExchange.data;
using GoodsExchange.data.Models;

namespace GoodsExchange.business
{

    public class PostBusiness : IPostBusiness
    {
        //private readonly PostDAO _postDAO;
        private readonly UnitOfWork unitOfWork;
        public PostBusiness()
        {
            //_postDAO = new PostDAO();
            unitOfWork ??= new UnitOfWork();
        }

        public async Task<IGoodsExchangeResult> SearchPost(string search)
        {
            try
            {
                var existComment = await unitOfWork.PostRepository.SearchPost(search);
                return new GoodsExchangeResult(0, "search post successfully", existComment);

            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Failed to search post: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> Create(Post postCreate)
        {
            try
            {
                //await _context.AddAsync(postCreate);
                //await _context.SaveChangesAsync();
                //int result = await _postDAO.CreateAsync(postCreate);
                postCreate.UpdatedTime = 0;
                postCreate.UpVote = 0;
                postCreate.IsAvailable = true;
                postCreate.AvailableUntil = DateTime.Now.AddYears(1);
                int result = await unitOfWork.PostRepository.CreateAsync(postCreate);
                if (result > 0) 
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
                var post = await unitOfWork.PostRepository.GetByIdAsync(postId);
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

                bool result = unitOfWork.PostRepository.Remove(post);
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
                var result = await unitOfWork.PostRepository.GetAllPost();
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
                var result = await  unitOfWork.PostRepository.GetPostByCreateDateAsync(createDate);
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
                var result = await unitOfWork.PostRepository.GetByIdAsync(id);
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
                var result = await unitOfWork.PostRepository.GetPostByCreateUserAsync(userId);
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
                var post = await unitOfWork.PostRepository.GetByIdAsync(postUpdate.PostId);
                if(post == null)
                {
                    return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.NOT_FOUND);
                }
                post.Title = postUpdate.Title;
                post.Description = postUpdate.Description;
                post.Address = postUpdate.Address;
                post.CategoryId = postUpdate.CategoryId;
                post.UpdatedAt = postUpdate.UpdatedAt;
                post.UpdatedTime = post.UpdatedTime + 1;
                post.PostOwnerId = postUpdate.PostOwnerId;
                post.ExchangeDate = postUpdate.ExchangeDate;
                post.IsAvailable = postUpdate.IsAvailable;
                unitOfWork.PostRepository.Update(post);

                return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS + "Post updated!", post);
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> UpVote(int postId)
        {
            var post = await unitOfWork.PostRepository.GetByIdAsync(postId);
            if (post == null)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.NOT_FOUND);
            }
            post.UpVote += 1;
            unitOfWork.PostRepository.Update(post);
            return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS +"Post updated!", post);
        }
    }
}
