using System.Threading.Tasks;
using AccelerometerStorage.Common;
using AccelerometerStorage.Domain;
using CSharpFunctionalExtensions;
using EnsureThat;

namespace AccelerometerStorage.Business
{
    internal class UserService : IUserService
    {
        private readonly IWriteRepository<User> writeRepository;
        private readonly IReadRepository<User> readRepository;

        public UserService(IReadRepository<User> readRepository, IWriteRepository<User> writeRepository)
        {
            EnsureArg.IsNotNull(readRepository);
            EnsureArg.IsNotNull(writeRepository);

            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
        }

        public async Task<Result> AddUser(AddUserCommand command)
        {
            EnsureArg.IsNotNull(command);
            
            var maybeUser = await readRepository.FindOne(u => u.Username == command.Username);

            return maybeUser.ToInverseResult("User already exists")
                .Map(() => User.Create(command.Username))
                .Map(u => writeRepository.Create(u.Value))
                .Map(_ => writeRepository.Commit());
        }
    }
}
