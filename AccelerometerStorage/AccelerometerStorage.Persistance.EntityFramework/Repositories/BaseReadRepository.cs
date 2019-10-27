using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AccelerometerStorage.Common;
using AccelerometerStorage.Domain;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.EntityFrameworkCore;

namespace AccelerometerStorage.Persistance.EntityFramework
{
    internal abstract class BaseReadRepository<T> : IReadRepository<T>
        where T : Entity
    {
        private readonly IQueryable<T> entitiesSet;

        protected BaseReadRepository(StorageContext context)
        {
            EnsureArg.IsNotNull(context);

            this.entitiesSet = DecorateEntities(context.Set<T>().AsQueryable());
        }

        public Task<Maybe<T>> FindOne(Expression<Func<T, bool>> expression)
        {
            return entitiesSet.FirstOrDefaultAsync(expression).ToMaybe();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await entitiesSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entitiesSet.ToListAsync();
        }

        public Task<Maybe<T>> GetById(Guid id)
        {
            return entitiesSet.FirstOrDefaultAsync(e => e.Id == id).ToMaybe();
        }

        protected virtual IQueryable<T> DecorateEntities(IQueryable<T> entities) => entities;
    }
}
