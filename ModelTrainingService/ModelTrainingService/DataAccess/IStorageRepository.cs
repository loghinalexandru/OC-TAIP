using System.Collections.Generic;
using System.Threading.Tasks;
using ModelTrainingService.Models;

namespace ModelTrainingService.DataAccess
{
    public interface IStorageRepository
    {
        Task GetAllUserData(string filename);
        Task GetUserData(string username, string filename);
        Task PostUserModel(string username, string modelPath);
        Task<IReadOnlyList<User>> GetUsers();
    }
}