using ModelTrainingService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public async Task GetAllUserData(string filename)
        {
            var uri = new Uri(_client.BaseAddress + "?Username=");
            var response = await _client.GetAsync(uri);

            await using var file = File.Create(filename);

            var contentStream = await response.Content.ReadAsStreamAsync();
            await contentStream.CopyToAsync(file);
        }

        public async Task GetUserData(string username, string filename)
        {
            var uri = new Uri(_client.BaseAddress + "?Username=" + username);
            var response = await _client.GetAsync(uri);

            await using var file = File.Create(filename);

            var contentStream = await response.Content.ReadAsStreamAsync();
            await contentStream.CopyToAsync(file);
        }

        public async Task PostUserModel(string username, string modelPath)
        {
            var uri = new Uri(_client.BaseAddress + "/models/" + username);
            var formContent = new MultipartFormDataContent();
            var model = new ByteArrayContent(File.ReadAllBytes(modelPath));
            formContent.Add(model, "modelFile", modelPath);

            await _client.PostAsync(uri, formContent);
        }

        public async Task<IReadOnlyList<User>> GetUsers()
        {
            var uri = new Uri(_client.BaseAddress + "/users");

            var response = await _client.GetAsync(uri);

            var jsonPayload = await response.Content.ReadAsStringAsync();

            return
                JsonConvert.DeserializeObject<List<User>>(jsonPayload);
        }
    }
}