using System;
using System.ComponentModel.DataAnnotations;
using Hunt.Model;

namespace Hunt.Model
{
    public class Hunter
    {
        [Key]
        public Guid Identifier { get; set; }
        public User User { get; set; }        
    }
}