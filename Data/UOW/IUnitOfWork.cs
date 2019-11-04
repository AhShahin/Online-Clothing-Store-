using OnlineStore.Data.Repos;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.UOW
{
    public interface IUnitOfWork: IDisposable
    {
        int Save();
        Task<bool> SaveAsync();
    }
}
