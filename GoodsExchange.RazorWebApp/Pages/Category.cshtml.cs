using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoodExchange.commons;

namespace GoodsExchange.RazorWebApp.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ICategoryBusiness _categoryBusiness;
        public CategoryModel(ICategoryBusiness categoryBusiness)
        {
            _categoryBusiness = categoryBusiness;
        }
        public List<Category> Categories { get; set; }
        public async Task OnGetAsync() 
        { 
            var category = await _categoryBusiness.GetAllCategory();
            if (category.Status == Constant.SUCCESS_STATUS && category.Data is List<Category>)
            {
                Categories = (List<Category>)category.Data;
            }
        }
    }
}
