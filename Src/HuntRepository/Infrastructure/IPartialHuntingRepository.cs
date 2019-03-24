using System;
using Hunt.Model;

namespace HuntRepository.Infrastructure
{
    public interface IPartialHuntingRepository: IModuleRepository<PartialHunting, PartialHunting, Guid>
    {
         Result<PartialHunting> Start(Guid identifier);
         Result<PartialHunting> Finish(Guid identifier);
    }
}