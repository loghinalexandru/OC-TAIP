using AccelerometerStorage.Domain;
using AccelerometerStorage.Domain.Events;
using CSharpFunctionalExtensions;
using EnsureThat;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace AccelerometerStorage.Business
{
    internal class StorageService : IStorageService
    {
        private readonly IFileStorageService fileStorageService;
        private readonly IWriteRepository<DataFile> dataFileWriteRepository;
        private readonly IReadRepository<DataFile> dataFileReadRepository;
        private readonly IUserService userService;

        public StorageService(
            IFileStorageService fileStorageService,
            IWriteRepository<DataFile> dataFileWriteRepository,
            IReadRepository<DataFile> dataFileReadRepository,
            IUserService userService)
        {
            EnsureArg.IsNotNull(fileStorageService);
            EnsureArg.IsNotNull(dataFileWriteRepository);
            EnsureArg.IsNotNull(dataFileReadRepository);
            EnsureArg.IsNotNull(userService);

            this.fileStorageService = fileStorageService;
            this.dataFileWriteRepository = dataFileWriteRepository;
            this.dataFileReadRepository = dataFileReadRepository;
            this.userService = userService;
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

            var modelResult = await dataFileReadRepository.Find(file =>
                file.User.Username == command.Username && file.FileType == FileType.Model);

            return userResult
                .Map(u =>
                {
                    var newFile = DataFile.Create(command.Filename, u, command.FileType);
                    if (command.FileType == FileType.Input && modelResult.FirstOrDefault() != null && newFile.IsSuccess)
                    {
                        newFile.Value.AddDomainEvent(new NewAccelerometerDataEvent {Message = command.Username});
                    }

                    return newFile;
                })
                .Map(df =>
                {
                    var saveFileCommand = new SaveFileCommand(command.ContentStream, command.Filename, command.Username,
                        df.Value.Id);
                    fileStorageService.SaveFile(saveFileCommand);
                    return df.Value;
                })
                .Map(df => dataFileWriteRepository.Create(df))
                .Map(_ => dataFileWriteRepository.Commit());
        }

        public async Task<MemoryStream> GetData(GetFilteredDataQuery query)
        {
            EnsureArg.IsNotNull(query);

            var dataFiles = string.IsNullOrEmpty(query.Username)
                ? await dataFileReadRepository.GetAll()
                : await dataFileReadRepository.Find(df =>
                    df.User.Username == query.Username && df.UploadedAt >= query.StartingFrom);

            var files = dataFiles
                .Where(df => df.FileType == query.FileType)
                .Select(df => fileStorageService.GetFileInfo(
                    new GetFileQuery(df.User.Username, df.Id, query.FileType)))
                .Where(dfr => dfr.IsSuccess)
                .Select(dfr => dfr.Value);

            using var memoryStream = new MemoryStream();
            using var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

            foreach (var file in files)
            {
                zipArchive.CreateEntryFromFile(file.Filepath, file.Filename);
            }

            return memoryStream;
        }

        public async Task<MemoryStream> GetLatest(GetFilteredDataQuery query)
        {
            var memoryStream = new MemoryStream();

            var files = await dataFileReadRepository.Find(df => df.User.Username == query.Username);

            var latestFile = files.OrderByDescending(file => file.UploadedAt).FirstOrDefault();

            if (latestFile == null)
            {
                return memoryStream;
            }

            var (_, isFailure, value) =
                fileStorageService.GetFileInfo(new GetFileQuery(query.Username, latestFile.Id, query.FileType));

            if (isFailure)
            {
                return memoryStream;
            }

            var fileStream = File.OpenRead(value.Filepath);

            fileStream.CopyTo(memoryStream);

            return memoryStream;
        }
    }
}