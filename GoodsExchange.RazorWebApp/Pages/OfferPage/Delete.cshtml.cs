using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoodsExchange.data.Models;
using GoodsExchange.business.Interface;
using GoodsExchange.RazorWebApp.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GoodsExchange.RazorWebApp.Pages.OfferPage
{
    public class DeleteModel : PageModel
    {
        private readonly IOfferBusiness _offerBusiness;
        private readonly IHubContext<OfferHub> _hubContext;

        public DeleteModel(IOfferBusiness offerBusiness, IHubContext<OfferHub> hubContext)
        {
            _offerBusiness = offerBusiness;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Offer Offer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _offerBusiness.GetById(id.Value);
            if (result.Status != 0 || result.Data == null)
            {
                return NotFound();
            }
            Offer = result.Data as Offer;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _offerBusiness.Delete(id.Value);
            if (result.Status == 0)
            {
                await _hubContext.Clients.All.SendAsync("OfferUpdated");
                return RedirectToPage("./Index");
            }
            ModelState.AddModelError(string.Empty, result.Message);
            return Page();
        }
    }
}
