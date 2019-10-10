using System;
using System.ComponentModel.DataAnnotations;

namespace JWTAuthority.Domain
{
    public class User
    {
        [Key]
        public String Username { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        public String Email { get; set; }
    }
}
