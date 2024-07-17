using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using GoodsExchange.RazorWebApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using GoodsExchange.business;

namespace GoodsExchange.RazorWebApp.Pages.OfferDetailPage
{
    public class EditModel : PageModel
    {
        private readonly IOfferDetailBusiness _offerDetailBusiness;
        private readonly IHubContext<OfferDetailHub> _hubContext;
        private readonly IOfferBusiness _offerBusiness;
        private readonly IPostBusiness _postBusiness;

        public EditModel(IOfferDetailBusiness offerDetailBusiness, IHubContext<OfferDetailHub> hubContext, IOfferBusiness offerBusiness, IPostBusiness postBusiness)
        {
            _offerDetailBusiness = offerDetailBusiness;
            _hubContext = hubContext;
            _offerBusiness = offerBusiness;
            _postBusiness = postBusiness;
        }

        [BindProperty]
        public OfferDetail OfferDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailResult = await _offerDetailBusiness.GetById(id.Value);
            if (detailResult.Status != 0 || detailResult.Data == null)
            {
                return NotFound();
            }
            OfferDetail = detailResult.Data as OfferDetail;

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

            var result = await _offerDetailBusiness.Update(OfferDetail);
            if (result.Status >= 0)
            {
                await _hubContext.Clients.All.SendAsync("OfferDetailUpdated");
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError(string.Empty, result.Message);
            return Page();
        }
    }
}
