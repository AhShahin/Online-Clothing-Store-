using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public interface IItemDetailsRepository
    {
        Task<IEnumerable<Item_details>> GetItems_details();
        Task<Item_details> GetItem_details(int id);
    }
}
