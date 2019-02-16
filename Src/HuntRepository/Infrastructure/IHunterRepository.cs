using System;
using Hunt.Model;

namespace HuntRepository.Infrastructure
{
    public interface IHunterRepository: IModuleRepository<Hunter, User, Guid>
    {
         
    }
}