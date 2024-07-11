using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GoodsExchange.data.Models;
using GoodsExchange.business.Interface;
using GoodsExchange.business;

namespace GoodsExchange.RazorWebApp.Pages.OfferPage
{
    public class IndexModel : PageModel
    {
        private readonly IOfferBusiness offer;
        public IndexModel()
        {
            offer ??= new OfferBusiness();
        }

        public IList<Offer> Offers { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await offer.GetAll();
            if (result != null && result.Status == 0 && result.Data != null)
            {
                Offers = result.Data as List<Offer>;
            }
        }
    }
}
