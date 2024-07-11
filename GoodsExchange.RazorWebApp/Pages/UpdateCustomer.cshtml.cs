using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoodExchange.commons;

namespace GoodsExchange.RazorWebApp.Pages
{
    public class UpdateCustomerModel : PageModel
    {
        private readonly ICustomerBusiness _customerBusiness;
        public UpdateCustomerModel(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }
        [BindProperty]
        public Customer Customer { get; set; } = default;
        public async Task OnGet(int customerId)
        {
            var result = await _customerBusiness.GetCustomerById(customerId);
            if (result.Status == Constant.SUCCESS_STATUS && result.Data is Customer)
            {
                Customer = (Customer)result.Data;
            }
        }
        public async Task<IActionResult> OnPost()
        {
            var result = Customer;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _customerBusiness.UpdateCustomer(result);
            return Redirect("/Customer");
        }
    }
}
