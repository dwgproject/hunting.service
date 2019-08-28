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

        public static QuarryServiceModel ConvertToService(this Quarry model)
        {
            if(model is null)
                return new QuarryServiceModel();
            return new QuarryServiceModel(){
                Identifier = model.Identifier,
                Amount = model.Amount,
                Animal = new AnimalServiceModel().ConvertToServiceAnimal(model.Animal)
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

        public static ICollection<QuarryServiceModel> ConvertCollectionToService(this ICollection<QuarryServiceModel> quarries, ICollection<Quarry> model)
        {
            if(model is null)
                return new List<QuarryServiceModel>();
            List<QuarryServiceModel> list = new List<QuarryServiceModel>();
            foreach(var item in model){
                list.Add(item.ConvertToService());
            }  
            return list;
        }
    }
}