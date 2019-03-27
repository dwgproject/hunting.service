using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Hunt.ServiceContext.Domain;
using Hunt.ServiceContext.Extensions;
using Hunt.ServiceContext.Result;
using HuntRepository.Infrastructure;

namespace Hunt.ServiceContext{

    public class ConfigurationService : IConfigurationService
    {
        private readonly IRoleRepository roleRepository;
        public ConfigurationService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public ServiceResult<string> AddRole(Role role)
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

        public ServiceResult<IEnumerable<Role>> GetRoles()
        {
            var elements = roleRepository.Query(rx => { return true; });
            if (elements.IsSuccess){
                ICollection<Role> roles = new Collection<Role>();
                foreach (var item in elements.Payload){
                    roles.Add(new Role().ConvertToServiceRole(item));
                }
                return ServiceResult<IEnumerable<Role>>.Success(roles, "cs6");
            }
            return ServiceResult<IEnumerable<Role>>.Failed(Enumerable.Empty<Role>(), "cs5");
        }
    }
}