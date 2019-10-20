using System.Threading.Tasks;
using AccelerometerStorage.Common;
using AccelerometerStorage.Domain;
using CSharpFunctionalExtensions;
using EnsureThat;

namespace AccelerometerStorage.Business
{
    internal class StorageService : IStorageService
    {
        private readonly IReadRepository<User> userReadRepository;
        private readonly IWriteRepository<DataFile> dataFileWriteRepository;

        public StorageService(IReadRepository<User> userReadRepository, IWriteRepository<DataFile> dataFileWriteRepository)
        {
            EnsureArg.IsNotNull(userReadRepository);
            EnsureArg.IsNotNull(dataFileWriteRepository);

            this.userReadRepository = userReadRepository;
            this.dataFileWriteRepository = dataFileWriteRepository;
        }

        public async Task<Result> AddData(AddDataCommand command)
        {
            var userResult = await userReadRepository.FindOne(u => u.Username == command.Username).ToResult("User not found");

            return userResult
                .Map(u => DataFile.Create(command.Filename, command.ContentStream.ToByteArray(), u))
                .Map(df => dataFileWriteRepository.Create(df.Value))
                .Map(_ => dataFileWriteRepository.Commit());
        }
    }
}
