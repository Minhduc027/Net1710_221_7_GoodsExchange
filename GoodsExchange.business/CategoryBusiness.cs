using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.business
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private static string CREATE_SUCCESS = "Create successfully!";
        private static string ERROR_EXECUTING_TASK = "Error while executing task: ";
        private static string NOT_FOUND = "Category not found";
        private static string DELETED = "Category deleted!";
        private static string SUCCESS = "Task executed successfully";
        private readonly Net1710_221_7_GoodsExchangeContext _context;
        public CategoryBusiness(Net1710_221_7_GoodsExchangeContext context)
        { _context = context; }
        public async Task<IGoodsExchangeResult> CreateCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                
                return new GoodsExchangeResult(0, CREATE_SUCCESS, category);
            } catch (Exception ex)
            {
                return new GoodsExchangeResult(-1,ERROR_EXECUTING_TASK + $"{ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> DeleteCategory(int categoryId)
        {
            try
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null)
                {
                    return new GoodsExchangeResult(-1, NOT_FOUND);
                }
                _context.Remove(category);
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(1, DELETED, category);
            } catch (Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + $"{ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> GetAllCategory()
        {
            try
            {
                var result = await _context.Categories.ToListAsync();

                return new GoodsExchangeResult(0, SUCCESS, result);
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + $"{ex.Message}");
            }
        }

        public async Task<IGoodsExchangeResult> UpdateCategory(Category category)
        {
            try
            {
                var categoryModel = await _context.Categories.FindAsync(category.CategoryId);
                if(categoryModel == null)
                {
                    return new GoodsExchangeResult(-1, NOT_FOUND);
                }
                categoryModel.CategoryName = category.CategoryName;
                await _context.SaveChangesAsync();

                return new GoodsExchangeResult(0, SUCCESS, categoryModel);
            } catch(Exception ex)
            {
                return new GoodsExchangeResult(-1, ERROR_EXECUTING_TASK + $"{ex.Message}");
            }
        }
    }
}
