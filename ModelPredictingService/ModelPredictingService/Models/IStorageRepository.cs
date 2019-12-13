using System.Threading.Tasks;

namespace ModelPredictingService.Models
{
    public interface IStorageRepository
    {
        Task GetLatestUserData(string username);
        Task GetLatestUserModel(string username);
    }
}