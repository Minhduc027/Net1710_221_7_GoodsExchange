using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using GoodsExchange.data.Models;
using GoodsExchange.business.Interface;
using GoodsExchange.RazorWebApp.Hubs;
using System.Threading.Tasks;

namespace GoodsExchange.RazorWebApp.Pages.OfferPage
{
    public class CreateModel : PageModel
    {
        private readonly IOfferBusiness _offerBusiness;
        private readonly IHubContext<OfferHub> _hubContext;

        public CreateModel(IOfferBusiness offerBusiness, IHubContext<OfferHub> hubContext)
        {
            _offerBusiness = offerBusiness;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _offerBusiness.GetAllCustomers();
            if (result.Status == 0 && result.Data != null)
            {
                ViewData["CustomerId"] = new SelectList(result.Data as List<Customer>, "CustomerId", "Address");
            }
            return Page();
        }

        [BindProperty]
        public Offer Offer { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _offerBusiness.Create(Offer);
            if (result.Status != 0)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }

            await _hubContext.Clients.All.SendAsync("OfferUpdated");

            return RedirectToPage("./Index");
        }
    }
}
