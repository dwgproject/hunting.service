using System;
using Hunt.Model;

namespace HuntRepository.Infrastructure
{
    public interface IHuntingRepository: IModuleRepository<Hunting, Hunting, Guid>
    {
         Result<Hunting> Finish(Hunting hunting);
    }
}