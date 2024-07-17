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

namespace GoodsExchange.RazorWebApp.Pages.CommentPage
{
    public class CommentListModel : PageModel
    {
        private readonly ICommentBusiness _commentBussiness;


        public CommentListModel()
        {
            _commentBussiness ??= new CommentBusiness();
        }

        public IList<Comment> Comment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var a = await _commentBussiness.GetAllComments();
            if (a != null)
            {
                Comment = a.Data as IList<Comment>;
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _commentBussiness.DeleteConmment(id);
            if (result.Status == 0)
            {
                return RedirectToPage();
            }
            else
            {
                return Page();
            }
        }

        public async Task OnPostSearchAsync(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var a = await _commentBussiness.SearchComment(search);
                if (a != null)
                {
                    Comment = a.Data as IList<Comment>;
                }
            }
            else
            {
                var a = await _commentBussiness.GetAllComments();
                if (a != null)
                {
                    Comment = a.Data as IList<Comment>;
                }
            }
        }
    }
}
