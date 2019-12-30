using AccelerometerStorage.Domain;
using EnsureThat;
using System.IO;

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

        public string Email { get; set; }

        public string Filename { get; }

        public Stream ContentStream { get; }

        public FileType FileType { get; }
    }
}