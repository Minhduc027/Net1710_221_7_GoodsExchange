using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoodExchange.commons;

namespace GoodsExchange.RazorWebApp.Pages
{
    public class PostModel : PageModel
    {
        private readonly IPostBusiness _postBusiness;
        private readonly ICategoryBusiness _categoryBusiness;
        public string ErrorCode { get; set; }
        [BindProperty]
        public Post Post { get; set; } = default;


        public PostModel(IPostBusiness postBusiness,
            ICategoryBusiness categoryBusiness
            )
        {
            _postBusiness = postBusiness;
            _categoryBusiness = categoryBusiness;
           
        }

        public List<Post> Posts { get; set; }
        public List<Category> Categories { get; set; }
        public List<Comment> Comments { get; set; }

        public async Task OnGetAsync()
        {
            var result = await _postBusiness.GetAll();
            if (result.Status == Constant.SUCCESS_STATUS && result.Data is List<Post>)
            {
                Posts = (List<Post>)result.Data;
            }
            var category = await _categoryBusiness.GetAllCategory();
            if (category.Status == Constant.SUCCESS_STATUS && category.Data is List<Category>)
            {
                Categories = (List<Category>)category.Data;
            }            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            this.Post.CreateDate = DateTime.Now;
            var result = await _postBusiness.Create(this.Post);
            if (result != null)
            {
                return RedirectToPage("/Post");
            }
            else
            {
                this.ErrorCode = "SystemError";
                return Page();
            }
        }
        public async Task<IActionResult> OnGetDelete(int postId)
        {
            var result = await _postBusiness.Delete(postId);
            if (result.Status == Constant.SUCCESS_STATUS)
            {
                return RedirectToPage("/Post");
            }
            else
            {
                ErrorCode = "SystemError";
                return Page();
            }
        }
        public async Task<IActionResult> OnPostUpdatePost(int id, string title, string description, string location)
        {
            var result = await _postBusiness.GetById(id);
            var post = (Post)result.Data;
            post.Title = title;
            post.Description = description;
            post.Address = location;
            await _postBusiness.Update(post);
            return RedirectToPage();
        }
    }
}
