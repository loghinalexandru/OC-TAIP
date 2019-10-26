using System.IO;
using EnsureThat;

namespace AccelerometerStorage.Business
{
    public sealed class AddDataCommand
    {
        public AddDataCommand(string username, string filename, Stream contentStream)
        {
            EnsureArg.IsNotNullOrEmpty(username);
            EnsureArg.IsNotNull(contentStream);
            EnsureArg.IsNotNull(contentStream);

            Username = username;
            Filename = filename;
            ContentStream = contentStream;
        }

        public string Username { get; }

        public string Filename { get; }

        public Stream ContentStream { get; }
    }
}
