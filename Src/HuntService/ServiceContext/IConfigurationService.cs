using System;
using System.Collections.Generic;
using Hunt.ServiceContext.Domain;
using Hunt.ServiceContext.Result;

namespace Hunt.ServiceContext{

    public interface IConfigurationService{
        ServiceResult<string> AddRole(Role role);
        ServiceResult<IEnumerable<Role>> GetRoles();
        ServiceResult<string> DeleteRole(Guid identifier);
    }
}