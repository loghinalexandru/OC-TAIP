using AccelerometerStorage.Domain;

namespace AccelerometerStorage.Persistance.EntityFramework
{

    internal sealed class ReadRepository<T> : BaseReadRepository<T>
        where T : Entity
    {
        public ReadRepository(StorageContext context)
            : base(context)
        {
        }
    }
}
