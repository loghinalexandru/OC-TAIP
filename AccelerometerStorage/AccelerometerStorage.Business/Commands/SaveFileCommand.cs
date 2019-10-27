using EnsureThat;
using System;
using System.IO;

namespace AccelerometerStorage.Business
{
    public sealed class SaveFileCommand
    {
        public Stream Content { get; }

        public string Filename { get; }

        public string Username { get; }

        public Guid DataFileId { get;  }

        public SaveFileCommand(Stream content, string filename, string username, Guid dataFileId)
        {
            EnsureArg.IsNotNull(content);
            EnsureArg.IsNotNullOrEmpty(filename);
            EnsureArg.IsNotNullOrEmpty(username);
            EnsureArg.IsNotEmpty(dataFileId);

            Content = content;
            Filename = filename;
            Username = username;
            DataFileId = dataFileId;
        }
    }
}
