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

namespace GoodsExchange.RazorWebApp.Pages.PostPage
{
    public class DeleteModel : PageModel
    {
        private readonly IPostBusiness _postBusiness;
        private readonly ICategoryBusiness _categoryBusiness;
        private readonly ICustomerBusiness _customerBusiness;

        public DeleteModel()
        {
            _postBusiness ??= new PostBusiness();
            _categoryBusiness ??= new CategoryBusiness();
            _customerBusiness ??= new CustomerBusiness();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postBusiness.GetById((int)id);

            if (post == null)
            {
                return NotFound();
            }
            else
            {
                Post = post.Data as Post;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postBusiness.GetById((int)id);
            if (post != null)
            {
                Post = post.Data as Post;
                await _postBusiness.Delete((int)id);
            }

            return RedirectToPage("./Index");
        }
    }
}
