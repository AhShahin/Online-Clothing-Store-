using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public interface IItemRepository
    {
        Task<object> GetTopSellingItem(ItemParams itemParams);
        Task<PagedList<object>> GetItemsWithLowQty(ItemParams itemParams);
        Task<PagedList<object>> GetNumOfItemsBySize(ItemParams itemParams);

        Task<PagedList<Item>> GetItems(ItemParams itemParams);
        Task<Item> GetItem(int id);
    }
}
