using System;
using Hunt.Model;

namespace HuntRepository.Infrastructure
{
    public interface IPartialHuntingRepository: IModuleRepository<PartialHunting, PartialHunting, Guid>
    {
         RepositoryResult<PartialHunting> Start(Guid identifier);
         RepositoryResult<PartialHunting> Finish(Guid identifier);
    }
}