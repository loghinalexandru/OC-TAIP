using EnsureThat;

namespace AccelerometerStorage.Business
{
    public sealed class GetFilteredDataQuery
    {
        public string Username { get; }

        public GetFilteredDataQuery(string username)
        {
            Username = username;
        }
    }
}
