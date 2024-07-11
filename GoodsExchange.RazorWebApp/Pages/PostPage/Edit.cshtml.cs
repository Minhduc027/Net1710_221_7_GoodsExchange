using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoodsExchange.data.Models;
using GoodsExchange.business.Interface;
using GoodsExchange.business;

namespace GoodsExchange.RazorWebApp.Pages.PostPage
{
    public class EditModel : PageModel
    {
        private readonly IPostBusiness _postBusiness;
        private readonly ICategoryBusiness _categoryBusiness;
        private readonly ICustomerBusiness _customerBusiness;

        public EditModel()
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

            var post =  await _postBusiness.GetById((int)id);
            if (post == null)
            {
                return NotFound();
            }
            Post = post.Data as Post;
            var categorylist = await _categoryBusiness.GetAllCategory();
            List<Category> categories = categorylist.Data as List<Category>;
            var customerlist = await _customerBusiness.GetAllCustomer();
            List<Customer> customers = customerlist.Data as List<Customer>;
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            ViewData["PostOwnerId"] = new SelectList(customers, "CustomerId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            

            try
            {
                await _postBusiness.Update(Post);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await PostExists(Post.PostId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> PostExists(int id)
        {
            var post = await _postBusiness.GetById(id);
            return post.Data != null;
        }
    }
}
