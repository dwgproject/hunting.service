using System;
using System.Collections.Generic;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context
{
    public interface IConfigurationService{
        ServiceResult<string> AddRole(RoleServiceModel role);
        ServiceResult<IEnumerable<RoleServiceModel>> GetRoles();
        ServiceResult<string> DeleteRole(Guid identifier);
        ServiceResult<IEnumerable<RoleServiceModel>> GetRole(string name);
        ServiceResult<string> UpdateRole(RoleServiceModel role);
        ServiceResult<string> AddAnimal(AnimalServiceModel animal);
        ServiceResult<string> DeleteAnimal(Guid identifier);
        ServiceResult<IEnumerable<AnimalServiceModel>> GetAnimals();
        ServiceResult<IEnumerable<AnimalServiceModel>> GetAnimal(string name);
        ServiceResult<string> UpdateAnimal(AnimalServiceModel animal);
    }
}