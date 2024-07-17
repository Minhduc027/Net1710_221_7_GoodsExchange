using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GoodsExchange.data.Models;
using GoodsExchange.business.Interface;
using GoodsExchange.business;
using System.Xml.Linq;

namespace GoodsExchange.RazorWebApp.Pages.CommentPage
{
    public class CommentModel : PageModel
    {
        private readonly ICommentBusiness _commentBussiness;
        private readonly IPostBusiness _postBusiness;

        public CommentModel()
        {
            _commentBussiness ??= new CommentBusiness();
            _postBusiness ??= new PostBusiness();
        }

        [BindProperty]
        public Post Post { get; set; }
        public IList<Comment> Comment { get;set; }

        [BindProperty]
        public Comment NewComment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ModelState.Clear();
            var post = await _postBusiness.GetById(id);
            Post = post.Data as Post;
            var a = await _commentBussiness.GetByPostId(id);
            if (a != null)
            {
                Comment = a.Data as IList<Comment>;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddCommentAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var customerId = HttpContext.Session.GetString("UserId");
            NewComment.PostId = Post.PostId;
            NewComment.DateTime = DateTime.UtcNow;
            NewComment.CustomerId = int.Parse(customerId!);
            var result = await _commentBussiness.CreateComment(NewComment);
            if (result.Status == 0)
            {
                await OnGetAsync(Post.PostId);

                NewComment = new Comment();
                ModelState.Clear();

                return RedirectToPage(new { id = Post.PostId });
            }
            else
            {
                return Page();
            }
        }


        public async Task<IActionResult> OnPostUpdateAsync(int id, string content)
        {
            var comment = await _commentBussiness.GetById(id);
            var existComment = comment.Data as Comment;
            if (comment != null)
            {
                existComment.Content = content;
                existComment.DateTime = DateTime.UtcNow;
                var result = await _commentBussiness.UpdateComment(existComment);
                if (result.Status == 0)
                {
                    await OnGetAsync(Post.PostId);
                    return RedirectToPage(new { id = Post.PostId });
                }
            }
            return Page();
        }
    }
}
