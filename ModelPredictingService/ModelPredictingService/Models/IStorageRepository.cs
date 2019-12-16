using System.Threading.Tasks;

namespace ModelPredictingService.Models
{
    public interface IStorageRepository
    {
        Task GetLatestUserData(string username, string processGuid);
        Task GetLatestUserModel(string username, string processGuid);
    }
}