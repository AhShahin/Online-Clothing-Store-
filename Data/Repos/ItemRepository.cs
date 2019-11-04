using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Data.Repos
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext Context;

        public ItemRepository(DataContext context) {
            Context = context;
        }

        public async Task<Item> GetItem(int id)
        {
            return await Context.Items
              .Include(i => i.Category)
              .Include(i => i.Item_Details)
               .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<PagedList<Item>> GetItems(ItemParams itemParams)
        {
            var items = Context.Items
              .Include(i => i.Category)
              .Include(i => i.Item_Details)
                .AsQueryable().AsNoTracking();

            if (!string.IsNullOrEmpty(itemParams.SearchTerm))
            {
                items = items.Where(i => i.Description.Contains(itemParams.SearchTerm));
            }

            if (!string.IsNullOrEmpty(itemParams.Name))
            {
                items = items.Where(i => i.Name.Contains(itemParams.Name));
            }

            if (!string.IsNullOrEmpty(itemParams.CategoryName))
            {
                items = items.Where(i => i.Category.Name == itemParams.CategoryName);
            }

            var columnsMap = new Dictionary<string, Expression<Func<Item, object>>>()
            {
                ["Name"] = i => i.Name,
                ["Viewed"] = i => i.Viewed,
                ["CategoryName"] = i => i.Category.Name,
            };
            items = items.ApplyOrdering(itemParams, columnsMap);

            return await PagedList<Item>.CreateAsync(items, itemParams.PageNumber, itemParams.PageSize);
        }

        public async Task<PagedList<object>> GetItemsWithLowQty(ItemParams itemParams)
        {
            var items = from i in Context.Items 
                        join id in Context.Item_Details on i.Id equals id.ItemId 
                        where id.Quantity <= 3
                        group i by i.Name into g
                        select new
                        {
                            Name = g.Key,
                            count = g.Count(),
                        };

            return await PagedList<object>.CreateAsync(items, itemParams.PageNumber, itemParams.PageSize);
        }

        public async Task<PagedList<object>> GetNumOfItemsBySize(ItemParams itemParams)
        {
            var items = from i in Context.Items
                        join id in Context.Item_Details on i.Id equals id.ItemId
                        group id by new { id.Size, i.Name } into g
                        select new
                        {
                            item = g.Select(x => x.Item.Name).First(),
                            Name = Enum.GetName(typeof(Size), g.Key.Size),
                            count = g.Count(),
                        };

            return await PagedList<object>.CreateAsync(items, itemParams.PageNumber, itemParams.PageSize);
        }

        public async Task<object> GetTopSellingItem(ItemParams itemParams)
        {
            return await Context.Items.Include(i => i.OrderItems).
                SelectMany(u => u.OrderItems).
                GroupBy(oi => oi.ItemId).
                Select(g => new { Name = g.Select(x => x.Item.Name).First(), Count = g.Count() })
                                .OrderByDescending(g => g.Count)
                                .Take(1).SingleAsync();
        }
    }
}
