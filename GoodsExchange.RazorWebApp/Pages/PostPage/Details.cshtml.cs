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
    public class DetailsModel : PageModel
    {
        private readonly IPostBusiness _postBusiness;
        private readonly ICategoryBusiness _categoryBusiness;
        private readonly ICustomerBusiness _customerBusiness;

        public DetailsModel()
        {
            _postBusiness ??= new PostBusiness();
            _categoryBusiness ??= new CategoryBusiness();
            _customerBusiness ??= new CustomerBusiness();
        }
        [BindProperty]
        public Post Post { get; set; } = default!;
        public Category Category { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public List<Comment> Comments { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postBusiness.GetById((int)id);
            if (post.Data == null)
            {
                return NotFound();
            }
            else
            {
                Post = post.Data as Post;
                var categoryData = await _categoryBusiness.GetCategoryById(Post.CategoryId);
                Category = categoryData;
                var customerData = await _customerBusiness.GetById(Post.PostOwnerId);
                Customer = customerData;
                Comments = Post.Comments as List<Comment>;
            }
            return Page();
        }
    }
}
