using System;

namespace Hunt.Domain{
    public class User{
        public Guid Identifier { get; set; }
        public string Name { get; set;}
        public string Surname { get; set; }
    }
}