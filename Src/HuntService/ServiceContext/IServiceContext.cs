using System;
using Hunt.ServiceContext.Result;
using Hunt.ServiceContext.Domain;

namespace Hunt.ServiceContext{
    public interface IServiceContext
    {
        //ServiceResult<string> SignUp(FullUser user);//zapisz się
        ServiceResult<User> SignIn(Authentication authentication);
        ServiceResult<string> SignOut(Guid identifier);
        bool CheckSession(Guid identifier);
    }
}