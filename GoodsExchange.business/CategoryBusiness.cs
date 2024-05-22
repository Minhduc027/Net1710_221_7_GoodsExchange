using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;
using GoodsExchange.data.DAO;
using GoodExchange.commons;
namespace GoodsExchange.business
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly categoryDAO  _categoryDAO;
        public CategoryBusiness()
        { _categoryDAO = new categoryDAO(); }
        public async Task<IGoodsExchangeResult> CreateCategory(Category category)
        {
            try
            {
                int result = await _categoryDAO.CreateAsync(category);
                if(result > -1)
                {
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.CREATE_SUCCESS, category);
                }else
                {
                    return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK);
                }
            } catch (Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> DeleteCategory(int categoryId)
        {
            try
            {
                var category = _categoryDAO.GetById(categoryId);
                if (category == null)
                {
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.NOT_FOUND);
                }
                bool result = await _categoryDAO.RemoveAsync(category);

                return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS, category);
            } catch (Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> GetAllCategory()
        {
            try
            {
                var result = await _categoryDAO.GetAllAsync();
                if(result == null)
                {
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS_EMPTY);
                }
                else
                {
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS, result);
                }
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }

        public async Task<IGoodsExchangeResult> UpdateCategory(Category category)
        {
            try
            {
                var categoryModel = await _categoryDAO.GetByIdAsync(category.CategoryId);
                if(categoryModel == null)
                {
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS_EMPTY);
                }
                categoryModel.CategoryName = category.CategoryName;
                int result = await _categoryDAO.UpdateAsync(categoryModel);
                if(result > 0)
                    return new GoodsExchangeResult(Constant.SUCCESS_STATUS, Constant.SUCCESS, categoryModel);
                else
                {
                    return new GoodsExchangeResult(Constant.NOTHING_WERE_CHANGED_STATUS, Constant.ERROR_EXECUTING_TASK, "Detele failed!");
                }
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(Constant.FAILED_STATUS, Constant.ERROR_EXECUTING_TASK + ex.Message);
            }
        }
    }
}
