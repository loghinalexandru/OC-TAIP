using System.Threading.Tasks;
using AccelerometerStorage.Common;
using AccelerometerStorage.Domain;
using CSharpFunctionalExtensions;
using EnsureThat;

namespace AccelerometerStorage.Business
{
    internal class StorageService : IStorageService
    {
        private readonly IFileStorageService fileStorageService;
        private readonly IReadRepository<User> userReadRepository;
        private readonly IWriteRepository<DataFile> dataFileWriteRepository;

        public StorageService(IFileStorageService fileStorageService, IReadRepository<User> userReadRepository, IWriteRepository<DataFile> dataFileWriteRepository)
        {
            EnsureArg.IsNotNull(fileStorageService);
            EnsureArg.IsNotNull(userReadRepository);
            EnsureArg.IsNotNull(dataFileWriteRepository);

            this.fileStorageService = fileStorageService;
            this.userReadRepository = userReadRepository;
            this.dataFileWriteRepository = dataFileWriteRepository;
        }

        public async Task<Result> AddData(AddDataCommand command)
        {
            EnsureArg.IsNotNull(command);

            var userResult = await userReadRepository.FindOne(u => u.Username == command.Username).ToResult("User not found");

            return userResult
                .Map(u => DataFile.Create(command.Filename, u))
                .Map(df =>
                {
                    var saveFileCommand = new SaveFileCommand(command.ContentStream, command.Filename, command.Username, df.Value.Id);
                    fileStorageService.SaveFile(saveFileCommand);
                    return df.Value;
                })
                .Map(df => dataFileWriteRepository.Create(df))
                .Map(_ => dataFileWriteRepository.Commit());
        }
    }
}
