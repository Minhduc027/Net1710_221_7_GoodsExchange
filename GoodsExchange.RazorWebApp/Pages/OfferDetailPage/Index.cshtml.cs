using Microsoft.AspNetCore.Mvc.RazorPages;
using GoodsExchange.data.Models;
using GoodsExchange.business.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsExchange.RazorWebApp.Pages.OfferDetailPage
{
    public class IndexModel : PageModel
    {
        private readonly IOfferDetailBusiness _offerDetailBusiness;

        public IndexModel(IOfferDetailBusiness offerDetailBusiness)
        {
            _offerDetailBusiness = offerDetailBusiness;
        }

        public IList<OfferDetail> OfferDetails { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await _offerDetailBusiness.GetAll();
            if (result.Status == 0 && result.Data != null)
            {
                OfferDetails = result.Data as List<OfferDetail>;
            }
        }
    }
}
