using Microsoft.EntityFrameworkCore;
using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Data.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext Context;

        public UserRepository(DataContext context) {
            Context = context;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = Context.Users
              .AsQueryable().AsNoTracking();

            if (!string.IsNullOrEmpty(userParams.SearchTerm))
            {
                users = users.Where(u => u.FirstName.Contains(userParams.SearchTerm) || u.LastName.Contains(userParams.SearchTerm));
            }

            if (!string.IsNullOrEmpty(userParams.Gender))
            {
                users = users.Where(u => u.Gender.ToString() == userParams.Gender);
            }

            if (!string.IsNullOrEmpty(userParams.Type))
            {
                users = users.Where(u => u.Type == userParams.Type);
            }

            if (userParams.MinAge != 0 || userParams.MaxAge != 0)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DoB >= minDob && u.DoB <= maxDob);
            }

            var columnsMap = new Dictionary<string, Expression<Func<User, object>>>()
            {
                ["FirstName"] = u => u.FirstName,
                ["LastName"] = u => u.LastName,
                ["Type"] = u => u.Type,
                ["Gender"] = u => u.Gender,
                ["CreatedAt"] = u => u.CreatedAt
            };
            users = users.ApplyOrdering(userParams, columnsMap);

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<User> GetUser(int id)
        {
            return await Context.Users
              .Include(u => u.Addresses)
              .Include(u => u.Orders)
              .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<object>> GetNumOfUsersByGender()
        {
            var usersBYGender = from u in Context.Users
                            group u by u.Gender into g
                            select new
                            {
                                Name = Enum.GetName(typeof(Gender), g.Key),
                                count = g.Count(),
                            };

            return await usersBYGender.ToListAsync();
        }

        public async Task<IEnumerable<object>> GetNumOfUsersByCountry()
        {
            var countries = from u in Context.Users
                             join a in Context.Addresses on u.Id equals a.UserId
                             where a.IsShippingAddress == true
                             group a by a.Country into g
                             select new {
                                 Name = g.Key,
                                 count = g.Count(),
                             };

            return await countries.ToListAsync();
        }
    }
}
