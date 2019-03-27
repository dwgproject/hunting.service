using System;
using System.Collections.Generic;
using Hunt.ServiceContext.Domain;
using Hunt.ServiceContext.Result;

namespace Hunt.ServiceContext{

    public interface IUserService{
        ServiceResult<string> Add(FullUser user);
        ServiceResult<IEnumerable<User>> All();
        ServiceResult<string> Delete(Guid identifier);
        ServiceResult<User> Get(Guid identifer);
        ServiceResult<User> Update(FullUser user);
    }
}