using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GoodsExchange.data.Models;
using GoodsExchange.business.Interface;
using GoodsExchange.business;

namespace GoodsExchange.RazorWebApp.Pages.PostPage
{
    public class CreateModel : PageModel
    {
        private readonly IPostBusiness _postBusiness;
        private readonly ICategoryBusiness _categoryBusiness;
        private readonly ICustomerBusiness _customerBusiness;
        public CreateModel()
        {
            _postBusiness ??= new PostBusiness();
            _categoryBusiness ??= new CategoryBusiness();
            _customerBusiness ??= new CustomerBusiness();
        }

        public async Task<IActionResult> OnGet()
        {
            Post = new Post
            {
                CreateDate = DateTime.Now
            };
            var categorylist = await _categoryBusiness.GetAllCategory();
            List<Category?> categories = categorylist.Data as List<Category>;
            var customerlist = await _customerBusiness.GetAllCustomer();
            List<Customer?> customers = customerlist.Data as List<Customer?>;
        ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
        ViewData["PostOwnerId"] = new SelectList(customers, "CustomerId", "Name");
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _postBusiness.Create(Post);

            return RedirectToPage("./Index");
        }
    }
}
