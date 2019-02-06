using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hunt.Model{
    public class Hunting{

        [Key]
        public Guid Identifier { get; set; }
        public DateTime Issued { get; set; }
        public User Leader { get; set; }
        public bool Status {get;set;}
        public ICollection<User> Users { get; set; }
        public ICollection<Animal> Animals { get; set; }
    }
}