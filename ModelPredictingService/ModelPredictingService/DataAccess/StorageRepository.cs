using ModelPredictingService.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ModelPredictingService.DataAccess
{
    public class StorageRepository : IStorageRepository
    {
        private readonly HttpClient _client;

        public StorageRepository(Options options)
        {
            _client =
                new HttpClient {BaseAddress = new Uri(options.StorageEndpoint)};
        }

        public async Task GetLatestUserData(string username)
        {
            var dataPath = $"data_{username}";
            var uri = new Uri(_client.BaseAddress + "/data/latest/" + username);
            var response = await _client.GetAsync(uri);

            var fileName = response.Content.Headers.ContentDisposition.FileName;

            Directory.CreateDirectory(dataPath);

            await using var file = File.Create(Path.Combine(dataPath, fileName));

            var contentStream = await response.Content.ReadAsStreamAsync();
            await contentStream.CopyToAsync(file);
        }

        public async Task GetLatestUserModel(string username)
        {
            var modelPath = $"model_{username}";
            var uri = new Uri(_client.BaseAddress + "/models/latest/" + username);
            var response = await _client.GetAsync(uri);

            var fileName = response.Content.Headers.ContentDisposition.FileName;

            Directory.CreateDirectory(modelPath);

            await using var file = File.Create(Path.Combine(modelPath, fileName));

            var contentStream = await response.Content.ReadAsStreamAsync();
            await contentStream.CopyToAsync(file);
        }
    }
}