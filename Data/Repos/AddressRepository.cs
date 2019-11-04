using Microsoft.EntityFrameworkCore;
using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext Context;

        public AddressRepository(DataContext context) {
            Context = context;
        }

        public async Task<PagedList<Address>> GetAddresses(AddressParams addressParams)
        {
            var addresses = Context.Addresses
              .AsQueryable().AsNoTracking();

            if (!string.IsNullOrEmpty(addressParams.SearchTerm))
            {
                addresses = addresses.Where(u => u.StreetAddress.Contains(addressParams.SearchTerm));
            }

            if (!string.IsNullOrEmpty(addressParams.City))
            {
                addresses = addresses.Where(u => u.City == addressParams.City);
            }

            if (!string.IsNullOrEmpty(addressParams.Postcode))
            {
                addresses = addresses.Where(u => u.Postcode == addressParams.Postcode);
            }

            if (!string.IsNullOrEmpty(addressParams.Country))
            {
                addresses = addresses.Where(u => u.Country == addressParams.Country);
            }

            if (!string.IsNullOrEmpty(addressParams.State))
            {
                addresses = addresses.Where(u => u.State == addressParams.State);
            }

            var columnsMap = new Dictionary<string, Expression<Func<Address, object>>>()
            {
                ["StreetAddress"] = a => a.StreetAddress,
                ["Country"] = a => a.Country,
                ["State"] = a => a.State,
                ["City"] = a => a.City,
                ["IsBillingAddress"] = a => a.IsBillingAddress,
                ["IsShippingAddress"] = a => a.IsShippingAddress
            };
            addresses = addresses.ApplyOrdering(addressParams, columnsMap);

            return await PagedList<Address>.CreateAsync(addresses, addressParams.PageNumber, addressParams.PageSize);
        }

        public async Task<Address> GetAddress(int id)
        {
            return await Context.Addresses
              .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<object>> GetNumOfAddressesByCountry()
        {
            return await Context.Addresses
                                .GroupBy(a => a.Country)
                                .Select(g => new { Name = g.Key, Count = g.Count() })
                                .ToListAsync();
        }
    }
}
