using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace AccelerometerStorage.Business
{
    public interface IStorageService
    {
        Task<Result> AddData(AddDataCommand command);
    }
}
