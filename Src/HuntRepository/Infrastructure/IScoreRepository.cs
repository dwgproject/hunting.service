using System;
using Hunt.Model;

namespace HuntRepository.Infrastructure
{
    public interface IScoreRepository: IModuleRepository<Score, Score, Guid>
    {
         
    }
}