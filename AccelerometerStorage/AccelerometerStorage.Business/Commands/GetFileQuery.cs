using EnsureThat;
using System;

namespace AccelerometerStorage.Business
{
    public sealed class GetFileQuery
    {
        public string Username { get; }

        public Guid DataFileId { get; }

        public GetFileQuery(string username, Guid dataFileId)
        {
            EnsureArg.IsNotNullOrEmpty(username);
            EnsureArg.IsNotEmpty(dataFileId);

            Username = username;
            DataFileId = dataFileId;
        }
    }
}
