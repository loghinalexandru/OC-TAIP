using AccelerometerStorage.Domain;
using CSharpFunctionalExtensions;
using System.IO;
using System.Threading.Tasks;

namespace AccelerometerStorage.Business
{
    public interface IStorageService
    {
        Task<Result> AddData(AddDataCommand command);

        Task<MemoryStream> GetData(GetFilteredDataQuery query);

        Task<MemoryStream> GetLatest(GetFilteredDataQuery query);
    }
}