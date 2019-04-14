using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual Role Role { get; set; }
        public virtual ICollection<UserHunting> Huntings { get; set; }
    }
}