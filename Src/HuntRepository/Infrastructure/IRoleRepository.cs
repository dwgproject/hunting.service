using System;
using GravityZero.HuntingSupport.Repository.Model;

namespace GravityZero.HuntingSupport.Repository.Infrastructure
{
    public interface IRoleRepository: IModuleRepository<Role, Role, Guid>
    {
         
    }
}