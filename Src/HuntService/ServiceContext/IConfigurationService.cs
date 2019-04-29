using System;
using System.Collections.Generic;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context
{
    public interface IConfigurationService{
        ServiceResult<string> AddRole(RoleServiceModel role);
        ServiceResult<IEnumerable<RoleServiceModel>> GetRoles();
        ServiceResult<string> DeleteRole(Guid identifier);
    }
}