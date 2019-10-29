using JWTAuthority.Domain;

namespace JWTAuthority.Helpers
{
    public interface ITokenBuilder
    {
        string GetToken(User user);
    }
}