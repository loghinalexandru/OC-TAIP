using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using AccelerometerStorage.Domain;
using CSharpFunctionalExtensions;
using EnsureThat;

namespace AccelerometerStorage.Business
{
    internal class StorageService : IStorageService
    {
        private readonly IFileStorageService fileStorageService;
        private readonly IWriteRepository<DataFile> dataFileWriteRepository;
        private readonly IReadRepository<DataFile> dataFileReadRepository;
        private readonly IUserService userService;
        private readonly IQueueHelper queueHelper;

        public StorageService(
            IFileStorageService fileStorageService, 
            IWriteRepository<DataFile> dataFileWriteRepository,
            IReadRepository<DataFile> dataFileReadRepository,
            IUserService userService,
            IQueueHelper queueHelper
            )
        {
            EnsureArg.IsNotNull(fileStorageService);
            EnsureArg.IsNotNull(dataFileWriteRepository);
            EnsureArg.IsNotNull(dataFileReadRepository);
            EnsureArg.IsNotNull(userService);

            this.fileStorageService = fileStorageService;
            this.dataFileWriteRepository = dataFileWriteRepository;
            this.dataFileReadRepository = dataFileReadRepository;
            this.userService = userService;
            this.queueHelper = queueHelper;
        }

        public async Task<Result> AddData(AddDataCommand command)
        {
            EnsureArg.IsNotNull(command);

            var userResult = await userService.GetByUsername(command.Username)
                .ToResult("User not found");
            userResult = await userResult.OnFailureCompensate(
                  () => command.FileType == FileType.Input
                            ? userService.AddUser(new AddUserCommand(command.Username))
                            : Task.FromResult(userResult));

            queueHelper.EnqueueMessage(userResult.Value.Username);

            return userResult
                .Map(u => DataFile.Create(command.Filename, u, command.FileType))
                .Map(df =>
                {
                    var saveFileCommand = new SaveFileCommand(command.ContentStream, command.Filename, command.Username, df.Value.Id);
                    fileStorageService.SaveFile(saveFileCommand);
                    return df.Value;
                })
                .Map(df => dataFileWriteRepository.Create(df))
                .Map(_ => dataFileWriteRepository.Commit());
        }

        public async Task<MemoryStream> GetData(GetFilteredDataQuery query)
        {
            EnsureArg.IsNotNull(query);

            var dataFiles = query.Username == null || query.Username.Equals("")
                ? await dataFileReadRepository.GetAll()
                : await dataFileReadRepository.Find(df => df.User.Username == query.Username);

            var files = dataFiles
                .Where(df => df.FileType == query.FileType)
                .Select(df => fileStorageService.GetFileInfo(
                    new GetFileQuery(df.User.Username, df.Id, query.FileType)))
                .Where(dfr => dfr.IsSuccess)
                .Select(dfr => dfr.Value);

            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        zipArchive.CreateEntryFromFile(file.Filepath, file.Filename);
                    }
                }

                return memoryStream;
            }
        }
    }
}
