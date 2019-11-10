using System.IO;
using System.Threading.Tasks;
using AccelerometerStorage.Business;
using CSharpFunctionalExtensions;
using EnsureThat;

namespace AccelerometerStorage.Infrastructure
{
    internal class FileStorageService : IFileStorageService
    {
        public readonly StorageSettings settings;

        public FileStorageService(StorageSettings settings)
        {
            EnsureArg.IsNotNull(settings);

            this.settings = settings;
        }

        public async Task SaveFile(SaveFileCommand command)
        {
            EnsureArg.IsNotNull(command);

            var dirpath = Path.Combine(settings.FileStorageRootPath,  command.Username);
            Directory.CreateDirectory(dirpath);

            using (var fileStream = new FileStream(Path.Combine(dirpath, command.DataFileId.ToString()), FileMode.Create))
            {
                command.Content.Position = 0;
                await command.Content.CopyToAsync(fileStream);
            }
        }

        public Result<Business.FileInfo> GetFileInfo(GetFileQuery query)
        {
            var dirpath = Path.Combine(settings.FileStorageRootPath, query.Username);
            var filepath = Path.Combine(dirpath, query.DataFileId.ToString());

            var result = Result.SuccessIf(
                Directory.Exists(dirpath) && File.Exists(filepath),
                "Invalid file path")
                .Map(() => new Business.FileInfo()
                {
                    Filename = query.Username + "\\" + query.DataFileId.ToString(),
                    Filepath = filepath
                });

            return result;
        }
    }
}
