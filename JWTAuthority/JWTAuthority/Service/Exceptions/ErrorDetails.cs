using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JWTAuthority.Service.Exceptions
{
    public class ErrorDetails
    {
        public List<String> ErrorMessages { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
