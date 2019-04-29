using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace GravityZero.HuntingSupport.Repository.Model
{
    public class PartialHunting
    {
        [Key]
        public Guid Identifier { get; set; }
        public int Number { get; set; }
        [IgnoreDataMember]
        [ForeignKey("HuntingIdentifier")]
        public virtual Hunting Hunting { get;set; }
        public virtual Status Status { get;set; }
        public virtual ICollection<PartialHuntersList> PartialHunters {get; set;}
    }
}