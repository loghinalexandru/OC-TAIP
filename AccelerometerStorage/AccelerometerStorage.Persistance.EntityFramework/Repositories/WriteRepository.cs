using AccelerometerStorage.Business;
using AccelerometerStorage.Domain;
using EnsureThat;
using System.Threading.Tasks;

namespace AccelerometerStorage.Persistance.EntityFramework
{
    internal class WriteRepository<T> : IWriteRepository<T>
        where T : Entity
    {
        private readonly StorageContext context;
        private readonly IQueueHelper _queueHelper;

        public WriteRepository(StorageContext context, IQueueHelper queueHelper)
        {
            EnsureArg.IsNotNull(context);

            this.context = context;
            this._queueHelper = queueHelper;
        }

        public async Task Create(T entity)
        {
            await context.Set<T>().AddAsync(entity);

            foreach (var entityDomainEvent in entity.DomainEvents)
            {
                _queueHelper.EnqueueMessage(entityDomainEvent.Message);
            }
        }

        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }
    }
}