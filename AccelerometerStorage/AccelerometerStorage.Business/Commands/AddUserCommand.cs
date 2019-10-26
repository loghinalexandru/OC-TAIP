using EnsureThat;

namespace AccelerometerStorage.Business
{
    public sealed class AddUserCommand
    {
        public string Username { get; }

        public AddUserCommand(string username)
        {
            EnsureArg.IsNotNullOrEmpty(username);

            Username = username;
        }
    }
}
