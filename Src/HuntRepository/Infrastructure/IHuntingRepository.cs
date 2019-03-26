using System;
using Hunt.Model;

namespace HuntRepository.Infrastructure
{
    public interface IHuntingRepository: IModuleRepository<Hunting, Hunting, Guid>
    {
         RepositoryResult<Hunting> Start(Guid identifier);
         RepositoryResult<Hunting> Finish(Hunting hunting);
    }
}