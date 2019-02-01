using System;
using System.ComponentModel.DataAnnotations;

namespace Hunt.Model{

    public class Club{
        
        [Key]
        public Guid Identifier { get; set; }
        public string Name { get; set; }
    }

}