using System;
using System.ComponentModel.DataAnnotations;

namespace Hunt.Model{
    public class Position{
        [Key]
        public Guid Identifier { get; set; }
        public virtual User User { get; set; }
        public double X { get; set;}
        public double y { get; set;}
    }
}