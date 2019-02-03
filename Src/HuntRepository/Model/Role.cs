using System;
using System.ComponentModel.DataAnnotations;

namespace Hunt.Model{

    public class Role
    {
        [Key]
        public Guid Identifier { get; set; } 
        public string Name { get; set; }
    }
}