using System;
using GravityZero.HuntingSupport.Repository.Model;

namespace GravityZero.HuntingSupport.Repository.Infrastructure
{
    public interface IHuntingRepository: IModuleRepository<Hunting, Hunting, Guid>
    {
         RepositoryResult<Hunting> Start(Guid identifier);
         RepositoryResult<Hunting> Finish(Hunting hunting);
    }
}