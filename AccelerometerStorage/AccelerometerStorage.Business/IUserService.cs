using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace AccelerometerStorage.Business
{
    public interface IUserService
    {
        Task<Result> AddUser(AddUserCommand command);
    }
}
