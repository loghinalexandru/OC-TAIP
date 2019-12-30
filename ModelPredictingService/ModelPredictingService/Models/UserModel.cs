namespace ModelPredictingService.Models
{
    public sealed class UserModel
    {
        public string Username { get; }
        public string Email { get; }

        public UserModel(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}