using Microsoft.AspNetCore.Http;

namespace AccelerometerStorage.WebApi
{
    public sealed class DataModel
    {
        public IFormFile CsvFile { get; set; }
    }
}
