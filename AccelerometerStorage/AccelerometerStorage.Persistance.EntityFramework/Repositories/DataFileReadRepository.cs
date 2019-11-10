using AccelerometerStorage.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AccelerometerStorage.Persistance.EntityFramework
{
    internal sealed class DataFileReadRepository : BaseReadRepository<DataFile>
    {
        public DataFileReadRepository(StorageContext context)
            : base(context)
        {
        }

        protected override IQueryable<DataFile> DecorateEntities(IQueryable<DataFile> entities)
            => entities.Include(DataFile.Expressions.User);
    }
}
