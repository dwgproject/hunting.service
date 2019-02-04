using System;
using Hunt.Domain;
using Hunt.Model;

namespace Hunt.ServiceContext{
    public interface IServiceContext
    {
        bool SignUp(FullUser user);
        Domain.User SignIn();
        void SignOut(Domain.User user);
        bool CheckSession(Guid identifier);
    }
}