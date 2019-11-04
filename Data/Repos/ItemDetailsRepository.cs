using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public class ItemDetailsRepository: IItemDetailsRepository
    {
        private readonly DataContext Context;

        public ItemDetailsRepository(DataContext context) {
            Context = context;
        }

        public async Task<Item_details> GetItem_details(int id)
        {
            return await Context.Item_Details.SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Item_details>> GetItems_details()
        {
            return await Context.Item_Details.AsNoTracking().ToListAsync();
        }
    }
}
