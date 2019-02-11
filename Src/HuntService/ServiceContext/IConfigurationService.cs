using System;
using System.Collections.Generic;
using Hunt.ServiceContext.Domain;
using Hunt.ServiceContext.Results;

namespace Hunt.ServiceContext{

    public interface IConfigurationService{
        Result<string> AddRole(Role role);
        Result<IEnumerable<Role>> GetRoles();
        Result<string> DeleteRole(Guid identifier);
    }
}