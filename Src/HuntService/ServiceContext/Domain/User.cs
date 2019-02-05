using System;

namespace Hunt.ServiceContext.Domain{
    public class User{
        public Guid Identifier { get; set; }
        public string Name { get; set;}
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}