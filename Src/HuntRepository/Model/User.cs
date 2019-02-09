using System;
using System.ComponentModel.DataAnnotations;

namespace Hunt.Model
{
    public class User{
        
        [Key]
        public Guid Identifier { get; set; }
        public DateTime Issued { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public Role Role { get; set; }
    }
}