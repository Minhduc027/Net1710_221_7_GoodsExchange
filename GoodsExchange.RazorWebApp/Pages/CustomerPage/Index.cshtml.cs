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
    public class IndexModel : PageModel
    {
        private readonly ICustomerBusiness _customerBusiness;

        public IndexModel()
        {
            _customerBusiness ??= new CustomerBusiness();
        }

        public IList<Customer> Customer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var customerList = await _customerBusiness.GetAllCustomer();
            if (customerList.Data != null)
            {
                Customer = customerList.Data as List<Customer>;
            }
        }
    }
}
