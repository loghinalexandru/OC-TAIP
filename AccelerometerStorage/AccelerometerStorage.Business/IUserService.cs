using AccelerometerStorage.Domain;
using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace AccelerometerStorage.Business
{
    public interface IUserService
    {
        Task<Result<User>> AddUser(AddUserCommand command);

        Task<Maybe<User>> GetByUsername(string username);
    }
}
