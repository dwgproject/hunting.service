using System;
using Hunt.Model;

namespace Hunt.Model
{
    public class Quarry
    {
        public Guid Identifier { get; set; }
        public Animal Animal { get; set; }
        public int Amount { get; set; }
        
    }
}