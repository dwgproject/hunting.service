using System;
using System.Collections.Generic;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context
{
    public interface IHuntingService
    {
         ServiceResult<string> AddQuarry(QuarryServiceModel quarry);
         ServiceResult<IEnumerable<Quarry>> GetQuarries();
         ServiceResult<IEnumerable<Quarry>> GetQuarry(Guid id);
         ServiceResult<string> DeleteQuarry(Guid id);
         ServiceResult<string> UpdateQuarry(QuarryServiceModel quarry);
         ServiceResult<string> AddHunting(HuntingServiceModel hunting);
    }
}