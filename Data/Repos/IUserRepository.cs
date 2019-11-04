using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public interface IUserRepository 
    {
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id);
        Task<IEnumerable<object>> GetNumOfUsersByGender();
        Task<IEnumerable<object>> GetNumOfUsersByCountry();
    }
}
