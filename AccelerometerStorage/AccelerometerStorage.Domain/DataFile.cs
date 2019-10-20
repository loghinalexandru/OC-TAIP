using System;
using AccelerometerStorage.Common;
using CSharpFunctionalExtensions;

namespace AccelerometerStorage.Domain
{
    public sealed class DataFile : Entity
    {
        private DataFile()
        {
        }

        private DataFile(string filename, byte[] content, User user)
        {
            Filename = filename;
            Content = content;
            UserId = user.Id;
            User = user;
            UploadedAt = DateTime.Now;
        }

        public string Filename { get; private set; }

        public byte[] Content { get; private set; }

        public Guid UserId { get; private set; }

        public User User { get; private set; }

        public DateTime UploadedAt { get; private set; }

        public static Result<DataFile> Create(string filename, byte[] content, User user)
        {
            var filenameResult = filename.EnsureIsValidString("Invalid file name");
            var contentResult = content.EnsureExists("Content should not be null");
            var userResult = user.EnsureExists("File must belong to an user");

            return Result.FirstFailureOrSuccess(filenameResult, contentResult, userResult)
                .Map(() => new DataFile(filename, content, user));
        }
    }
}
