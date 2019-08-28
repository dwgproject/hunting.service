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

        public static PartialHuntersServiceModel ConvertToService(this PartialHuntersList model)
        {
            if(model is null)
                return new PartialHuntersServiceModel();
            return new PartialHuntersServiceModel(){
                Identifier = model.Identifier,
                HunterNumber = model.HunterNumber,
                User = new User().ConverToUserService()
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

        public static ICollection<PartialHuntersServiceModel> ConvertCollectionToService(this ICollection<PartialHuntersServiceModel> partialHunters, ICollection<PartialHuntersList> model)
        {
            if(model is null)
                return new List<PartialHuntersServiceModel>();
            var list = new List<PartialHuntersServiceModel>();

            foreach (var item in model)
            {
                list.Add(item.ConvertToService());
            }

            return list;
        }
    }
}