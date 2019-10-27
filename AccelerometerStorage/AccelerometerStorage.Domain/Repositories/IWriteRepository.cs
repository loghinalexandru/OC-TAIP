using System.Threading.Tasks;

namespace AccelerometerStorage.Domain
{
    public interface IWriteRepository<T>
        where T : Entity
    {
        Task Create(T entity);

        Task Commit();
    }
}
