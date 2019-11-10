using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace AccelerometerStorage.Business
{
    public interface IFileStorageService
    {
        Task SaveFile(SaveFileCommand command);

        Result<FileInfo> GetFileInfo(GetFileQuery query);
    }
}
