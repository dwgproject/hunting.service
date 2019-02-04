using Hunt.Domain;
using Hunt.Model;
using Hunt.ServiceContext;
using HuntRepository.Infrastructure;
using System;
using System.Collections.Generic;

namespace Hunt.ServiceContext
{
    public class Context : IServiceContext
    {
        public Context(IRepository repository)
        {
        }

        public bool CheckSession(Guid identifier)
        {
            throw new NotImplementedException();
        }

        public Domain.User SignIn()
        {
            throw new NotImplementedException();
        }

        public void SignOut(Domain.User user)
        {
            throw new NotImplementedException();
        }

        public bool SignUp(FullUser user)
        {
            throw new NotImplementedException();
        }
    }
}