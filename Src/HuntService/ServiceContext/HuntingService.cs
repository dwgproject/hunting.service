using System;
using System.Collections.Generic;
using System.Linq;
using GravityZero.HuntingSupport.Repository.Infrastructure;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Domain;
using GravityZero.HuntingSupport.Service.Context.Extensions;

namespace GravityZero.HuntingSupport.Service.Context
{
    public class HuntingService : IHuntingService
    {
        private readonly IQuarryRepository quarryRepository;
        private readonly IHuntingRepository huntingRepository;
        public HuntingService(IQuarryRepository quarryRepository, IHuntingRepository huntingRepository)
        {
            this.quarryRepository = quarryRepository;
            this.huntingRepository = huntingRepository;
        }
        public ServiceResult<string> AddQuarry(QuarryServiceModel quarry)
        {
            var addResult = quarryRepository.Add(quarry.ConvertToModel());
            return addResult.IsSuccess ? ServiceResult<string>.Success("Success - add","") : ServiceResult<string>.Failed(string.Empty,"");
        }

        public ServiceResult<IEnumerable<Quarry>> GetQuarries()
        {
            var queryResult = quarryRepository.Query(qr=>{return true;});
            return queryResult.IsSuccess ? ServiceResult<IEnumerable<Quarry>>.Success(queryResult.Payload,"") : 
                                    ServiceResult<IEnumerable<Quarry>>.Failed(Enumerable.Empty<Quarry>(),"");
        }
        
        public ServiceResult<IEnumerable<Quarry>> GetQuarry(Guid id)
        {
            var quaryResult = quarryRepository.Query(qr=>qr.Identifier == id);
            return quaryResult.IsSuccess ? ServiceResult<IEnumerable<Quarry>>.Success(quaryResult.Payload,"") : ServiceResult<IEnumerable<Quarry>>.Failed(Enumerable.Empty<Quarry>(),"");
        }

        public ServiceResult<string> DeleteQuarry(Guid id)
        {
            var deleteResult = quarryRepository.Delete(id);
            return deleteResult.IsSuccess ? ServiceResult<string>.Success("Success - delete", "") : ServiceResult<string>.Failed(string.Empty,"");
        }

        public ServiceResult<string> UpdateQuarry(QuarryServiceModel quarry)
        {
            var updateResult = quarryRepository.Update(quarry.ConvertToModel());
            return updateResult.IsSuccess ? ServiceResult<string>.Success("Success - update","") : ServiceResult<string>.Failed(string.Empty,"");

        }

        public ServiceResult<string> AddHunting(HuntingServiceModel hunting)
        {
            var addResult = huntingRepository.Add(hunting.ConvertToModel());
            return addResult.IsSuccess ? ServiceResult<string>.Success("Success - add", "") : ServiceResult<string>.Failed(string.Empty, "");
        }
    }
}