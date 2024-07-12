using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoodsExchange.data.Models;
using GoodsExchange.business.Interface;
using Microsoft.AspNetCore.SignalR;
using GoodsExchange.RazorWebApp.Hubs;
using System.Threading.Tasks;

namespace GoodsExchange.RazorWebApp.Pages.OfferDetailPage
{
    public class DeleteModel : PageModel
    {
        private readonly IOfferDetailBusiness _offerDetailBusiness;
        private readonly IHubContext<OfferDetailHub> _hubContext;

        public DeleteModel(IOfferDetailBusiness offerDetailBusiness, IHubContext<OfferDetailHub> hubContext)
        {
            _offerDetailBusiness = offerDetailBusiness;
            _hubContext = hubContext;
        }

        [BindProperty]
        public OfferDetail OfferDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _offerDetailBusiness.GetById(id.Value);
            if (result.Status != 0 || result.Data == null)
            {
                return NotFound();
            }
            OfferDetail = result.Data as OfferDetail;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _offerDetailBusiness.Delete(id.Value);
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
