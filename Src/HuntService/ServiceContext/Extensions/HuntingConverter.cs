using GravityZero.HuntingSupport.Service.Context.Domain;
using GravityZero.HuntingSupport.Repository.Model;
using System.Collections.Generic;
using System.Linq;

namespace GravityZero.HuntingSupport.Service.Context.Extensions
{
    public static class HuntingConverter
    {
        public static GravityZero.HuntingSupport.Repository.Model.Hunting ConvertToModel(this HuntingServiceModel model)
        {
            if(model is null)
                return new Repository.Model.Hunting();

            return new GravityZero.HuntingSupport.Repository.Model.Hunting()
            {
                    Issued = model.Issued,
                    Leader = model.Leader.ConvertToUserModel(),
                    Description = model.Description,
                    Quarries = model.Quarries.CovertCollectionToModel(),
                    Status = (Status)model.Status,
                    Users = model.Users.ConvertCollectionToModel(),
                    Identifier = model.Identifier,
                    PartialHuntings = model.PartialHuntings.ConvertCollectionToModel()
            };           
        }

        public static HuntingServiceModel ConvertToService(this Repository.Model.Hunting hunting)
        {
            if(hunting is null)
                return new HuntingServiceModel();
            return new HuntingServiceModel(){
              Identifier = hunting.Identifier,
              Issued = hunting.Issued,
              Description = hunting.Description,
              Leader = new UserServiceModel().ConvertToservice(hunting.Leader),
              Status = (StatusServiceModel)hunting.Status,
              Quarries = new List<QuarryServiceModel>().ConvertCollectionToService(hunting.Quarries),
              Users = new List<UserHuntingServiceModel>().ConvertCollectionToService(hunting.Users),
              PartialHuntings = new List<PartialHuntingServiceModel>().ConvertCollectionToService(hunting.PartialHuntings)
            };
        }

        public static HuntingServiceModel ConvertToService(this HuntingServiceModel model, GravityZero.HuntingSupport.Repository.Model.Hunting hunting)
        {
            if(hunting is null)
                return new HuntingServiceModel();
            return new HuntingServiceModel(){
              Identifier = hunting.Identifier,
              Issued = hunting.Issued,
              Description = hunting.Description,
              Leader = new UserServiceModel().ConvertToservice(hunting.Leader),
              Status = (StatusServiceModel)hunting.Status,
              Quarries = new List<QuarryServiceModel>().ConvertCollectionToService(hunting.Quarries),
              Users = new List<UserHuntingServiceModel>().ConvertCollectionToService(hunting.Users),
              PartialHuntings = new List<PartialHuntingServiceModel>().ConvertCollectionToService(hunting.PartialHuntings)
            };
        }
    }
}