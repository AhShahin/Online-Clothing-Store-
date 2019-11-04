using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly DataContext Context;

        public CategoryRepository(DataContext context) {
            Context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await Context.Categories
              .Include(c => c.Items)
              .AsNoTracking()
              .ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await Context.Categories
              .Include(c => c.Items)
              .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
