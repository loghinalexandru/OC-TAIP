using AccelerometerStorage.Domain;

namespace AccelerometerStorage.Tests.Common
{
    public static class UserFactory
    {
        public static User GetUser()
        {
            return User.Create("stefan").Value;
        }
    }
}
