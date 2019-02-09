using System;
using System.ComponentModel.DataAnnotations;

namespace Hunt.Model{

    public class Score{
        [Key]
        public Guid Identifier { get; set;}
        public User User { get; set; }
        public Hunting Hunting { get; set; }
        public DateTime? Issued { get; set; }
        public Animal Animal { get; set; }
        public int Quantity { get; set; }
    }
}