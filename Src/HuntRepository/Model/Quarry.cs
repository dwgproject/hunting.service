using System;
using System.ComponentModel.DataAnnotations;

namespace GravityZero.HuntingSupport.Repository.Model
{
    public class Quarry
    {
        [Key]
        public Guid Identifier { get; set; }
        public virtual Animal Animal { get; set; }
        public int Amount { get; set; }
        
    }
}