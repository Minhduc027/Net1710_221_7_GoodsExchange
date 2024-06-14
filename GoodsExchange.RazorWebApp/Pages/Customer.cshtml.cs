using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoodExchange.commons;

namespace GoodsExchange.RazorWebApp.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerBusiness _customerBusiness;
        public string ErrorCode { get; set; }
        [BindProperty]
        public Customer Customer { get; set; } = default;


        public CustomerModel(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;            

        }

        public List<Customer> Customers { get; set; }

        public async Task OnGetAsync()
        {
            var result = await _customerBusiness.GetAllCustomer();
            if (result.Status == Constant.SUCCESS_STATUS && result.Data is List<Customer>)
            {
                Customers = (List<Customer>)result.Data;
            }            
        }
        public async Task<IActionResult> OnPostAsync()
        {            
            var result = await _customerBusiness.CreateCustomer(this.Customer);
            if (result != null)
            {
                return RedirectToPage("/Customer");
            }
            else
            {
                this.ErrorCode = "SystemError";
                return Page();
            }
        }
        public async Task<IActionResult> OnGetDelete(int customerId)
        {
            var result = await _customerBusiness.DeleteCustomer(customerId);
            if (result.Status == Constant.SUCCESS_STATUS)
            {
                return RedirectToPage("/Customer");
            }
            else
            {
                ErrorCode = "SystemError";
                return Page();
            }
        }
        public async Task<IActionResult> OnUpdateCustomer(int id, string name, string address, DateOnly dob, string phone, int gender, string email)
        {
            var result = await _customerBusiness.GetCustomerById(id);
            var customer = (Customer)result.Data;
            customer.Name = name;
            customer.Address = address;
            customer.Dob = dob;
            customer.Phone = phone;
            customer.Gender = gender;
            customer.Email = email;
            await _customerBusiness.UpdateCustomer(customer);
            return RedirectToPage();
        }
    }
}
