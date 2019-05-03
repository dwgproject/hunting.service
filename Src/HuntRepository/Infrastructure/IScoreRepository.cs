using System;
using GravityZero.HuntingSupport.Repository.Model;

namespace GravityZero.HuntingSupport.Repository.Infrastructure
{
    public interface IScoreRepository: IModuleRepository<Score, Score, Guid>
    {
         
    }
}