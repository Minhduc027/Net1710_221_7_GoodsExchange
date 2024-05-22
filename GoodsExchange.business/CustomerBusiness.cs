using GoodsExchange.business.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.business
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly Net1710_221_7_GoodsExchangeContext _context;
        public CustomerBusiness(Net1710_221_7_GoodsExchangeContext context)
        {
            _context = context;
        }

        public async Task<IGoodsExchangeResult> CreateCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, "Customer created successfully", customer);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Error creating customer: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> DeleteCustomer(int customerId)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(customerId);
                if (customer == null)
                {
                    return new GoodsExchangeResult(-1, "Customer not found");
                }

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, "Customer deleted successfully");
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Error deleting customer: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> GetAllCustomer()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                return new GoodsExchangeResult(0, "Customers retrieved successfully", customers);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Error retrieving customers: {ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> UpdateCustomer(Customer customer)
        {
            try
            {
                var existingCustomer = await _context.Customers.FindAsync(customer.CustomerId);
                if (existingCustomer == null)
                {
                    return new GoodsExchangeResult(-1, "Customer not found");
                }

                existingCustomer.Name = customer.Name;
                existingCustomer.Address = customer.Address;
                existingCustomer.Dob = customer.Dob;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Gender = customer.Gender;
                existingCustomer.Email = customer.Email;

                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, "Customer updated successfully", existingCustomer);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, $"Error updating customer: {ex.Message}");
            }
        }
    }
}
