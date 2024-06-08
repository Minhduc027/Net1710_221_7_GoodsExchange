using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GoodsExchange.RazorWebApp.Pages.Offer
{
    public class IndexModel : PageModel
    {
        private readonly IOfferBusiness _offerBusiness;

        public IndexModel(IOfferBusiness offerBusiness)
        {
            _offerBusiness = offerBusiness;
        }

        public List<GoodsExchange.data.Models.Offer> Offers { get; set; }

        public async Task OnGetAsync()
        {
            var result = await _offerBusiness.GetAll();
            if (result.Status == 0)
            {
                Offers = result.Data as List<GoodsExchange.data.Models.Offer>;
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _offerBusiness.Delete(id);
            if (result.Status == 0)
            {
                return RedirectToPage();
            }
            return Page();
        }
    }
}
