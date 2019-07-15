using System.Collections.Generic;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context.Extensions
{
    public static class PartialHuntersConverter
    {
        public static PartialHuntersList ConvertToModel(this PartialHuntersServiceModel model)
        {
            if(model is null)
                return new PartialHuntersList();
            return new PartialHuntersList(){
                HunterNumber = model.HunterNumber,
                Identifier = model.Identifier,
                User = {Identifier = model.Identifier}
            };
        }

        public static ICollection<PartialHuntersList> ConvertCollectionToModel(this ICollection<PartialHuntersServiceModel> model)
        {
            if(model is null)
                return new List<PartialHuntersList>();
            IList<PartialHuntersList> list = new List<PartialHuntersList>();
            foreach (var item in model){
                list.Add(item.ConvertToModel());
            }
            return list;
        }
    }
}