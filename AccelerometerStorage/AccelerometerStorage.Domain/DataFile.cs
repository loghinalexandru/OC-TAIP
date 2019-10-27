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

        private DataFile(string filename, User user)
        {
            Filename = filename;
            UserId = user.Id;
            User = user;
            UploadedAt = DateTime.Now;
        }

        public string Filename { get; private set; }

        public Guid UserId { get; private set; }

        public User User { get; private set; }

        public DateTime UploadedAt { get; private set; }

        public static Result<DataFile> Create(string filename, User user)
        {
            var filenameResult = filename.EnsureIsValidString("Invalid file name");
            var userResult = user.EnsureExists("File must belong to an user");

            return Result.FirstFailureOrSuccess(filenameResult, userResult)
                .Map(() => new DataFile(filename, user));
        }
    }
}
