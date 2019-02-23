using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hunt.Model;

namespace Hunt.Model
{
    public class Hunter: User
    {
        public virtual ICollection<HunterHunting> Huntings { get; set; }         
    }
}