using System;
using Hunt.Model;

namespace HuntRepository.Infrastructure
{
    public interface IRoleRepository: IModuleRepository<Role, Role, Guid>
    {
         
    }
}