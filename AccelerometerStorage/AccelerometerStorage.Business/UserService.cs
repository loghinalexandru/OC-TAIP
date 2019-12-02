using System.Collections.Generic;
using System.Linq;
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

        public async Task<Result<User>> AddUser(AddUserCommand command)
        {
            EnsureArg.IsNotNull(command);

            var maybeUser = await readRepository.FindOne(u => u.Username == command.Username);

            return maybeUser.ToInverseResult("User already exists")
                .Map(() => User.Create(command.Username))
                .Map(u =>
                {
                    writeRepository.Create(u.Value);
                    writeRepository.Commit();
                    return u.Value;
                });
        }

        public async Task<Maybe<User>> GetByUsername(string username)
        {
            return await readRepository.FindOne(u => u.Username == username);
        }

        public async Task<IEnumerable<UserDto>> Get()
        {
            var users = await readRepository.GetAll();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username
            });
        }
    }
}
