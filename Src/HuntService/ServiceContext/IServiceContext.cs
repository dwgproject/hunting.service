using System;
using GravityZero.HuntingSupport.Service.Context;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Session
{
    public interface IServiceContext
    {
        //ServiceResult<string> SignUp(FullUser user);//zapisz się
        ServiceResult<Guid> SignIn(Authentication authentication);
        ServiceResult<string> SignOut(Guid identifier);
        bool CheckSession(Guid identifier);
    }
}