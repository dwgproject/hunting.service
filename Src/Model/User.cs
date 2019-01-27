using System;
using System.Collections;

namespace HuntingHelperWebService.Model{
    public class User{
        
        public string Identifier { get; private set; }
        public DateTime Issued { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }


        public User()
        {
            
        }

    }
}