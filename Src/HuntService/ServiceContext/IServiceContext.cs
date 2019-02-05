using System;
using Hunt.ServiceContext.Results;
using Hunt.ServiceContext.Domain;

namespace Hunt.ServiceContext{
    public interface IServiceContext
    {
        Result<string> SignUp(FullUser user);//zapisz się
        Result<User> SignIn(Authentication authentication);
        Result<string> SignOut(Guid identifier);
        bool CheckSession(Guid identifier);
    }
}