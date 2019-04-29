using System;
using GravityZero.HuntingSupport.Repository.Model;

namespace GravityZero.HuntingSupport.Repository.Infrastructure
{
    public interface IPartialHuntingRepository: IModuleRepository<PartialHunting, PartialHunting, Guid>
    {
         RepositoryResult<PartialHunting> Start(Guid identifier);
         RepositoryResult<PartialHunting> Finish(Guid identifier);
    }
}