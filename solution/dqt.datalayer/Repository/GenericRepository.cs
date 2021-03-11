﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dqt.datalayer.Repository
{
    public abstract class GenericRepository<T, DatabaseContext> : IRepository<T>
        where T : class
        where DatabaseContext : DbContext
    {
        protected DatabaseContext Context;

        public GenericRepository(DatabaseContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(Context.Set<T>().Where(predicate));
        }
    }
}
