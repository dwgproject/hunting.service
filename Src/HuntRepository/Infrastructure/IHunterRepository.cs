using System;
using System.Collections.Generic;
using Hunt.Model;

namespace HuntRepository.Infrastructure
{
    public interface IHunterRepository: IModuleRepository<Hunter, User, Guid>
    {
        
         Result<IEnumerable<Hunter>> QueryHunter(Func<Hunter,bool> query);
    }
}