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

namespace GoodsExchange.RazorWebApp.Pages.CustomerPage
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerBusiness _customerBusiness;

        public DeleteModel()
        {
            _customerBusiness ??= new CustomerBusiness();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerBusiness.GetCustomerById((int)id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer.Data as Customer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerBusiness.GetCustomerById((int)id);
            if (customer != null)
            {
                Customer = customer.Data as Customer;
                await _customerBusiness.DeleteCustomer((int)id);
            }

            return RedirectToPage("./Index");
        }
    }
}
