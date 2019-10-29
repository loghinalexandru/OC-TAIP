using Newtonsoft.Json;
using System.Collections.Generic;

namespace JWTAuthority.Service.Exceptions
{
    public class ErrorDetails
    {
        public Dictionary<string, string> ErrorMessages { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
