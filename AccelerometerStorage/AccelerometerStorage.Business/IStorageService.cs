using System.IO;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace AccelerometerStorage.Business
{
    public interface IStorageService
    {
        Task<Result> AddData(AddDataCommand command);

        Task<MemoryStream> GetData(GetFilteredDataQuery query);
    }
}
