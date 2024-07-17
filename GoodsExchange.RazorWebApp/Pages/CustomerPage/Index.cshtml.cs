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
using GoodExchange.commons;

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

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int pageIndex = 1, int pageSize = 10, string searchString = null)
    {
        CurrentFilter = searchString;
        var result = await _customerBusiness.SearchCustomersByName(searchString);

        if (result.Status == Constant.SUCCESS_STATUS)
        {
            var customers = result.Data as List<Customer>;

            Customer = customers.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            // Đặt giá trị cho CurrentPage và TotalPages
            CurrentPage = pageIndex;
            TotalPages = (int)Math.Ceiling(customers.Count / (double)pageSize);
        }
        else
        {
            // Xử lý lỗi nếu cần
            // Ví dụ: ViewBag.ErrorMessage = result.Message;
        }
    }
    }
}
