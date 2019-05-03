using System;
using System.Collections.Generic;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context
{

    public interface IUserService{
        ServiceResult<string> Add(FullUser user);
        ServiceResult<IEnumerable<UserServiceModel>> All();
        ServiceResult<string> Delete(Guid identifier);
        ServiceResult<UserServiceModel> Get(Guid identifer);
        ServiceResult<FullUser> Update(FullUser user);
    }
}