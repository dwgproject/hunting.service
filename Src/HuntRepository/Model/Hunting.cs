using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hunt.Model;

namespace Hunt.Model
{
    public class Hunting{

        [Key]
        public Guid Identifier { get; set; }
        public DateTime Issued { get; set; }
        
        [ForeignKey("UserIdentifier")]
        public User Leader { get; set; }
        public Status Status {get;set;}
        public string Description { get; set; }
        public ICollection<UserHunting> Users { get; set; }
        public ICollection<Quarry> Quarries { get; set; }
        //wszystkie mioty na polowanie
        public ICollection<PartialHunting> PartialHuntings {get; set;}
    }
}