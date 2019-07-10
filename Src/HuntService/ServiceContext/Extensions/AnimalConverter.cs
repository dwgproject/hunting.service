using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context.Extensions
{
    public static class AnimalConverter
    {
        public static Animal ConvertToModel(this AnimalServiceModel model)
        {
            if(model is null)
                return new Animal();
            
            return new Animal(){
                Identifier = model.Identifier,
                Name = model.Name
            };
        }

        public static AnimalServiceModel ConvertToServiceAnimal(this Animal animal)
        {
            if(animal is null)
                return new AnimalServiceModel();
            return new AnimalServiceModel(){
                Identifier = animal.Identifier,
                Name = animal.Name
            };
        }
    }
}