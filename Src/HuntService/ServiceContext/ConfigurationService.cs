using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GravityZero.HuntingSupport.Repository.Infrastructure;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Domain;
using GravityZero.HuntingSupport.Service.Context.Extensions;

namespace GravityZero.HuntingSupport.Service.Context
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IAnimalRepository animalRepository;
        public ConfigurationService(IRoleRepository roleRepository, IAnimalRepository animalRepository)
        {
            this.roleRepository = roleRepository;
            this.animalRepository = animalRepository;
        }

        public ServiceResult<string> AddRole(RoleServiceModel role)
        {
            var addResult = roleRepository.Add(role.ConvertToModel());
            return addResult.IsSuccess ? ServiceResult<string>.Success("success - add", "cs1") : ServiceResult<string>.Failed(string.Empty, "cs2");
            //ServiceResult<string>(addResult.IsSuccess, addResult.IsSuccess ? string.Empty : "Error during adding a role.");
        }

        public ServiceResult<string> DeleteRole(Guid identifier)
        {
            RepositoryResult<string> deleteResult = roleRepository.Delete(identifier); //błąd poprawić
            //var findResult = roleRepository.Find(identifier);
            return deleteResult.IsSuccess ? ServiceResult<string>.Success("success - delete", "cs3") : ServiceResult<string>.Failed(string.Empty, "cs4");
        }

        public ServiceResult<IEnumerable<RoleServiceModel>> GetRoles()
        {
            var elements = roleRepository.Query(rx => { return true; });
            if (elements.IsSuccess){
                ICollection<RoleServiceModel> roles = new Collection<RoleServiceModel>();
                foreach (var item in elements.Payload){
                    roles.Add(new RoleServiceModel().ConvertToServiceRole(item));
                }
                return ServiceResult<IEnumerable<RoleServiceModel>>.Success(roles, "cs6");
            }
            return ServiceResult<IEnumerable<RoleServiceModel>>.Failed(Enumerable.Empty<RoleServiceModel>(), "cs5");
        }

        public ServiceResult<IEnumerable<RoleServiceModel>> GetRole(string name)
        {
            var element = roleRepository.Query(rx=>rx.Name == name);
            if(element.IsSuccess){
                ICollection<RoleServiceModel> roles = new Collection<RoleServiceModel>();
                foreach(var item in element.Payload){
                    roles.Add(new RoleServiceModel().ConvertToServiceRole(item));
                }
                RoleServiceModel role = new RoleServiceModel().ConvertToServiceRole(element.Payload.FirstOrDefault());
                return ServiceResult<IEnumerable<RoleServiceModel>>.Success(roles,"cs");    
            }
            return ServiceResult<IEnumerable<RoleServiceModel>>.Failed(Enumerable.Empty<RoleServiceModel>(),"");
        }

        public ServiceResult<string> UpdateRole(RoleServiceModel role)
        {
            var updateResult = roleRepository.Update(role.ConvertToModel());
            return updateResult.IsSuccess ? ServiceResult<string>.Success("success - update","") : ServiceResult<string>.Failed(string.Empty,"");
        }

        public ServiceResult<string> AddAnimal(AnimalServiceModel animal)
        {
            var addResult = animalRepository.Add(animal.ConvertToModel());
            return addResult.IsSuccess ? ServiceResult<string>.Success("success - add","") : ServiceResult<string>.Failed(string.Empty,"");
        }

        public ServiceResult<string> DeleteAnimal(Guid identifier)
        {
            var deleteResult = animalRepository.Delete(identifier);
            return deleteResult.IsSuccess ? ServiceResult<string>.Success("success - delete", "") : ServiceResult<string>.Failed(string.Empty,"");
        }

        public ServiceResult<IEnumerable<AnimalServiceModel>> GetAnimals()
        {
            var queryResult = animalRepository.Query(ar=>{return true;});
            if(queryResult.IsSuccess){
                ICollection<AnimalServiceModel> animals = new Collection<AnimalServiceModel>();
                foreach (var item in queryResult.Payload)
                {
                    animals.Add(item.ConvertToServiceAnimal());
                }
                return ServiceResult<IEnumerable<AnimalServiceModel>>.Success(animals,"");
            }
            return ServiceResult<IEnumerable<AnimalServiceModel>>.Failed(Enumerable.Empty<AnimalServiceModel>(),"");
        }

        public ServiceResult<IEnumerable<AnimalServiceModel>> GetAnimal(string name)
        {
            var queryResult = animalRepository.Query(ar=>ar.Name == name);
            if(queryResult.IsSuccess){
                ICollection<AnimalServiceModel> animals = new Collection<AnimalServiceModel>();
                foreach (var item in queryResult.Payload)
                {
                    animals.Add(item.ConvertToServiceAnimal());
                }
                return ServiceResult<IEnumerable<AnimalServiceModel>>.Success(animals,"");
            }
            return ServiceResult<IEnumerable<AnimalServiceModel>>.Failed(Enumerable.Empty<AnimalServiceModel>(),"");
        }

        public ServiceResult<string> UpdateAnimal(AnimalServiceModel animal)
        {
            var updateResult = animalRepository.Update(animal.ConvertToModel());
            return updateResult.IsSuccess ? ServiceResult<string>.Success("Success - update","") : ServiceResult<string>.Failed(string.Empty, "");
        }
    }
}