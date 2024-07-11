using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GoodsExchange.data.Models;
using GoodsExchange.business.Interface;
using Microsoft.AspNetCore.SignalR;
using GoodsExchange.RazorWebApp.Hubs;

namespace GoodsExchange.RazorWebApp.Pages.OfferPage
{
    public class EditModel : PageModel
    {
        private readonly IOfferBusiness _offerBusiness;
        private readonly IHubContext<OfferHub> _hubContext;

        public EditModel(IOfferBusiness offerBusiness, IHubContext<OfferHub> hubContext)
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

            var customerResult = await _offerBusiness.GetAllCustomers();
            if (customerResult.Status == 0 && customerResult.Data != null)
            {
                ViewData["CustomerId"] = new SelectList(customerResult.Data as List<Customer>, "CustomerId", "Address");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await _offerBusiness.Update(Offer);
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
