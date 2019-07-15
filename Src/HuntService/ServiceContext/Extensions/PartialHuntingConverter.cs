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
    }
}