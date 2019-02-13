using System;
using System.ComponentModel.DataAnnotations;

namespace Hunt.Model
{
    public class User{
        
        [Key]
        public Guid Identifier { get; set; }
        public DateTime Issued { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}