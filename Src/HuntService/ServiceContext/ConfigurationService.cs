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
        public ConfigurationService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public ServiceResult<string> AddRole(RoleServiceModel role)
        {
            var addResult = roleRepository.Add(role.ConvertToModel());
            return addResult.IsSuccess ? ServiceResult<string>.Success(string.Empty, "cs1") : ServiceResult<string>.Failed(string.Empty, "cs2");
            //ServiceResult<string>(addResult.IsSuccess, addResult.IsSuccess ? string.Empty : "Error during adding a role.");
        }

        public ServiceResult<string> DeleteRole(Guid identifier)
        {
            roleRepository.Delete(identifier); //błąd poprawić
            var findResult = roleRepository.Find(identifier);
            return findResult.IsSuccess ? ServiceResult<string>.Success(string.Empty, "cs3") : ServiceResult<string>.Failed(string.Empty, "cs4");
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
            return updateResult.IsSuccess ? ServiceResult<string>.Success(string.Empty,"") : ServiceResult<string>.Failed(string.Empty,"");
        }
    }
}