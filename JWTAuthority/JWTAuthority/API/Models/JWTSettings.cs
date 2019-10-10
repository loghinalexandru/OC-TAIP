using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthority.API.Models
{
    public class JWTSettings
    {
        public String Key { get; set; }
        public String Issuer { get; set; }
    }
}
