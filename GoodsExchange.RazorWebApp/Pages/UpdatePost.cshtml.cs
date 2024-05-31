using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoodExchange.commons;

namespace GoodsExchange.RazorWebApp.Pages
{
    public class UpdatePostModel : PageModel
    {
        private readonly IPostBusiness _postBusiness;
        public UpdatePostModel(IPostBusiness postBusiness)
        {
                _postBusiness = postBusiness;
        }
        [BindProperty]
        public Post Post { get; set; } = default;
        public async Task OnGet(int postId)
        {
           var result = await _postBusiness.GetById(postId);
            if(result.Status == Constant.SUCCESS_STATUS && result.Data is Post)
            {
                Post = (Post)result.Data;
            }
        }
        public async Task<IActionResult> OnPost()
        {
            var result = Post;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _postBusiness.Update(result);
            return Redirect("/Post");
        }
    }
}
