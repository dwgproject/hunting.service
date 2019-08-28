using System.Collections.Generic;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context.Extensions
{
    public static class PartialHuntingConverter
    {
        public static PartialHunting ConvertToModel(this PartialHuntingServiceModel model)
        {
            if(model is null)
                return new PartialHunting();
            return new PartialHunting(){
                Hunting = model.Hunting.ConvertToModel(),
                Identifier = model.Identifier,
                Number = model.Number,
                Status = (Status)model.Status,
                PartialHunters = model.PartialHunters.ConvertCollectionToModel()
            };
        }

        public static PartialHuntingServiceModel ConvertToService(this PartialHunting model)
        {
            Repository.Model.Hunting hunting = model.Hunting;
            if(model is null)
                return new PartialHuntingServiceModel();

            return new PartialHuntingServiceModel(){
                Identifier = model.Identifier,
                Number = model.Number,
                Status = (StatusServiceModel)model.Status,
                Hunting = new HuntingServiceModel(){Identifier = model.Hunting.Identifier} 
            };
        }

        public static ICollection<PartialHunting> ConvertCollectionToModel(this ICollection<PartialHuntingServiceModel> model)
        {
            if(model is null)
                return new List<PartialHunting>();
            
            IList<PartialHunting> list = new List<PartialHunting>();
            foreach (var item in model){
                list.Add(item.ConvertToModel());
            }
            return list;
        }

        public static ICollection<PartialHuntingServiceModel> ConvertCollectionToService(this ICollection<PartialHuntingServiceModel> partialHuntingServiceModels, ICollection<PartialHunting> model)
        {
            if(model is null)
                return new List<PartialHuntingServiceModel>();
            var list = new List<PartialHuntingServiceModel>();

            foreach (var item in model)
            {
                list.Add(item.ConvertToService());
            }
            return list;
        }
    }
}