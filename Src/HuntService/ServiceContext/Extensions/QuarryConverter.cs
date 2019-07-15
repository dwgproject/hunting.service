using System.Collections.Generic;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context.Extensions
{
    public static class QuarryConverter
    {
        public static Quarry ConvertToModel(this QuarryServiceModel model)
        {
            if(model is null)
                return new Quarry();
            return new Quarry(){
                Identifier = model.Identifier,
                Animal = model.Animal.ConvertToModel(),
                Amount = model.Amount
            };
        }

        public static ICollection<Quarry> CovertCollectionToModel(this ICollection<QuarryServiceModel> model)
        {
            if(model is null){
                return new List<Quarry>();
            }
            List<Quarry> quarries = new List<Quarry>();
            foreach (var item in model)
            {
                quarries.Add(item.ConvertToModel());                
            }
            return quarries;
        }
    }
}