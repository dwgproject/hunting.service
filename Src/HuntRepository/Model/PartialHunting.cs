using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Hunt.Model
{
    public class PartialHunting
    {
        [Key]
        public Guid Identifier { get; set; }
        public int Number { get; set; }
        [IgnoreDataMember]
        [ForeignKey("HuntingIdentifier")]
        public Hunting Hunting { get;set; }
        public Status Status { get;set; }
        public ICollection<PartialHuntersList> PartialHunters {get; set;}
    }
}