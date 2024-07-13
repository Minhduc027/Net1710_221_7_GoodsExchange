using Business.Repository;
using GoodsExchange.business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Equipments_CaoKhaSuong.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ICustomerBusiness _customerBusiness;

        public LoginModel(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        [BindProperty]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var email = Email;
                var password = Password;
                var user = await _customerBusiness.GetById(email, password);
                if (user != null)
                {
                    HttpContext.Session.SetString("UserId", user.CustomerId.ToString());
                    return 
                }
                else
                {
                    ViewData["message"] = "This account didn't exist";
                    return Page();
                }
            }
        }
    }
}
