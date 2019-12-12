using System.Threading.Tasks;

namespace ModelTrainingService.DataAccess
{
    public interface IStorageRepository
    {
        Task GetUserData(string username, string filename);

        Task GetUserModel(string username);
    }
}