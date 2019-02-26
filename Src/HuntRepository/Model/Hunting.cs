using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hunt.Model;

namespace Hunt.Model{
    public class Hunting{

        [Key]
        public Guid Identifier { get; set; }
        public DateTime Issued { get; set; }
        
        [ForeignKey("UserIdentifier")]
        public User Leader { get; set; }
        public bool Status {get;set;}
        public ICollection<UserHunting> Users { get; set; }
        public ICollection<Animal> Animals { get; set; }
        //wszystkie mioty na polowanie
        public ICollection<PartialHunting> PartialHuntings {get; set;}
    }
}