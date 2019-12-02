using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace JWTAuthority.Domain
{
    public class User
    {
        [Key]
        [NotNull]
        public String Username { get; set; }
        [Required]
        [NotNull]
        public String Password { get; set; }
        [NotNull]
        [Required]
        public String Email { get; set; }
    }
}
