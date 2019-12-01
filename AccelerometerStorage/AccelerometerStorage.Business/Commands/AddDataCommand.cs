using System.IO;
using AccelerometerStorage.Domain;
using EnsureThat;

namespace AccelerometerStorage.Business
{
    public sealed class AddDataCommand
    {
        public AddDataCommand(string username, string filename, Stream contentStream, FileType fileType)
        {
            EnsureArg.IsNotNullOrEmpty(username);
            EnsureArg.IsNotNull(contentStream);
            EnsureArg.IsNotNull(contentStream);

            Username = username;
            Filename = filename;
            ContentStream = contentStream;
            FileType = fileType;
        }

        public string Username { get; }

        public string Filename { get; }

        public Stream ContentStream { get; }

        public FileType FileType { get; }
    }
}
