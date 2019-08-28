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

        public ServiceResult<IEnumerable<QuarryServiceModel>> GetQuarries()
        {
            var queryResult = quarryRepository.Query(qr=>{return true;});
            var list = new List<QuarryServiceModel>();
            foreach (var item in queryResult.Payload)
            {
                list.Add(item.ConvertToService());
            }
            return queryResult.IsSuccess ? ServiceResult<IEnumerable<QuarryServiceModel>>.Success(list,"") : 
                                    ServiceResult<IEnumerable<QuarryServiceModel>>.Failed(Enumerable.Empty<QuarryServiceModel>(),"");
        }
        
        public ServiceResult<IEnumerable<QuarryServiceModel>> GetQuarry(Guid id)
        {
            var quaryResult = quarryRepository.Query(qr=>qr.Identifier == id);
            var list = new List<QuarryServiceModel>();
            foreach (var item in quaryResult.Payload)
            {
                list.Add(item.ConvertToService());
            }
            return quaryResult.IsSuccess ? ServiceResult<IEnumerable<QuarryServiceModel>>.Success(list,"") : ServiceResult<IEnumerable<QuarryServiceModel>>.Failed(Enumerable.Empty<QuarryServiceModel>(),"");
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

        public ServiceResult<IEnumerable<HuntingServiceModel>> GetHuntings()
        {
            var getResult = huntingRepository.Query(qr =>{return true;});
            List<HuntingServiceModel> result = new List<HuntingServiceModel>();
            foreach (var item in getResult.Payload)
            {
                result.Add(item.ConvertToService());
            }
            return getResult.IsSuccess ? ServiceResult<IEnumerable<HuntingServiceModel>>.Success(result,"") :
                                            ServiceResult<IEnumerable<HuntingServiceModel>>.Failed(Enumerable.Empty<HuntingServiceModel>(),"");
        }

        public ServiceResult<string> UpdateHunting(HuntingServiceModel hunting)
        {
            var updateResult = huntingRepository.Update(hunting.ConvertToModel());
            return updateResult.IsSuccess ? ServiceResult<string>.Success("Success - update", "") : ServiceResult<string>.Failed(string.Empty,"");
        }

        public ServiceResult<IEnumerable<HuntingServiceModel>> GetHunting(Guid id)
        {
            var queryResult = huntingRepository.Query(i=>i.Identifier == id);
            IList<HuntingServiceModel> result = new List<HuntingServiceModel>();
            foreach(var item in queryResult.Payload){
                result.Add(item.ConvertToService());
            }
            return queryResult.IsSuccess ? ServiceResult<IEnumerable<HuntingServiceModel>>.Success(result,"") : ServiceResult<IEnumerable<HuntingServiceModel>>.Failed(Enumerable.Empty<HuntingServiceModel>(),"");
        }
    }
}