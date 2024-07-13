using GoodsExchange.business.Interface;
using GoodExchange.commons;
using GoodsExchange.data;
using GoodsExchange.data.Models;
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
        //private readonly CustomerDAO _customerDAO;
        private readonly UnitOfWork unitOfWork;
        public CustomerBusiness()
        {
            //_customerDAO = new CustomerDAO();
            unitOfWork ??= new UnitOfWork();
        }

        public async Task<IGoodsExchangeResult> CreateCustomer(Customer customer)
        {
            try
            {
                //await _customerDAO.CreateAsync(customer);
                await unitOfWork.CustomerRepository.CreateAsync(customer);

                return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.CREATE_SUCCESS, customer);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> DeleteCustomer(int customerId)
        {
            try
            {
                //var customer = await _customerDAO.GetByIdAsync(customerId);
                var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.NOT_FOUND);
                }

                //await _customerDAO.RemoveAsync(customer);
                await unitOfWork.CustomerRepository.RemoveAsync(customer);

                return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.DELETED, customer);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetAllCustomer()
        {
            try
            {
                //var customers = await _customerDAO.GetAllAsync();
                var customers = await unitOfWork.CustomerRepository.GetAllAsync();
                return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS + "Get all customers.", customers);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetCustomerById(int customerId)
        {
            try
            {
                var result = await unitOfWork.CustomerRepository.GetByIdAsync(customerId);
                if (result == null)
                {
                    return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.NOT_FOUND);
                }
                return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS + "Get customer by Id.", result);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<Customer> GetById(int customerId)
        {
            return await unitOfWork.CustomerRepository.GetByIdAsync(customerId);
        }

        public async Task<IGoodsExchangeResult> UpdateCustomer(Customer customer)
        {
            try
            {
                //var existingCustomer = await _customerDAO.GetByIdAsync(customer.CustomerId);
                var existingCustomer = await unitOfWork.CustomerRepository.GetByIdAsync(customer.CustomerId);
                if (existingCustomer == null)
                {
                    return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.NOT_FOUND);
                }

                existingCustomer.Name = customer.Name;
                existingCustomer.Address = customer.Address;
                existingCustomer.Dob = customer.Dob;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Gender = customer.Gender;
                existingCustomer.Email = customer.Email;

                //await _customerDAO.UpdateAsync(existingCustomer);
                await unitOfWork.CustomerRepository.UpdateAsync(existingCustomer);

                return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS + "Customer updated!", existingCustomer);
            }
            catch (Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }
        public async Task<Customer> GetByEmail(string email, string phone)
        {
            var userInfo = await unitOfWork.CustomerRepository.GetByEmail(email, phone);
            if(userInfo == null)
            {
                return null;
            }
            return userInfo;
        }
    }
}
