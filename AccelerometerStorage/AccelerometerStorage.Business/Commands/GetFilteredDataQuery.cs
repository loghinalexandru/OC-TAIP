using AccelerometerStorage.Domain;
using EnsureThat;

namespace AccelerometerStorage.Business
{
    public sealed class GetFilteredDataQuery
    {
        public string Username { get; }

        public FileType FileType { get; }

        public GetFilteredDataQuery(string username, FileType fileType)
        {
            Username = username;
            FileType = fileType;
        }
    }
}
