using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GravityZero.HuntingSupport.Repository.Model
{
    public class Hunting{

        [Key]
        public Guid Identifier { get; set; }
        public DateTime Issued { get; set; }
        
        [ForeignKey("UserIdentifier")]
        public virtual User Leader { get; set; }
        public virtual Status Status {get;set;}
        public string Description { get; set; }
        public virtual ICollection<UserHunting> Users { get; set; }
        public virtual ICollection<Quarry> Quarries { get; set; }
        //wszystkie mioty na polowanie
        public virtual ICollection<PartialHunting> PartialHuntings {get; set;}
    }
}