using System.Threading.Tasks;

namespace ModelPredictingService.Models
{
    public interface IStorageRepository
    {
        Task GetUserData(string username, string filename);

        Task GetUserModel(string username);
    }
}