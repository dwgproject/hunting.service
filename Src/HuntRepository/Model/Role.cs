using System;
using System.ComponentModel.DataAnnotations;

namespace GravityZero.HuntingSupport.Repository.Model
{
    public class Role
    {
        [Key]
        public Guid Identifier { get; set; } 
        [Required]
        public string Name { get; set; }
    }
}