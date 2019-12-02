using System;

namespace ModelTrainingService.Models
{
    public sealed class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}