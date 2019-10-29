namespace JWTAuthority.Helpers
{
    public interface IHashHelper
    {
        string GetHashAsString(string message);
    }
}