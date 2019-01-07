using System.Collections.Generic;
using HuntingHelperWebService.Model;

namespace HuntingHelperWebService.ApplicationContext{
    public class ApplicationContext : IApplicationContext{

        public IEnumerable<User> Users { get; private set; }

        public ApplicationContext()
        {
            
        }


    }
}