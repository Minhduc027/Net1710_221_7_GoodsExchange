using GoodsExchange.data.Base;
using GoodsExchange.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.data.Repository
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository() { }
    }
}
