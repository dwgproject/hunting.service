using System;
using System.ComponentModel.DataAnnotations;

namespace Hunt.Model
{
    public class PartialHuntersList
    {
        [Key]
        public Guid Identifier { get; set; }
        public virtual User User { get;set; }
        // numer wylosowany na pojedynczy miot
        public int HunterNumber { get; set; }
    }
}