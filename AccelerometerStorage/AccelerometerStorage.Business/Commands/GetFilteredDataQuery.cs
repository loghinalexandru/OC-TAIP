using System;
using AccelerometerStorage.Domain;
using EnsureThat;
using Microsoft.VisualBasic;

namespace AccelerometerStorage.Business
{
    public sealed class GetFilteredDataQuery
    {
        public string Username { get; }

        public FileType FileType { get; }

        public DateTime StartingFrom { get; }

        public GetFilteredDataQuery(string username, FileType fileType, DateTime startingFrom)
        {
            Username = username;
            FileType = fileType;
            StartingFrom = startingFrom;
        }
    }
}
