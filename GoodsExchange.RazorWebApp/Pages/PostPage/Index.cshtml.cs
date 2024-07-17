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
using GoodExchange.commons;

namespace GoodsExchange.RazorWebApp.Pages.PostPage
{
    public class IndexModel : PageModel
    {
        private readonly IPostBusiness _postBusiness;

        public IndexModel()
        {
            _postBusiness ??= new PostBusiness();
        }

        public IList<Post> Post { get;set; } = default!;
        public async Task OnGetAsync()
        {
            var postList = await _postBusiness.GetAll();
            if(postList.Data != null)
            {
                Post = postList.Data as List<Post>;
            }
        }
        public async Task<IActionResult> OnGetUpVote(int id)
        {
            var result = await _postBusiness.UpVote(id);
            
                var postList = await _postBusiness.GetAll();
                if (postList.Data != null)
                {
                    Post = postList.Data as List<Post>;
                }
                return Page();
        }

        public async Task OnPostSearchAsync(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var result = await _postBusiness.SearchPost(search);
                if (result != null)
                {
                    Post = (List<Post>)result.Data;
                }
            }
            else
            {
                var a = await _postBusiness.GetAll();
                if (a != null)
                {
                    Post = (List<Post>)a.Data;
                }
            }
        }
    }
}
