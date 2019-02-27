using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hunt.Model
{
    public class PartialHunting
    {
        [Key]
        public Guid Identifier { get; set; }
        public int Number { get; set; }
        public Hunting Hunting { get;set; }
        public bool Status { get;set; }
        public ICollection<PartialHuntersList> PartialHunters {get; set;}
    }
}