using System;
using System.ComponentModel.DataAnnotations;

namespace GravityZero.HuntingSupport.Repository.Model
{
    public class Animal{
        
        [Key]
        public Guid Identifier { get; set; }
        public string Name { get; set; }
    }
}