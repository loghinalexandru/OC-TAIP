using System.Threading.Tasks;
using AccelerometerStorage.Domain;
using EnsureThat;

namespace AccelerometerStorage.Persistance.EntityFramework
{
    internal class WriteRepository<T> : IWriteRepository<T>
        where T : Entity
    {
        private readonly StorageContext context;

        public WriteRepository(StorageContext context)
        {
            EnsureArg.IsNotNull(context);

            this.context = context;
        }

        public async Task Create(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }
    }
}
