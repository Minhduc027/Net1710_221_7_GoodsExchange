using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using GoodsExchange.RazorWebApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GoodsExchange.RazorWebApp.Pages.OfferDetailPage
{
    public class CreateModel : PageModel
    {
        private readonly IOfferDetailBusiness _offerDetailBusiness;
        private readonly IOfferBusiness _offerBusiness;
        private readonly IPostBusiness _postBusiness;
        private readonly IHubContext<OfferDetailHub> _hubContext;

        public CreateModel(IOfferDetailBusiness offerDetailBusiness, IOfferBusiness offerBusiness, IPostBusiness postBusiness, IHubContext<OfferDetailHub> hubContext)
        {
            _offerDetailBusiness = offerDetailBusiness;
            _offerBusiness = offerBusiness;
            _postBusiness = postBusiness;
            _hubContext = hubContext;
        }

        [BindProperty]
        public OfferDetail OfferDetail { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var offersResult = await _offerBusiness.GetAll();
            var postsResult = await _postBusiness.GetAll(); 

            if (offersResult.Status == 0 && offersResult.Data != null)
            {
                ViewData["OfferId"] = new SelectList(offersResult.Data as List<Offer>, "OfferId", "OfferId");
            }
            if (postsResult.Status > 0 && postsResult.Data != null)
            {
                ViewData["PostId"] = new SelectList(postsResult.Data as List<Post>, "PostId", "Title");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _offerDetailBusiness.Create(OfferDetail);
            if (result.Status != 0)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }

            await _hubContext.Clients.All.SendAsync("OfferDetailUpdated");

            return RedirectToPage("./Index");
        }
    }
}
