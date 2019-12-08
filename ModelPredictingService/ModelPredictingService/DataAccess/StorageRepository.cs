using ModelPredictingService.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ModelTrainingService.DataAccess
{
    public class StorageRepository : IStorageRepository
    {
        private readonly HttpClient _client;

        public StorageRepository(Options options)
        {
            _client =
                new HttpClient {BaseAddress = new Uri(options.StorageEndpoint)};
        }

        public async Task GetUserData(string username, string filename)
        {
            var uri = new Uri(_client.BaseAddress + "?Username=" + username);
            var response = await _client.GetAsync(uri);

            await using var file = File.Create(filename);

            var contentStream = await response.Content.ReadAsStreamAsync();
            await contentStream.CopyToAsync(file);
        }

        public async Task GetUserModel(string username)
        {
            var uri = new Uri(_client.BaseAddress + "/models/" + username);
            var response = await _client.GetAsync(uri);

            await using var file = File.Create(username + ".zip");

            var contentStream = await response.Content.ReadAsStreamAsync();
            await contentStream.CopyToAsync(file);
        }
    }
}