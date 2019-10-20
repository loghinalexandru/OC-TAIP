using System.Collections.Generic;
using System.Linq;
using AccelerometerStorage.Common;
using CSharpFunctionalExtensions;

namespace AccelerometerStorage.Domain
{
    public sealed class User : Entity
    {
        private readonly ICollection<DataFile> dataFiles = new List<DataFile>();
        
        private User()
        {
        }

        private User(string username)
        {
            Username = username;
        }

        public string Username { get; private set; }

        public IEnumerable<DataFile> DataFiles => dataFiles.AsEnumerable();

        public static Result<User> Create(string username)
        {
            var usernameResult = username.EnsureIsValidString("Invalid username");

            return usernameResult.Map(u => new User(u));
        }
    }
}
