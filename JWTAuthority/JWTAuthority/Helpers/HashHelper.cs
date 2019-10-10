using System.Security.Cryptography;
using System.Text;

namespace JWTAuthority.Helpers
{
    public class HashHelper : IHashHelper
    {
        private readonly HashAlgorithm _hasher;

        public HashHelper(HashAlgorithm hasher)
        {
            _hasher = hasher;
        }

        public string GetHashAsString(string message)
        {
            var passwordHash = _hasher.ComputeHash(Encoding.UTF8.GetBytes(message));
            var result = new StringBuilder();

            foreach (var hashByte in passwordHash)
            {
                result.AppendFormat("{0:x2}", hashByte);
            }

            return result.ToString();
        }
    }
}
