using OnlineStore.Data.Repos;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.UOW
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DataContext Context;
        private bool disposed = false;

        public UnitOfWork(DataContext context)
        {
            Context = context;
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public async virtual Task<bool> SaveAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
