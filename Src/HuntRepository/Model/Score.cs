using System;
using System.ComponentModel.DataAnnotations;

namespace Hunt.Model{

    public class Score{
        [Key]
        public Guid Identifier { get; set;}
        public virtual User User { get; set; }
        public virtual Hunting Hunting { get; set; }
        public DateTime? Issued { get; set; }
        public virtual Quarry Quarry { get; set; }
        public int Quantity { get; set; }
    }
}