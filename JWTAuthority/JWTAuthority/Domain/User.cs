using System;
using System.ComponentModel.DataAnnotations;

namespace JWTAuthority.Domain
{
    public class User
    {
        [Key]
        [MinLength(3)]
        public String Username { get; set; }

        [Required]
        [MinLength(8)]
        public String Password { get; set; }
    }
}
